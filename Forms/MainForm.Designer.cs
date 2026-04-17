namespace PlayBackCSharp.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            grpConnection = new GroupBox();
            btnLogout = new Button();
            btnLogin = new Button();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            numPort = new NumericUpDown();
            lblPort = new Label();
            txtIP4 = new TextBox();
            txtIP3 = new TextBox();
            txtIP2 = new TextBox();
            txtIP1 = new TextBox();
            lblIP = new Label();
            grpPlayback = new GroupBox();
            cmbDirection = new ComboBox();
            lblDirection = new Label();
            chkServerMode = new CheckBox();
            btnCapture = new Button();
            btnAdvanced = new Button();
            lblPosition = new Label();
            trackPosition = new TrackBar();
            grpSpeed = new GroupBox();
            lblSpeed = new Label();
            btnNormalSpeed = new Button();
            btnSpeedUp = new Button();
            btnSlowDown = new Button();
            btnPause = new Button();
            btnStop = new Button();
            btnPlay = new Button();
            dtpEndTime = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            lblEndTime = new Label();
            dtpStartTime = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            lblStartTime = new Label();
            cmbChannel = new ComboBox();
            lblChannel = new Label();
            panelVideo = new Panel();
            grpConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            grpPlayback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackPosition).BeginInit();
            grpSpeed.SuspendLayout();
            SuspendLayout();
            // 
            // grpConnection
            // 
            grpConnection.Controls.Add(btnLogout);
            grpConnection.Controls.Add(btnLogin);
            grpConnection.Controls.Add(txtPassword);
            grpConnection.Controls.Add(lblPassword);
            grpConnection.Controls.Add(txtUsername);
            grpConnection.Controls.Add(lblUsername);
            grpConnection.Controls.Add(numPort);
            grpConnection.Controls.Add(lblPort);
            grpConnection.Controls.Add(txtIP4);
            grpConnection.Controls.Add(txtIP3);
            grpConnection.Controls.Add(txtIP2);
            grpConnection.Controls.Add(txtIP1);
            grpConnection.Controls.Add(lblIP);
            grpConnection.Location = new Point(22, 26);
            grpConnection.Margin = new Padding(6);
            grpConnection.Name = "grpConnection";
            grpConnection.Padding = new Padding(6);
            grpConnection.Size = new Size(1411, 213);
            grpConnection.TabIndex = 0;
            grpConnection.TabStop = false;
            grpConnection.Text = "Connection";
            // 
            // btnLogout
            // 
            btnLogout.Enabled = false;
            btnLogout.Location = new Point(1226, 128);
            btnLogout.Margin = new Padding(6);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(167, 64);
            btnLogout.TabIndex = 12;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(1226, 51);
            btnLogin.Margin = new Padding(6);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(167, 64);
            btnLogin.TabIndex = 11;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(910, 128);
            txtPassword.Margin = new Padding(6);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '*';
            txtPassword.Size = new Size(275, 39);
            txtPassword.TabIndex = 10;
            txtPassword.Text = "admin1234";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(780, 134);
            lblPassword.Margin = new Padding(6, 0, 6, 0);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(116, 32);
            lblPassword.TabIndex = 9;
            lblPassword.Text = "Password:";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(910, 58);
            txtUsername.Margin = new Padding(6);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(275, 39);
            txtUsername.TabIndex = 8;
            txtUsername.Text = "admin";
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(780, 64);
            lblUsername.Margin = new Padding(6, 0, 6, 0);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(126, 32);
            lblUsername.TabIndex = 7;
            lblUsername.Text = "Username:";
            // 
            // numPort
            // 
            numPort.Location = new Point(501, 128);
            numPort.Margin = new Padding(6);
            numPort.Maximum = new decimal(new int[] { 65535, 0, 0, 0 });
            numPort.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numPort.Name = "numPort";
            numPort.Size = new Size(223, 39);
            numPort.TabIndex = 6;
            numPort.Value = new decimal(new int[] { 37777, 0, 0, 0 });
            // 
            // lblPort
            // 
            lblPort.AutoSize = true;
            lblPort.Location = new Point(427, 134);
            lblPort.Margin = new Padding(6, 0, 6, 0);
            lblPort.Name = "lblPort";
            lblPort.Size = new Size(61, 32);
            lblPort.TabIndex = 5;
            lblPort.Text = "Port:";
            // 
            // txtIP4
            // 
            txtIP4.Location = new Point(613, 58);
            txtIP4.Margin = new Padding(6);
            txtIP4.MaxLength = 3;
            txtIP4.Name = "txtIP4";
            txtIP4.Size = new Size(62, 39);
            txtIP4.TabIndex = 4;
            txtIP4.Text = "146";
            txtIP4.TextAlign = HorizontalAlignment.Center;
            // 
            // txtIP3
            // 
            txtIP3.Location = new Point(529, 58);
            txtIP3.Margin = new Padding(6);
            txtIP3.MaxLength = 3;
            txtIP3.Name = "txtIP3";
            txtIP3.Size = new Size(62, 39);
            txtIP3.TabIndex = 3;
            txtIP3.Text = "12";
            txtIP3.TextAlign = HorizontalAlignment.Center;
            // 
            // txtIP2
            // 
            txtIP2.Location = new Point(446, 58);
            txtIP2.Margin = new Padding(6);
            txtIP2.MaxLength = 3;
            txtIP2.Name = "txtIP2";
            txtIP2.Size = new Size(62, 39);
            txtIP2.TabIndex = 2;
            txtIP2.Text = "23";
            txtIP2.TextAlign = HorizontalAlignment.Center;
            // 
            // txtIP1
            // 
            txtIP1.Location = new Point(362, 58);
            txtIP1.Margin = new Padding(6);
            txtIP1.MaxLength = 3;
            txtIP1.Name = "txtIP1";
            txtIP1.Size = new Size(62, 39);
            txtIP1.TabIndex = 1;
            txtIP1.Text = "172";
            txtIP1.TextAlign = HorizontalAlignment.Center;
            // 
            // lblIP
            // 
            lblIP.AutoSize = true;
            lblIP.Location = new Point(37, 64);
            lblIP.Margin = new Padding(6, 0, 6, 0);
            lblIP.Name = "lblIP";
            lblIP.Size = new Size(117, 32);
            lblIP.TabIndex = 0;
            lblIP.Text = "Device IP:";
            // 
            // grpPlayback
            // 
            grpPlayback.Controls.Add(cmbDirection);
            grpPlayback.Controls.Add(lblDirection);
            grpPlayback.Controls.Add(chkServerMode);
            grpPlayback.Controls.Add(btnCapture);
            grpPlayback.Controls.Add(btnAdvanced);
            grpPlayback.Controls.Add(lblPosition);
            grpPlayback.Controls.Add(trackPosition);
            grpPlayback.Controls.Add(grpSpeed);
            grpPlayback.Controls.Add(btnPause);
            grpPlayback.Controls.Add(btnStop);
            grpPlayback.Controls.Add(btnPlay);
            grpPlayback.Controls.Add(dtpEndTime);
            grpPlayback.Controls.Add(dtpEndDate);
            grpPlayback.Controls.Add(lblEndTime);
            grpPlayback.Controls.Add(dtpStartTime);
            grpPlayback.Controls.Add(dtpStartDate);
            grpPlayback.Controls.Add(lblStartTime);
            grpPlayback.Controls.Add(cmbChannel);
            grpPlayback.Controls.Add(lblChannel);
            grpPlayback.Enabled = false;
            grpPlayback.Location = new Point(22, 252);
            grpPlayback.Margin = new Padding(6);
            grpPlayback.Name = "grpPlayback";
            grpPlayback.Padding = new Padding(6);
            grpPlayback.Size = new Size(1411, 427);
            grpPlayback.TabIndex = 1;
            grpPlayback.TabStop = false;
            grpPlayback.Text = "Playback Control";
            // 
            // cmbDirection
            // 
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.FormattingEnabled = true;
            cmbDirection.Items.AddRange(new object[] { "Forward", "Backward" });
            cmbDirection.Location = new Point(631, 55);
            cmbDirection.Margin = new Padding(6);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(182, 40);
            cmbDirection.TabIndex = 17;
            // 
            // lblDirection
            // 
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(501, 62);
            lblDirection.Margin = new Padding(6, 0, 6, 0);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(116, 32);
            lblDirection.TabIndex = 16;
            lblDirection.Text = "Direction:";
            // 
            // chkServerMode
            // 
            chkServerMode.AutoSize = true;
            chkServerMode.Location = new Point(873, 60);
            chkServerMode.Margin = new Padding(6);
            chkServerMode.Name = "chkServerMode";
            chkServerMode.Size = new Size(183, 36);
            chkServerMode.TabIndex = 15;
            chkServerMode.Text = "Server Mode";
            chkServerMode.UseVisualStyleBackColor = true;
            // 
            // btnCapture
            // 
            btnCapture.Location = new Point(1226, 192);
            btnCapture.Margin = new Padding(6);
            btnCapture.Name = "btnCapture";
            btnCapture.Size = new Size(167, 64);
            btnCapture.TabIndex = 14;
            btnCapture.Text = "Capture";
            btnCapture.UseVisualStyleBackColor = true;
            btnCapture.Click += BtnCapture_Click;
            // 
            // btnAdvanced
            // 
            btnAdvanced.Location = new Point(1226, 117);
            btnAdvanced.Margin = new Padding(6);
            btnAdvanced.Name = "btnAdvanced";
            btnAdvanced.Size = new Size(167, 64);
            btnAdvanced.TabIndex = 18;
            btnAdvanced.Text = "Advanced...";
            btnAdvanced.UseVisualStyleBackColor = true;
            btnAdvanced.Click += BtnAdvanced_Click;
            // 
            // lblPosition
            // 
            lblPosition.AutoSize = true;
            lblPosition.Location = new Point(37, 363);
            lblPosition.Margin = new Padding(6, 0, 6, 0);
            lblPosition.Name = "lblPosition";
            lblPosition.Size = new Size(213, 32);
            lblPosition.TabIndex = 13;
            lblPosition.Text = "00:00:00 / 00:00:00";
            // 
            // trackPosition
            // 
            trackPosition.Location = new Point(37, 267);
            trackPosition.Margin = new Padding(6);
            trackPosition.Maximum = 100;
            trackPosition.Name = "trackPosition";
            trackPosition.Size = new Size(1356, 90);
            trackPosition.TabIndex = 12;
            trackPosition.TickFrequency = 10;
            trackPosition.Scroll += TrackPosition_Scroll;
            // 
            // grpSpeed
            // 
            grpSpeed.Controls.Add(lblSpeed);
            grpSpeed.Controls.Add(btnNormalSpeed);
            grpSpeed.Controls.Add(btnSpeedUp);
            grpSpeed.Controls.Add(btnSlowDown);
            grpSpeed.Enabled = false;
            grpSpeed.Location = new Point(631, 117);
            grpSpeed.Margin = new Padding(6);
            grpSpeed.Name = "grpSpeed";
            grpSpeed.Padding = new Padding(6);
            grpSpeed.Size = new Size(557, 139);
            grpSpeed.TabIndex = 11;
            grpSpeed.TabStop = false;
            grpSpeed.Text = "Speed Control";
            // 
            // lblSpeed
            // 
            lblSpeed.AutoSize = true;
            lblSpeed.Location = new Point(223, 96);
            lblSpeed.Margin = new Padding(6, 0, 6, 0);
            lblSpeed.Name = "lblSpeed";
            lblSpeed.Size = new Size(148, 32);
            lblSpeed.TabIndex = 3;
            lblSpeed.Text = "Speed: 1.00x";
            // 
            // btnNormalSpeed
            // 
            btnNormalSpeed.Location = new Point(371, 32);
            btnNormalSpeed.Margin = new Padding(6);
            btnNormalSpeed.Name = "btnNormalSpeed";
            btnNormalSpeed.Size = new Size(167, 53);
            btnNormalSpeed.TabIndex = 2;
            btnNormalSpeed.Text = "Normal (1x)";
            btnNormalSpeed.UseVisualStyleBackColor = true;
            btnNormalSpeed.Click += BtnNormalSpeed_Click;
            // 
            // btnSpeedUp
            // 
            btnSpeedUp.Location = new Point(195, 32);
            btnSpeedUp.Margin = new Padding(6);
            btnSpeedUp.Name = "btnSpeedUp";
            btnSpeedUp.Size = new Size(167, 53);
            btnSpeedUp.TabIndex = 1;
            btnSpeedUp.Text = "Speed Up";
            btnSpeedUp.UseVisualStyleBackColor = true;
            btnSpeedUp.Click += BtnSpeedUp_Click;
            // 
            // btnSlowDown
            // 
            btnSlowDown.Location = new Point(19, 32);
            btnSlowDown.Margin = new Padding(6);
            btnSlowDown.Name = "btnSlowDown";
            btnSlowDown.Size = new Size(167, 53);
            btnSlowDown.TabIndex = 0;
            btnSlowDown.Text = "Slow Down";
            btnSlowDown.UseVisualStyleBackColor = true;
            btnSlowDown.Click += BtnSlowDown_Click;
            // 
            // btnPause
            // 
            btnPause.Enabled = false;
            btnPause.Location = new Point(223, 192);
            btnPause.Margin = new Padding(6);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(167, 64);
            btnPause.TabIndex = 10;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += BtnPause_Click;
            // 
            // btnStop
            // 
            btnStop.Enabled = false;
            btnStop.Location = new Point(409, 192);
            btnStop.Margin = new Padding(6);
            btnStop.Name = "btnStop";
            btnStop.Size = new Size(167, 64);
            btnStop.TabIndex = 9;
            btnStop.Text = "Stop";
            btnStop.UseVisualStyleBackColor = true;
            btnStop.Click += BtnStop_Click;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(37, 192);
            btnPlay.Margin = new Padding(6);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(167, 64);
            btnPlay.TabIndex = 8;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += BtnPlay_Click;
            // 
            // dtpEndTime
            // 
            dtpEndTime.CustomFormat = "HH:mm:ss";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(1096, 128);
            dtpEndTime.Margin = new Padding(6);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.ShowUpDown = true;
            dtpEndTime.Size = new Size(164, 39);
            dtpEndTime.TabIndex = 7;
            // 
            // dtpEndDate
            // 
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(873, 128);
            dtpEndDate.Margin = new Padding(6);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(201, 39);
            dtpEndDate.TabIndex = 6;
            // 
            // lblEndTime
            // 
            lblEndTime.AutoSize = true;
            lblEndTime.Location = new Point(743, 134);
            lblEndTime.Margin = new Padding(6, 0, 6, 0);
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new Size(119, 32);
            lblEndTime.TabIndex = 5;
            lblEndTime.Text = "End Time:";
            // 
            // dtpStartTime
            // 
            dtpStartTime.CustomFormat = "HH:mm:ss";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(464, 128);
            dtpStartTime.Margin = new Padding(6);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.Size = new Size(164, 39);
            dtpStartTime.TabIndex = 4;
            dtpStartTime.Value = new DateTime(2025, 12, 12, 13, 38, 27, 130);
            // 
            // dtpStartDate
            // 
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(241, 128);
            dtpStartDate.Margin = new Padding(6);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(201, 39);
            dtpStartDate.TabIndex = 3;
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(37, 134);
            lblStartTime.Margin = new Padding(6, 0, 6, 0);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(127, 32);
            lblStartTime.TabIndex = 2;
            lblStartTime.Text = "Start Time:";
            // 
            // cmbChannel
            // 
            cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(241, 55);
            cmbChannel.Margin = new Padding(6);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(219, 40);
            cmbChannel.TabIndex = 1;
            // 
            // lblChannel
            // 
            lblChannel.AutoSize = true;
            lblChannel.Location = new Point(37, 62);
            lblChannel.Margin = new Padding(6, 0, 6, 0);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(107, 32);
            lblChannel.TabIndex = 0;
            lblChannel.Text = "Channel:";
            // 
            // panelVideo
            // 
            panelVideo.BackColor = Color.Black;
            panelVideo.BorderStyle = BorderStyle.FixedSingle;
            panelVideo.Location = new Point(22, 691);
            panelVideo.Margin = new Padding(6);
            panelVideo.Name = "panelVideo";
            panelVideo.Size = new Size(1410, 911);
            panelVideo.TabIndex = 2;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1456, 1623);
            Controls.Add(panelVideo);
            Controls.Add(grpPlayback);
            Controls.Add(grpConnection);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6);
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dahua Playback Demo";
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            grpPlayback.ResumeLayout(false);
            grpPlayback.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackPosition).EndInit();
            grpSpeed.ResumeLayout(false);
            grpSpeed.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpConnection;
        private Label lblIP;
        private TextBox txtIP1;
        private TextBox txtIP2;
        private TextBox txtIP3;
        private TextBox txtIP4;
        private Label lblPort;
        private NumericUpDown numPort;
        private Label lblUsername;
        private TextBox txtUsername;
        private Label lblPassword;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnLogout;
        private GroupBox grpPlayback;
        private Label lblChannel;
        private ComboBox cmbChannel;
        private Label lblStartTime;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpStartTime;
        private Label lblEndTime;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpEndTime;
        private Button btnPlay;
        private Button btnStop;
        private Button btnPause;
        private GroupBox grpSpeed;
        private Button btnSlowDown;
        private Button btnSpeedUp;
        private Button btnNormalSpeed;
        private Label lblSpeed;
        private TrackBar trackPosition;
        private Label lblPosition;
        private Button btnCapture;
        private CheckBox chkServerMode;
        private ComboBox cmbDirection;
        private Label lblDirection;
        private Panel panelVideo;
        private Button btnAdvanced;
    }
}
