namespace PlayBackCSharp.Forms
{
    partial class TimelapseForm
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
            btnLogin = new Button();
            grpTimelapse = new GroupBox();
            btnSettings = new Button();
            lblExportInfo = new Label();
            lblProgressInfo = new Label();
            lblVidFPS = new Label();
            numericUpDownVidFPS = new NumericUpDown();
            lblVidL = new Label();
            numericUpDownVidL = new NumericUpDown();
            progressBarExport = new ProgressBar();
            btnExport = new Button();
            lblFrameInterval = new Label();
            numericUpDownFrameInterval = new NumericUpDown();
            panelTimelapsePreview = new Panel();
            lblPreviewTime = new Label();
            trackBarPreview = new TrackBar();
            pictureBoxPreview = new PictureBox();
            radioButtonTimelapseVid = new RadioButton();
            radioButtonTimelapseImg = new RadioButton();
            dateTimePickerEnd = new DateTimePicker();
            lblTimelapseEnd = new Label();
            dateTimePickerStart = new DateTimePicker();
            lblTimelapseStart = new Label();
            grpConnection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).BeginInit();
            grpTimelapse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVidFPS).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVidL).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFrameInterval).BeginInit();
            panelTimelapsePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPreview).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).BeginInit();
            SuspendLayout();
            // 
            // grpConnection
            // 
            grpConnection.Controls.Add(btnLogout);
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
            grpConnection.Controls.Add(btnLogin);
            grpConnection.Location = new Point(22, 26);
            grpConnection.Margin = new Padding(6);
            grpConnection.Name = "grpConnection";
            grpConnection.Padding = new Padding(6);
            grpConnection.Size = new Size(1411, 277);
            grpConnection.TabIndex = 0;
            grpConnection.TabStop = false;
            grpConnection.Text = "Connection";
            // 
            // btnLogout
            // 
            btnLogout.Enabled = false;
            btnLogout.Location = new Point(1207, 181);
            btnLogout.Margin = new Padding(6);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(186, 64);
            btnLogout.TabIndex = 12;
            btnLogout.Text = "Logout";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += BtnLogout_Click;
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
            // btnLogin
            // 
            btnLogin.Location = new Point(1207, 51);
            btnLogin.Margin = new Padding(6);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(186, 64);
            btnLogin.TabIndex = 11;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += BtnLogin_Click;
            // 
            // grpTimelapse
            // 
            grpTimelapse.Controls.Add(btnSettings);
            grpTimelapse.Controls.Add(lblExportInfo);
            grpTimelapse.Controls.Add(lblProgressInfo);
            grpTimelapse.Controls.Add(lblVidFPS);
            grpTimelapse.Controls.Add(numericUpDownVidFPS);
            grpTimelapse.Controls.Add(lblVidL);
            grpTimelapse.Controls.Add(numericUpDownVidL);
            grpTimelapse.Controls.Add(progressBarExport);
            grpTimelapse.Controls.Add(btnExport);
            grpTimelapse.Controls.Add(lblFrameInterval);
            grpTimelapse.Controls.Add(numericUpDownFrameInterval);
            grpTimelapse.Controls.Add(panelTimelapsePreview);
            grpTimelapse.Controls.Add(radioButtonTimelapseVid);
            grpTimelapse.Controls.Add(radioButtonTimelapseImg);
            grpTimelapse.Controls.Add(dateTimePickerEnd);
            grpTimelapse.Controls.Add(lblTimelapseEnd);
            grpTimelapse.Controls.Add(dateTimePickerStart);
            grpTimelapse.Controls.Add(lblTimelapseStart);
            grpTimelapse.Enabled = false;
            grpTimelapse.Location = new Point(22, 316);
            grpTimelapse.Margin = new Padding(6);
            grpTimelapse.Name = "grpTimelapse";
            grpTimelapse.Padding = new Padding(6);
            grpTimelapse.Size = new Size(1411, 619);
            grpTimelapse.TabIndex = 1;
            grpTimelapse.TabStop = false;
            grpTimelapse.Text = "Timelapse Control";
            // 
            // btnSettings
            // 
            btnSettings.Location = new Point(375, 404);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new Size(216, 84);
            btnSettings.TabIndex = 17;
            btnSettings.Text = "Export settings";
            btnSettings.UseVisualStyleBackColor = true;
            // 
            // lblExportInfo
            // 
            lblExportInfo.Location = new Point(224, 507);
            lblExportInfo.Name = "lblExportInfo";
            lblExportInfo.Size = new Size(500, 32);
            lblExportInfo.TabIndex = 16;
            lblExportInfo.Text = "Info about export";
            lblExportInfo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblProgressInfo
            // 
            lblProgressInfo.AutoSize = true;
            lblProgressInfo.Location = new Point(328, 507);
            lblProgressInfo.Name = "lblProgressInfo";
            lblProgressInfo.Size = new Size(0, 32);
            lblProgressInfo.TabIndex = 15;
            // 
            // lblVidFPS
            // 
            lblVidFPS.AutoSize = true;
            lblVidFPS.Location = new Point(37, 414);
            lblVidFPS.Name = "lblVidFPS";
            lblVidFPS.Size = new Size(121, 32);
            lblVidFPS.TabIndex = 14;
            lblVidFPS.Text = "Video FPS";
            lblVidFPS.Visible = false;
            // 
            // numericUpDownVidFPS
            // 
            numericUpDownVidFPS.Location = new Point(34, 449);
            numericUpDownVidFPS.Name = "numericUpDownVidFPS";
            numericUpDownVidFPS.Size = new Size(240, 39);
            numericUpDownVidFPS.TabIndex = 13;
            numericUpDownVidFPS.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownVidFPS.Visible = false;
            // 
            // lblVidL
            // 
            lblVidL.AutoSize = true;
            lblVidL.Location = new Point(383, 291);
            lblVidL.Name = "lblVidL";
            lblVidL.Size = new Size(152, 32);
            lblVidL.TabIndex = 12;
            lblVidL.Text = "Video length";
            lblVidL.Visible = false;
            // 
            // numericUpDownVidL
            // 
            numericUpDownVidL.Location = new Point(380, 326);
            numericUpDownVidL.Name = "numericUpDownVidL";
            numericUpDownVidL.ReadOnly = true;
            numericUpDownVidL.Size = new Size(240, 39);
            numericUpDownVidL.TabIndex = 11;
            numericUpDownVidL.Value = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownVidL.Visible = false;
            // 
            // progressBarExport
            // 
            progressBarExport.Location = new Point(224, 551);
            progressBarExport.Name = "progressBarExport";
            progressBarExport.Size = new Size(500, 46);
            progressBarExport.TabIndex = 10;
            // 
            // btnExport
            // 
            btnExport.Font = new Font("Segoe UI", 13.875F, FontStyle.Bold, GraphicsUnit.Point, 238);
            btnExport.Location = new Point(9, 530);
            btnExport.Name = "btnExport";
            btnExport.Size = new Size(185, 80);
            btnExport.TabIndex = 9;
            btnExport.Text = "Export";
            btnExport.UseVisualStyleBackColor = true;
            btnExport.Click += BtnExport_Click;
            // 
            // lblFrameInterval
            // 
            lblFrameInterval.AutoSize = true;
            lblFrameInterval.Location = new Point(37, 291);
            lblFrameInterval.Name = "lblFrameInterval";
            lblFrameInterval.Size = new Size(208, 32);
            lblFrameInterval.TabIndex = 8;
            lblFrameInterval.Text = "Timelapse interval";
            // 
            // numericUpDownFrameInterval
            // 
            numericUpDownFrameInterval.Location = new Point(37, 326);
            numericUpDownFrameInterval.Name = "numericUpDownFrameInterval";
            numericUpDownFrameInterval.Size = new Size(240, 39);
            numericUpDownFrameInterval.TabIndex = 7;
            numericUpDownFrameInterval.Value = new decimal(new int[] { 30, 0, 0, 0 });
            // 
            // panelTimelapsePreview
            // 
            panelTimelapsePreview.Controls.Add(lblPreviewTime);
            panelTimelapsePreview.Controls.Add(trackBarPreview);
            panelTimelapsePreview.Controls.Add(pictureBoxPreview);
            panelTimelapsePreview.Location = new Point(780, 64);
            panelTimelapsePreview.Name = "panelTimelapsePreview";
            panelTimelapsePreview.Size = new Size(613, 522);
            panelTimelapsePreview.TabIndex = 6;
            // 
            // lblPreviewTime
            // 
            lblPreviewTime.AutoSize = true;
            lblPreviewTime.Location = new Point(233, 488);
            lblPreviewTime.Name = "lblPreviewTime";
            lblPreviewTime.Size = new Size(0, 32);
            lblPreviewTime.TabIndex = 2;
            // 
            // trackBarPreview
            // 
            trackBarPreview.Location = new Point(3, 465);
            trackBarPreview.Maximum = 100;
            trackBarPreview.Name = "trackBarPreview";
            trackBarPreview.Size = new Size(607, 90);
            trackBarPreview.TabIndex = 1;
            trackBarPreview.TickFrequency = 10;
            // 
            // pictureBoxPreview
            // 
            pictureBoxPreview.BackColor = Color.Black;
            pictureBoxPreview.BorderStyle = BorderStyle.FixedSingle;
            pictureBoxPreview.Location = new Point(3, 3);
            pictureBoxPreview.Name = "pictureBoxPreview";
            pictureBoxPreview.Size = new Size(607, 456);
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPreview.TabIndex = 0;
            pictureBoxPreview.TabStop = false;
            // 
            // radioButtonTimelapseVid
            // 
            radioButtonTimelapseVid.AutoSize = true;
            radioButtonTimelapseVid.Location = new Point(479, 208);
            radioButtonTimelapseVid.Name = "radioButtonTimelapseVid";
            radioButtonTimelapseVid.Size = new Size(182, 36);
            radioButtonTimelapseVid.TabIndex = 5;
            radioButtonTimelapseVid.TabStop = true;
            radioButtonTimelapseVid.Text = "Video export";
            radioButtonTimelapseVid.UseVisualStyleBackColor = true;
            // 
            // radioButtonTimelapseImg
            // 
            radioButtonTimelapseImg.AutoSize = true;
            radioButtonTimelapseImg.Location = new Point(479, 102);
            radioButtonTimelapseImg.Name = "radioButtonTimelapseImg";
            radioButtonTimelapseImg.Size = new Size(186, 36);
            radioButtonTimelapseImg.TabIndex = 4;
            radioButtonTimelapseImg.TabStop = true;
            radioButtonTimelapseImg.Text = "Image export";
            radioButtonTimelapseImg.UseVisualStyleBackColor = true;
            // 
            // dateTimePickerEnd
            // 
            dateTimePickerEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePickerEnd.Format = DateTimePickerFormat.Custom;
            dateTimePickerEnd.Location = new Point(37, 205);
            dateTimePickerEnd.Name = "dateTimePickerEnd";
            dateTimePickerEnd.Size = new Size(400, 39);
            dateTimePickerEnd.TabIndex = 3;
            // 
            // lblTimelapseEnd
            // 
            lblTimelapseEnd.AutoSize = true;
            lblTimelapseEnd.Location = new Point(195, 170);
            lblTimelapseEnd.Margin = new Padding(6, 0, 6, 0);
            lblTimelapseEnd.Name = "lblTimelapseEnd";
            lblTimelapseEnd.Size = new Size(54, 32);
            lblTimelapseEnd.TabIndex = 2;
            lblTimelapseEnd.Text = "End";
            // 
            // dateTimePickerStart
            // 
            dateTimePickerStart.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            dateTimePickerStart.Format = DateTimePickerFormat.Custom;
            dateTimePickerStart.Location = new Point(37, 99);
            dateTimePickerStart.Name = "dateTimePickerStart";
            dateTimePickerStart.Size = new Size(400, 39);
            dateTimePickerStart.TabIndex = 1;
            // 
            // lblTimelapseStart
            // 
            lblTimelapseStart.AutoSize = true;
            lblTimelapseStart.Location = new Point(195, 64);
            lblTimelapseStart.Margin = new Padding(6, 0, 6, 0);
            lblTimelapseStart.Name = "lblTimelapseStart";
            lblTimelapseStart.Size = new Size(62, 32);
            lblTimelapseStart.TabIndex = 0;
            lblTimelapseStart.Text = "Start";
            // 
            // TimelapseForm
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1456, 960);
            Controls.Add(grpTimelapse);
            Controls.Add(grpConnection);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(6);
            MaximizeBox = false;
            Name = "TimelapseForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dahua Timelapse";
            grpConnection.ResumeLayout(false);
            grpConnection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numPort).EndInit();
            grpTimelapse.ResumeLayout(false);
            grpTimelapse.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVidFPS).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownVidL).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDownFrameInterval).EndInit();
            panelTimelapsePreview.ResumeLayout(false);
            panelTimelapsePreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)trackBarPreview).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPreview).EndInit();
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
        private GroupBox grpTimelapse;
        private Label lblTimelapseStart;
        private DateTimePicker dateTimePickerStart;
        private DateTimePicker dateTimePickerEnd;
        private Label lblTimelapseEnd;
        private Panel panelTimelapsePreview;
        private PictureBox pictureBoxPreview;
        private TrackBar trackBarPreview;
        private Label lblPreviewTime;
        private RadioButton radioButtonTimelapseVid;
        private RadioButton radioButtonTimelapseImg;
        private NumericUpDown numericUpDownFrameInterval;
        private Button btnExport;
        private Label lblFrameInterval;
        private ProgressBar progressBarExport;
        private Label lblVidL;
        private NumericUpDown numericUpDownVidL;
        private Label lblProgressInfo;
        private Label lblVidFPS;
        private NumericUpDown numericUpDownVidFPS;
        private Label lblExportInfo;
        private Button btnSettings;
    }
}
