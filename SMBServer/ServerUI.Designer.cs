namespace SMBServer
{
    partial class ServerUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerUI));
            this.comboIPAddress = new System.Windows.Forms.ComboBox();
            this.rbtNetBiosOverTCP = new System.Windows.Forms.RadioButton();
            this.rbtDirectTCPTransport = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblTransport = new System.Windows.Forms.Label();
            this.lblAddress = new System.Windows.Forms.Label();
            this.chkIntegratedWindowsAuthentication = new System.Windows.Forms.CheckBox();
            this.chkSMB1 = new System.Windows.Forms.CheckBox();
            this.chkSMB2 = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.netbiosPort = new System.Windows.Forms.NumericUpDown();
            this.directPort = new System.Windows.Forms.NumericUpDown();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.netbiosPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.directPort)).BeginInit();
            this.SuspendLayout();
            // 
            // comboIPAddress
            // 
            this.comboIPAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboIPAddress.FormattingEnabled = true;
            this.comboIPAddress.Location = new System.Drawing.Point(79, 6);
            this.comboIPAddress.Name = "comboIPAddress";
            this.comboIPAddress.Size = new System.Drawing.Size(121, 21);
            this.comboIPAddress.TabIndex = 0;
            // 
            // rbtNetBiosOverTCP
            // 
            this.rbtNetBiosOverTCP.AutoSize = true;
            this.rbtNetBiosOverTCP.Checked = true;
            this.rbtNetBiosOverTCP.Location = new System.Drawing.Point(79, 38);
            this.rbtNetBiosOverTCP.Name = "rbtNetBiosOverTCP";
            this.rbtNetBiosOverTCP.Size = new System.Drawing.Size(140, 17);
            this.rbtNetBiosOverTCP.TabIndex = 2;
            this.rbtNetBiosOverTCP.TabStop = true;
            this.rbtNetBiosOverTCP.Text = "NetBIOS over TCP, Port";
            this.rbtNetBiosOverTCP.UseVisualStyleBackColor = true;
            // 
            // rbtDirectTCPTransport
            // 
            this.rbtDirectTCPTransport.AutoSize = true;
            this.rbtDirectTCPTransport.Location = new System.Drawing.Point(79, 61);
            this.rbtDirectTCPTransport.Name = "rbtDirectTCPTransport";
            this.rbtDirectTCPTransport.Size = new System.Drawing.Size(150, 17);
            this.rbtDirectTCPTransport.TabIndex = 3;
            this.rbtDirectTCPTransport.Text = "Direct TCP Transport, Port";
            this.rbtDirectTCPTransport.UseVisualStyleBackColor = true;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(339, 9);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(91, 23);
            this.btnStart.TabIndex = 4;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnStop
            // 
            this.btnStop.Enabled = false;
            this.btnStop.Location = new System.Drawing.Point(339, 38);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(91, 23);
            this.btnStop.TabIndex = 5;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblTransport
            // 
            this.lblTransport.AutoSize = true;
            this.lblTransport.Location = new System.Drawing.Point(12, 40);
            this.lblTransport.Name = "lblTransport";
            this.lblTransport.Size = new System.Drawing.Size(55, 13);
            this.lblTransport.TabIndex = 14;
            this.lblTransport.Text = "Transport:";
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(12, 9);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(61, 13);
            this.lblAddress.TabIndex = 13;
            this.lblAddress.Text = "IP Address:";
            // 
            // chkIntegratedWindowsAuthentication
            // 
            this.chkIntegratedWindowsAuthentication.AutoSize = true;
            this.chkIntegratedWindowsAuthentication.Checked = true;
            this.chkIntegratedWindowsAuthentication.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIntegratedWindowsAuthentication.Location = new System.Drawing.Point(79, 114);
            this.chkIntegratedWindowsAuthentication.Name = "chkIntegratedWindowsAuthentication";
            this.chkIntegratedWindowsAuthentication.Size = new System.Drawing.Size(192, 17);
            this.chkIntegratedWindowsAuthentication.TabIndex = 15;
            this.chkIntegratedWindowsAuthentication.Text = "Integrated Windows Authentication";
            this.chkIntegratedWindowsAuthentication.UseVisualStyleBackColor = true;
            // 
            // chkSMB1
            // 
            this.chkSMB1.AutoSize = true;
            this.chkSMB1.Checked = true;
            this.chkSMB1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSMB1.Location = new System.Drawing.Point(79, 86);
            this.chkSMB1.Name = "chkSMB1";
            this.chkSMB1.Size = new System.Drawing.Size(95, 17);
            this.chkSMB1.TabIndex = 16;
            this.chkSMB1.Text = "SMB 1.0/CIFS";
            this.chkSMB1.UseVisualStyleBackColor = true;
            this.chkSMB1.CheckedChanged += new System.EventHandler(this.chkSMB1_CheckedChanged);
            // 
            // chkSMB2
            // 
            this.chkSMB2.AutoSize = true;
            this.chkSMB2.Checked = true;
            this.chkSMB2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSMB2.Location = new System.Drawing.Point(204, 86);
            this.chkSMB2.Name = "chkSMB2";
            this.chkSMB2.Size = new System.Drawing.Size(93, 17);
            this.chkSMB2.TabIndex = 17;
            this.chkSMB2.Text = "SMB 2.0 / 2.1";
            this.chkSMB2.UseVisualStyleBackColor = true;
            this.chkSMB2.CheckedChanged += new System.EventHandler(this.chkSMB2_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Protocol:";
            // 
            // netbiosPort
            // 
            this.netbiosPort.Location = new System.Drawing.Point(235, 38);
            this.netbiosPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.netbiosPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.netbiosPort.Name = "netbiosPort";
            this.netbiosPort.Size = new System.Drawing.Size(72, 20);
            this.netbiosPort.TabIndex = 19;
            this.netbiosPort.Value = new decimal(new int[] {
            139,
            0,
            0,
            0});
            this.netbiosPort.ValueChanged += new System.EventHandler(this.netbiosPort_ValueChanged);
            // 
            // directPort
            // 
            this.directPort.Location = new System.Drawing.Point(235, 60);
            this.directPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.directPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.directPort.Name = "directPort";
            this.directPort.Size = new System.Drawing.Size(72, 20);
            this.directPort.TabIndex = 20;
            this.directPort.Value = new decimal(new int[] {
            445,
            0,
            0,
            0});
            this.directPort.ValueChanged += new System.EventHandler(this.directPort_ValueChanged);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SMB Server";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // ServerUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(434, 131);
            this.Controls.Add(this.directPort);
            this.Controls.Add(this.netbiosPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkSMB1);
            this.Controls.Add(this.chkSMB2);
            this.Controls.Add(this.chkIntegratedWindowsAuthentication);
            this.Controls.Add(this.lblTransport);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.rbtDirectTCPTransport);
            this.Controls.Add(this.rbtNetBiosOverTCP);
            this.Controls.Add(this.comboIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(450, 170);
            this.MinimumSize = new System.Drawing.Size(450, 170);
            this.Name = "ServerUI";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "SMB Server";
            this.Load += new System.EventHandler(this.ServerUI_Load);
            ((System.ComponentModel.ISupportInitialize)(this.netbiosPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.directPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboIPAddress;
        private System.Windows.Forms.RadioButton rbtNetBiosOverTCP;
        private System.Windows.Forms.RadioButton rbtDirectTCPTransport;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblTransport;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.CheckBox chkIntegratedWindowsAuthentication;
        private System.Windows.Forms.CheckBox chkSMB1;
        private System.Windows.Forms.CheckBox chkSMB2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown netbiosPort;
        private System.Windows.Forms.NumericUpDown directPort;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

