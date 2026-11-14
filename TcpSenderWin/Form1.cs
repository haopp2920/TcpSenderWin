using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TcpSenderWin
{
    public partial class Form1 : Form
    {
        private CancellationTokenSource? _cts;
        private Random _random = new Random();

        public Form1()
        {
            InitializeComponent();
            btnStart.Click += btnStart_Click;
            // 默认选中“原始数据”
            if (cmbRequestType.Items.Count > 0)
                cmbRequestType.SelectedIndex = 0;
        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            MessageBox.Show(text: "按钮已触发！");

            if (btnStart.Text == "开始发送")
            {
                // 简单的输入校验
                if (!int.TryParse(txtPort.Text, out int port))
                {
                    MessageBox.Show("端口必须是数字！");
                    return;
                }
                if (numInterval.Value <= 0)
                {
                    MessageBox.Show("间隔时间必须大于0毫秒，否则会死机！");
                }

                btnStart.Text = "停止发送";
                _cts = new CancellationTokenSource();
                txtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] 任务启动...\r\n");

                int interval = (int)numInterval.Value;

                // 解析排除列表
                HashSet<string> excludeList = new HashSet<string>();
                foreach (var ip in txtExcludeIp.Text.Split(new[] { ',', '，', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    excludeList.Add(ip.Trim());
                }

                // 启动后台线程
                await Task.Run(() => StartSending(port, interval, excludeList, _cts.Token));
            }
            else
            {
                _cts?.Cancel();
                btnStart.Text = "开始发送";
            }
        }

        private void StartSending(int port, int interval, HashSet<string> excludeList, CancellationToken token)
        {
            // 解析IP段
            string[] parts = txtIpRange.Text.Split('-');
            if (parts.Length != 2)
            {
                UpdateLog("[错误] IP段格式不正确，请使用如 192.168.1.1-192.168.1.254 的格式");
                this.Invoke(new Action(() => { btnStart.Text = "开始发送"; }));
                return;
            }

            try
            {
                uint startIp = IpToUint(parts[0].Trim());
                uint endIp = IpToUint(parts[1].Trim());

                if (startIp > endIp)
                {
                    UpdateLog("[错误] 起始 IP 不能大于结束 IP");
                    this.Invoke(new Action(() => { btnStart.Text = "开始发送"; }));
                    return;
                }

                while (!token.IsCancellationRequested)
                {
                    // 1. 随机挑选一个 IP
                    uint randomIpNum = (uint)_random.Next((int)startIp, (int)endIp + 1); // +1 确保包含结束IP
                    string targetIp = UintToIp(randomIpNum);

                    // 2. 检查是否在排除列表中
                    if (excludeList.Contains(targetIp) || targetIp == "0.0.0.0")
                    {
                        continue; // 跳过，重新挑选
                    }

                    try
                    {
                        using (TcpClient client = new TcpClient())
                        {
                            // 设置超时
                            client.SendTimeout = 2000;

                            // 异步连接，带1.5秒超时控制
                            var connectTask = client.ConnectAsync(targetIp, port);
                            if (Task.WhenAny(connectTask, Task.Delay(1500)).Result != connectTask)
                            {
                                throw new TimeoutException("连接超时");
                            }

                            NetworkStream stream = client.GetStream();

                            // 3. 生成请求数据
                            string data = GeneratePayload(targetIp);
                            byte[] buffer = Encoding.UTF8.GetBytes(data);

                            // 发送
                            stream.Write(buffer, 0, buffer.Length);

                            // 截取日志，防止太长刷屏
                            string displayData = data.Replace("\r\n", " ").Replace("\n", " ");
                            if (displayData.Length > 50) displayData = displayData.Substring(0, 50) + "...";

                            UpdateLog($"[{DateTime.Now:HH:mm:ss}] 发送至 {targetIp}:{port} -> {displayData}");
                        }
                    }
                    catch (Exception)
                    {
                        // 为了不刷屏，连接失败的日志可以选择静默，或者你放开下面这行
                        // UpdateLog($"[{DateTime.Now:HH:mm:ss}] {targetIp} 失败");
                    }

                    // 4. 等待间隔（使用 Task.Delay 可以随时被停止按钮打断）
                    try
                    {
                        Task.Delay(interval, token).Wait(token);
                    }
                    catch (OperationCanceledException) { break; }
                }
            }
            catch (Exception ex)
            {
                UpdateLog($"[系统错误] {ex.Message}");
            }
            finally
            {
                this.Invoke(new Action(() => { btnStart.Text = "开始发送"; }));
            }
        }

        // 根据下拉框生成报文
        private string GeneratePayload(string targetIp)
        {
            string timeStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string type = cmbRequestType.Text;

            if (type == "HTTP GET")
            {
                string path = string.IsNullOrWhiteSpace(txtPath.Text) ? "/" : txtPath.Text;
                return $"GET {path} HTTP/1.1\r\n" +
                       $"Host: {targetIp}\r\n" +
                       $"User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)\r\n" +
                       $"Time: {timeStr}\r\n" +
                       $"Connection: close\r\n\r\n";
            }
            else if (type == "HTTP POST")
            {
                string path = string.IsNullOrWhiteSpace(txtPath.Text) ? "/" : txtPath.Text;
                string postData = string.IsNullOrWhiteSpace(txtPostData.Text) ? "test=123" : txtPostData.Text;
                int contentLength = Encoding.UTF8.GetByteCount(postData);
                return $"POST {path} HTTP/1.1\r\n" +
                       $"Host: {targetIp}\r\n" +
                       $"User-Agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64)\r\n" +
                       $"Content-Type: application/x-www-form-urlencoded\r\n" +
                       $"Content-Length: {contentLength}\r\n" +
                       $"Time: {timeStr}\r\n" +
                       $"Connection: close\r\n\r\n" +
                       postData;
            }
            else
            {
                // 原始数据
                return $"Time: {timeStr} | Hello from C#!";
            }
        }

        // ================= 工具函数：IP 与 UInt 互转 =================
        private uint IpToUint(string ip)
        {
            byte[] bytes = IPAddress.Parse(ip).GetAddressBytes();
            Array.Reverse(bytes);
            return BitConverter.ToUInt32(bytes, 0);
        }

        private string UintToIp(uint ip)
        {
            byte[] bytes = BitConverter.GetBytes(ip);
            Array.Reverse(bytes);
            return new IPAddress(bytes).ToString();
        }

        // 安全更新UI日志
        private void UpdateLog(string message)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => UpdateLog(message)));
            }
            else
            {
                txtLog.AppendText(message + "\r\n");
                // 滚动到最后一行
                txtLog.SelectionStart = txtLog.Text.Length;
                txtLog.ScrollToCaret();

                // 保持日志不超过 200 行
                if (txtLog.Lines.Length > 200)
                {
                    txtLog.Text = string.Join("\r\n", txtLog.Lines, txtLog.Lines.Length - 100, 100);
                }
            }
        }
    }
}