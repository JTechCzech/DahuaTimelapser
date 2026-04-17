using PlayBackCSharp.SDK;
using System.Runtime.InteropServices;

namespace PlayBackCSharp.Forms
{
    public partial class PlaybackByTimeForm : Form
    {
        #region Fields

        private IntPtr _loginID = IntPtr.Zero;
        private IntPtr _downloadHandle = IntPtr.Zero;
        private NetSDK.fTimeDownLoadPosCallBack? _downloadCallback;

        #endregion

        #region Constructor

        public PlaybackByTimeForm(IntPtr loginID, int channelCount)
        {
            InitializeComponent();
            _loginID = loginID;
            InitializeChannels(channelCount);
        }

        #endregion

        #region Initialization

        private void InitializeChannels(int channelCount)
        {
            cmbChannel.Items.Clear();
            for (int i = 0; i < channelCount; i++)
            {
                cmbChannel.Items.Add($"Channel {i}");
            }
            if (cmbChannel.Items.Count > 0)
                cmbChannel.SelectedIndex = 0;

            // Initialize query type combo
            cmbQueryType.Items.AddRange(new object[] { "All", "Alarm", "Motion Detection" });
            cmbQueryType.SelectedIndex = 0;

            // Initialize play mode combo
            cmbPlayMode.Items.AddRange(new object[] { "Direct Mode", "Server Mode" });
            cmbPlayMode.SelectedIndex = 0;

            // Initialize stream type combo
            cmbStreamType.Items.AddRange(new object[] { "Both", "Main Stream", "Extra Stream" });
            cmbStreamType.SelectedIndex = 1;

            // Initialize direction combo
            cmbDirection.Items.AddRange(new object[] { "Forward", "Backward" });
            cmbDirection.SelectedIndex = 0;
        }

        #endregion

        #region Event Handlers

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (_loginID == IntPtr.Zero)
            {
                MessageBox.Show("Not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                int channel = cmbChannel.SelectedIndex;
                int recordType = cmbQueryType.SelectedIndex;

                NET_TIME startTime = NET_TIME.FromDateTime(dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay);
                NET_TIME endTime = NET_TIME.FromDateTime(dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay);

                // Clear list
                lstRecordFiles.Items.Clear();

                // Search for files
                IntPtr findHandle = NetSDK.CLIENT_FindFile(
                    _loginID,
                    channel,
                    recordType,
                    IntPtr.Zero,        // no card ID
                    ref startTime,
                    ref endTime,
                    0,                  // bTime = 0 (search by time)
                    5000);              // wait 5 seconds

                if (findHandle == IntPtr.Zero)
                {
                    uint error = NetSDK.CLIENT_GetLastError();
                    MessageBox.Show($"Search failed: {NetSDK.GetErrorMessage(error)}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Retrieve files
                int count = 0;
                while (true)
                {
                    int result = NetSDK.CLIENT_FindNextFile(findHandle, out NET_RECORDFILE_INFO fileInfo);

                    if (result == 0)
                        break; // No more files

                    if (result < 0)
                    {
                        uint error = NetSDK.CLIENT_GetLastError();
                        if (error != 0)
                        {
                            MessageBox.Show($"Error reading files: {NetSDK.GetErrorMessage(error)}", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        break;
                    }

                    // Add to list
                    string recordTypeStr = fileInfo.nRecordFileType switch
                    {
                        0 => "Normal",
                        1 => "Alarm",
                        2 => "Motion",
                        _ => "Unknown"
                    };

                    ListViewItem item = new ListViewItem(fileInfo.FileName);
                    item.SubItems.Add(fileInfo.starttime.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add(fileInfo.endtime.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss"));
                    item.SubItems.Add($"{fileInfo.size:F2} KB");
                    item.SubItems.Add(recordTypeStr);
                    item.Tag = fileInfo;

                    lstRecordFiles.Items.Add(item);
                    count++;

                    if (count >= 2000) // Limit to 2000 files
                        break;
                }

                NetSDK.CLIENT_FindClose(findHandle);

                lblStatus.Text = $"Found {count} record files";

                if (count == 0)
                {
                    MessageBox.Show("No record files found in the specified time range.", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Search error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if (_loginID == IntPtr.Zero)
            {
                MessageBox.Show("Not logged in!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (lstRecordFiles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file to download!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                NET_RECORDFILE_INFO fileInfo = (NET_RECORDFILE_INFO)lstRecordFiles.SelectedItems[0].Tag!;

                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "DAV File|*.dav|All Files|*.*",
                    FileName = fileInfo.FileName
                };

                if (sfd.ShowDialog() != DialogResult.OK)
                    return;

                int channel = cmbChannel.SelectedIndex;
                int recordType = cmbQueryType.SelectedIndex;

                NET_TIME startTime = fileInfo.starttime;
                NET_TIME endTime = fileInfo.endtime;

                // Setup callback
                _downloadCallback = OnDownloadProgress;

                // Start download
                _downloadHandle = NetSDK.CLIENT_DownloadByTime(
                    _loginID,
                    channel,
                    recordType,
                    ref startTime,
                    ref endTime,
                    sfd.FileName,
                    _downloadCallback,
                    IntPtr.Zero);

                if (_downloadHandle == IntPtr.Zero)
                {
                    uint error = NetSDK.CLIENT_GetLastError();
                    MessageBox.Show($"Download failed: {NetSDK.GetErrorMessage(error)}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                btnDownload.Enabled = false;
                btnStopDownload.Enabled = true;
                progressDownload.Value = 0;
                lblStatus.Text = "Downloading...";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Download error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStopDownload_Click(object sender, EventArgs e)
        {
            if (_downloadHandle != IntPtr.Zero)
            {
                NetSDK.CLIENT_StopDownload(_downloadHandle);
                _downloadHandle = IntPtr.Zero;
            }

            btnDownload.Enabled = true;
            btnStopDownload.Enabled = false;
            lblStatus.Text = "Download stopped";
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (_downloadHandle != IntPtr.Zero)
            {
                NetSDK.CLIENT_StopDownload(_downloadHandle);
                _downloadHandle = IntPtr.Zero;
            }

            Close();
        }

        #endregion

        #region Callbacks

        private void OnDownloadProgress(IntPtr lPlayHandle, uint dwTotalSize, uint dwDownLoadSize,
            int index, ref NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser)
        {
            if (InvokeRequired)
            {
                // Create a local copy of the struct to avoid ref parameter in lambda
                NET_RECORDFILE_INFO recordfileinfoLocal = recordfileinfo;
                Invoke(new Action(() => OnDownloadProgress(lPlayHandle, dwTotalSize, dwDownLoadSize,
                    index, ref recordfileinfoLocal, dwUser)));
                return;
            }

            if (dwTotalSize > 0)
            {
                int progress = (int)((dwDownLoadSize * 100) / dwTotalSize);
                progressDownload.Value = Math.Min(progress, 100);
                lblStatus.Text = $"Downloading: {progress}% ({dwDownLoadSize / 1024.0 / 1024.0:F2} MB / {dwTotalSize / 1024.0 / 1024.0:F2} MB)";

                if (progress >= 100)
                {
                    _downloadHandle = IntPtr.Zero;
                    btnDownload.Enabled = true;
                    btnStopDownload.Enabled = false;
                    lblStatus.Text = "Download completed!";
                    MessageBox.Show("Download completed successfully!", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion

        #region Form Events

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (_downloadHandle != IntPtr.Zero)
            {
                NetSDK.CLIENT_StopDownload(_downloadHandle);
                _downloadHandle = IntPtr.Zero;
            }
        }

        #endregion
    }
}
