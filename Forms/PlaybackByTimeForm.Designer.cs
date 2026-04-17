namespace PlayBackCSharp.Forms
{
    partial class PlaybackByTimeForm
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
            grpSearch = new GroupBox();
            btnSearch = new Button();
            cmbDirection = new ComboBox();
            lblDirection = new Label();
            cmbStreamType = new ComboBox();
            lblStreamType = new Label();
            cmbPlayMode = new ComboBox();
            lblPlayMode = new Label();
            cmbQueryType = new ComboBox();
            lblQueryType = new Label();
            dtpEndTime = new DateTimePicker();
            dtpEndDate = new DateTimePicker();
            lblEndTime = new Label();
            dtpStartTime = new DateTimePicker();
            dtpStartDate = new DateTimePicker();
            lblStartTime = new Label();
            cmbChannel = new ComboBox();
            lblChannel = new Label();
            grpResults = new GroupBox();
            lstRecordFiles = new ListView();
            colFileName = new ColumnHeader();
            colStartTime = new ColumnHeader();
            colEndTime = new ColumnHeader();
            colSize = new ColumnHeader();
            colType = new ColumnHeader();
            grpDownload = new GroupBox();
            lblStatus = new Label();
            progressDownload = new ProgressBar();
            btnStopDownload = new Button();
            btnDownload = new Button();
            btnClose = new Button();
            grpSearch.SuspendLayout();
            grpResults.SuspendLayout();
            grpDownload.SuspendLayout();
            SuspendLayout();
            //
            // grpSearch
            //
            grpSearch.Controls.Add(btnSearch);
            grpSearch.Controls.Add(cmbDirection);
            grpSearch.Controls.Add(lblDirection);
            grpSearch.Controls.Add(cmbStreamType);
            grpSearch.Controls.Add(lblStreamType);
            grpSearch.Controls.Add(cmbPlayMode);
            grpSearch.Controls.Add(lblPlayMode);
            grpSearch.Controls.Add(cmbQueryType);
            grpSearch.Controls.Add(lblQueryType);
            grpSearch.Controls.Add(dtpEndTime);
            grpSearch.Controls.Add(dtpEndDate);
            grpSearch.Controls.Add(lblEndTime);
            grpSearch.Controls.Add(dtpStartTime);
            grpSearch.Controls.Add(dtpStartDate);
            grpSearch.Controls.Add(lblStartTime);
            grpSearch.Controls.Add(cmbChannel);
            grpSearch.Controls.Add(lblChannel);
            grpSearch.Location = new Point(12, 12);
            grpSearch.Name = "grpSearch";
            grpSearch.Size = new Size(760, 140);
            grpSearch.TabIndex = 0;
            grpSearch.TabStop = false;
            grpSearch.Text = "Search Parameters";
            //
            // btnSearch
            //
            btnSearch.Location = new Point(650, 100);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(100, 30);
            btnSearch.TabIndex = 16;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += BtnSearch_Click;
            //
            // cmbDirection
            //
            cmbDirection.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDirection.FormattingEnabled = true;
            cmbDirection.Location = new Point(620, 60);
            cmbDirection.Name = "cmbDirection";
            cmbDirection.Size = new Size(130, 23);
            cmbDirection.TabIndex = 15;
            //
            // lblDirection
            //
            lblDirection.AutoSize = true;
            lblDirection.Location = new Point(550, 63);
            lblDirection.Name = "lblDirection";
            lblDirection.Size = new Size(58, 15);
            lblDirection.TabIndex = 14;
            lblDirection.Text = "Direction:";
            //
            // cmbStreamType
            //
            cmbStreamType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbStreamType.FormattingEnabled = true;
            cmbStreamType.Location = new Point(400, 60);
            cmbStreamType.Name = "cmbStreamType";
            cmbStreamType.Size = new Size(130, 23);
            cmbStreamType.TabIndex = 13;
            //
            // lblStreamType
            //
            lblStreamType.AutoSize = true;
            lblStreamType.Location = new Point(300, 63);
            lblStreamType.Name = "lblStreamType";
            lblStreamType.Size = new Size(75, 15);
            lblStreamType.TabIndex = 12;
            lblStreamType.Text = "Stream Type:";
            //
            // cmbPlayMode
            //
            cmbPlayMode.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPlayMode.FormattingEnabled = true;
            cmbPlayMode.Location = new Point(150, 60);
            cmbPlayMode.Name = "cmbPlayMode";
            cmbPlayMode.Size = new Size(130, 23);
            cmbPlayMode.TabIndex = 11;
            //
            // lblPlayMode
            //
            lblPlayMode.AutoSize = true;
            lblPlayMode.Location = new Point(20, 63);
            lblPlayMode.Name = "lblPlayMode";
            lblPlayMode.Size = new Size(68, 15);
            lblPlayMode.TabIndex = 10;
            lblPlayMode.Text = "Play Mode:";
            //
            // cmbQueryType
            //
            cmbQueryType.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbQueryType.FormattingEnabled = true;
            cmbQueryType.Location = new Point(330, 25);
            cmbQueryType.Name = "cmbQueryType";
            cmbQueryType.Size = new Size(150, 23);
            cmbQueryType.TabIndex = 9;
            //
            // lblQueryType
            //
            lblQueryType.AutoSize = true;
            lblQueryType.Location = new Point(240, 28);
            lblQueryType.Name = "lblQueryType";
            lblQueryType.Size = new Size(72, 15);
            lblQueryType.TabIndex = 8;
            lblQueryType.Text = "Query Type:";
            //
            // dtpEndTime
            //
            dtpEndTime.CustomFormat = "HH:mm:ss";
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.Location = new Point(650, 100);
            dtpEndTime.Name = "dtpEndTime";
            dtpEndTime.ShowUpDown = true;
            dtpEndTime.Size = new Size(90, 23);
            dtpEndTime.TabIndex = 7;
            //
            // dtpEndDate
            //
            dtpEndDate.Format = DateTimePickerFormat.Short;
            dtpEndDate.Location = new Point(530, 100);
            dtpEndDate.Name = "dtpEndDate";
            dtpEndDate.Size = new Size(110, 23);
            dtpEndDate.TabIndex = 6;
            //
            // lblEndTime
            //
            lblEndTime.AutoSize = true;
            lblEndTime.Location = new Point(450, 103);
            lblEndTime.Name = "lblEndTime";
            lblEndTime.Size = new Size(58, 15);
            lblEndTime.TabIndex = 5;
            lblEndTime.Text = "End Time:";
            //
            // dtpStartTime
            //
            dtpStartTime.CustomFormat = "HH:mm:ss";
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            dtpStartTime.Location = new Point(330, 100);
            dtpStartTime.Name = "dtpStartTime";
            dtpStartTime.ShowUpDown = true;
            dtpStartTime.Size = new Size(90, 23);
            dtpStartTime.TabIndex = 4;
            dtpStartTime.Value = DateTime.Now.AddHours(-1);
            //
            // dtpStartDate
            //
            dtpStartDate.Format = DateTimePickerFormat.Short;
            dtpStartDate.Location = new Point(210, 100);
            dtpStartDate.Name = "dtpStartDate";
            dtpStartDate.Size = new Size(110, 23);
            dtpStartDate.TabIndex = 3;
            //
            // lblStartTime
            //
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(20, 103);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(104, 15);
            lblStartTime.TabIndex = 2;
            lblStartTime.Text = "Time Range (Start):";
            //
            // cmbChannel
            //
            cmbChannel.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbChannel.FormattingEnabled = true;
            cmbChannel.Location = new Point(90, 25);
            cmbChannel.Name = "cmbChannel";
            cmbChannel.Size = new Size(120, 23);
            cmbChannel.TabIndex = 1;
            //
            // lblChannel
            //
            lblChannel.AutoSize = true;
            lblChannel.Location = new Point(20, 28);
            lblChannel.Name = "lblChannel";
            lblChannel.Size = new Size(55, 15);
            lblChannel.TabIndex = 0;
            lblChannel.Text = "Channel:";
            //
            // grpResults
            //
            grpResults.Controls.Add(lstRecordFiles);
            grpResults.Location = new Point(12, 158);
            grpResults.Name = "grpResults";
            grpResults.Size = new Size(760, 320);
            grpResults.TabIndex = 1;
            grpResults.TabStop = false;
            grpResults.Text = "Record Files";
            //
            // lstRecordFiles
            //
            lstRecordFiles.Columns.AddRange(new ColumnHeader[] { colFileName, colStartTime, colEndTime, colSize, colType });
            lstRecordFiles.FullRowSelect = true;
            lstRecordFiles.GridLines = true;
            lstRecordFiles.Location = new Point(10, 20);
            lstRecordFiles.MultiSelect = false;
            lstRecordFiles.Name = "lstRecordFiles";
            lstRecordFiles.Size = new Size(740, 290);
            lstRecordFiles.TabIndex = 0;
            lstRecordFiles.UseCompatibleStateImageBehavior = false;
            lstRecordFiles.View = View.Details;
            //
            // colFileName
            //
            colFileName.Text = "File Name";
            colFileName.Width = 250;
            //
            // colStartTime
            //
            colStartTime.Text = "Start Time";
            colStartTime.Width = 150;
            //
            // colEndTime
            //
            colEndTime.Text = "End Time";
            colEndTime.Width = 150;
            //
            // colSize
            //
            colSize.Text = "Size";
            colSize.Width = 100;
            //
            // colType
            //
            colType.Text = "Type";
            colType.Width = 80;
            //
            // grpDownload
            //
            grpDownload.Controls.Add(lblStatus);
            grpDownload.Controls.Add(progressDownload);
            grpDownload.Controls.Add(btnStopDownload);
            grpDownload.Controls.Add(btnDownload);
            grpDownload.Location = new Point(12, 484);
            grpDownload.Name = "grpDownload";
            grpDownload.Size = new Size(650, 90);
            grpDownload.TabIndex = 2;
            grpDownload.TabStop = false;
            grpDownload.Text = "Download";
            //
            // lblStatus
            //
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(15, 65);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(39, 15);
            lblStatus.TabIndex = 3;
            lblStatus.Text = "Ready";
            //
            // progressDownload
            //
            progressDownload.Location = new Point(15, 30);
            progressDownload.Name = "progressDownload";
            progressDownload.Size = new Size(620, 25);
            progressDownload.TabIndex = 2;
            //
            // btnStopDownload
            //
            btnStopDownload.Enabled = false;
            btnStopDownload.Location = new Point(530, 55);
            btnStopDownload.Name = "btnStopDownload";
            btnStopDownload.Size = new Size(100, 30);
            btnStopDownload.TabIndex = 1;
            btnStopDownload.Text = "Stop";
            btnStopDownload.UseVisualStyleBackColor = true;
            btnStopDownload.Click += BtnStopDownload_Click;
            //
            // btnDownload
            //
            btnDownload.Location = new Point(420, 55);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(100, 30);
            btnDownload.TabIndex = 0;
            btnDownload.Text = "Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += BtnDownload_Click;
            //
            // btnClose
            //
            btnClose.Location = new Point(672, 539);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(100, 35);
            btnClose.TabIndex = 3;
            btnClose.Text = "Close";
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += BtnClose_Click;
            //
            // PlaybackByTimeForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 586);
            Controls.Add(btnClose);
            Controls.Add(grpDownload);
            Controls.Add(grpResults);
            Controls.Add(grpSearch);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "PlaybackByTimeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Playback by Time";
            grpSearch.ResumeLayout(false);
            grpSearch.PerformLayout();
            grpResults.ResumeLayout(false);
            grpDownload.ResumeLayout(false);
            grpDownload.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private GroupBox grpSearch;
        private Label lblChannel;
        private ComboBox cmbChannel;
        private Label lblStartTime;
        private DateTimePicker dtpStartDate;
        private DateTimePicker dtpStartTime;
        private Label lblEndTime;
        private DateTimePicker dtpEndDate;
        private DateTimePicker dtpEndTime;
        private Label lblQueryType;
        private ComboBox cmbQueryType;
        private Label lblPlayMode;
        private ComboBox cmbPlayMode;
        private Label lblStreamType;
        private ComboBox cmbStreamType;
        private Label lblDirection;
        private ComboBox cmbDirection;
        private Button btnSearch;
        private GroupBox grpResults;
        private ListView lstRecordFiles;
        private ColumnHeader colFileName;
        private ColumnHeader colStartTime;
        private ColumnHeader colEndTime;
        private ColumnHeader colSize;
        private ColumnHeader colType;
        private GroupBox grpDownload;
        private Button btnDownload;
        private Button btnStopDownload;
        private ProgressBar progressDownload;
        private Label lblStatus;
        private Button btnClose;
    }
}
