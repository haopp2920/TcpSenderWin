namespace TcpSenderWin
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblIpRange = new Label();
            txtIpRange = new TextBox();
            lblExclude = new Label();
            txtExcludeIp = new TextBox();
            lblPort = new Label();
            lblInterval = new Label();
            lblType = new Label();
            txtPort = new TextBox();
            txtPath = new TextBox();
            this.txtPostData = new TextBox();
            txtLog = new TextBox();
            numInterval = new NumericUpDown();
            cmbRequestType = new ComboBox();
            btnStart = new Button();
            ((System.ComponentModel.ISupportInitialize)numInterval).BeginInit();
            SuspendLayout();
            // 
            // lblIpRange
            // 
            lblIpRange.AutoSize = true;
            lblIpRange.Font = new Font("思源黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            lblIpRange.Location = new Point(12, 14);
            lblIpRange.Name = "lblIpRange";
            lblIpRange.Size = new Size(93, 23);
            lblIpRange.TabIndex = 0;
            lblIpRange.Text = "IP段(起-止):";
            // 
            // txtIpRange
            // 
            txtIpRange.Font = new Font("思源黑体", 12F, FontStyle.Regular, GraphicsUnit.Point, 134);
            txtIpRange.Location = new Point(153, 11);
            txtIpRange.Name = "txtIpRange";
            txtIpRange.Size = new Size(205, 31);
            txtIpRange.TabIndex = 1;
            txtIpRange.Text = "192.168.1.1-192.168.1.254\t";
            // 
            // lblExclude
            // 
            lblExclude.AutoSize = true;
            lblExclude.Font = new Font("思源黑体", 12F);
            lblExclude.Location = new Point(12, 46);
            lblExclude.Name = "lblExclude";
            lblExclude.Size = new Size(135, 23);
            lblExclude.TabIndex = 2;
            lblExclude.Text = "排除IP(逗号隔开):";
            // 
            // txtExcludeIp
            // 
            txtExcludeIp.Font = new Font("思源黑体", 12F);
            txtExcludeIp.Location = new Point(153, 46);
            txtExcludeIp.Name = "txtExcludeIp";
            txtExcludeIp.Size = new Size(100, 31);
            txtExcludeIp.TabIndex = 3;
            txtExcludeIp.Text = "127.0.0.1";
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Font = new Font("思源黑体", 12F);
            lblPort.Location = new Point(12, 83);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(46, 23);
            lblPort.TabIndex = 4;
            lblPort.Text = "端口:";
            // 
            // lblInterval
            // 
            lblInterval.AutoSize = true;
            lblInterval.Font = new Font("思源黑体", 12F);
            lblInterval.Location = new Point(12, 117);
            lblInterval.Name = "lblInterval";
            lblInterval.Size = new Size(78, 23);
            lblInterval.TabIndex = 5;
            lblInterval.Text = "间隔(ms):";
            // 
            // lblType
            // 
            lblType.AutoSize = true;
            lblType.Font = new Font("思源黑体", 12F);
            lblType.Location = new Point(12, 149);
            lblType.Name = "lblType";
            lblType.Size = new Size(78, 23);
            lblType.TabIndex = 6;
            lblType.Text = "请求类型:";
            // 
            // txtPort
            // 
            txtPort.Font = new Font("思源黑体", 12F);
            txtPort.Location = new Point(153, 80);
            txtPort.Name = "txtPort";
            txtPort.Size = new Size(100, 31);
            txtPort.TabIndex = 9;
            txtPort.Text = "9000";
            // 
            // txtPath
            // 
            txtPath.Font = new Font("思源黑体", 12F);
            txtPath.Location = new Point(153, 189);
            txtPath.Name = "txtPath";
            txtPath.Size = new Size(205, 31);
            txtPath.TabIndex = 10;
            txtPath.Text = "/index.html";
            // 
            // txtPostData
            // 
            this.txtPostData.Font = new Font("思源黑体", 12F);
            this.txtPostData.Location = new Point(153, 226);
            this.txtPostData.Name = "txtPostData";
            this.txtPostData.Size = new Size(205, 31);
            this.txtPostData.TabIndex = 11;
            this.txtPostData.Text = "user=admin&pass=123";
            // 
            // txtLog
            // 
            txtLog.Font = new Font("思源黑体", 12F);
            txtLog.Location = new Point(364, 14);
            txtLog.Multiline = true;
            txtLog.Name = "txtLog";
            txtLog.ReadOnly = true;
            txtLog.ScrollBars = ScrollBars.Vertical;
            txtLog.Size = new Size(398, 243);
            txtLog.TabIndex = 12;
            // 
            // numInterval
            // 
            numInterval.Font = new Font("思源黑体", 12F);
            numInterval.Location = new Point(153, 115);
            numInterval.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
            numInterval.Name = "numInterval";
            numInterval.Size = new Size(100, 31);
            numInterval.TabIndex = 13;
            numInterval.Value = new decimal(new int[] { 100, 0, 0, 0 });
            // 
            // cmbRequestType
            // 
            cmbRequestType.Font = new Font("思源黑体", 12F);
            cmbRequestType.FormattingEnabled = true;
            cmbRequestType.Items.AddRange(new object[] { "原始数据", "HTTP GET", "HTTP POST" });
            cmbRequestType.Location = new Point(153, 152);
            cmbRequestType.Name = "cmbRequestType";
            cmbRequestType.Size = new Size(205, 31);
            cmbRequestType.TabIndex = 14;
            // 
            // btnStart
            // 
            btnStart.Font = new Font("思源黑体", 12F);
            btnStart.Location = new Point(364, 365);
            btnStart.Name = "btnStart";
            btnStart.Size = new Size(398, 64);
            btnStart.TabIndex = 15;
            btnStart.Text = "开始发送";
            btnStart.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnStart);
            Controls.Add(cmbRequestType);
            Controls.Add(numInterval);
            Controls.Add(txtLog);
            Controls.Add(this.txtPostData);
            Controls.Add(txtPath);
            Controls.Add(txtPort);
            Controls.Add(lblType);
            Controls.Add(lblInterval);
            Controls.Add(lblPort);
            Controls.Add(txtExcludeIp);
            Controls.Add(lblExclude);
            Controls.Add(txtIpRange);
            Controls.Add(lblIpRange);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)numInterval).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblIpRange;
        private TextBox txtIpRange;
        private Label lblExclude;
        private TextBox txtExcludeIp;
        private Label lblPort;
        private Label lblInterval;
        private Label lblType;
        private TextBox txtPostData;
        private TextBox txtPath;
        private TextBox txtPort;
        private TextBox txtLog;
        private NumericUpDown numInterval;
        private ComboBox cmbRequestType;
        private Button btnStart;
    }
}
