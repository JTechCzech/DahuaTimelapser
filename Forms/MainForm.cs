using PlayBackCSharp.SDK;
using System.Runtime.InteropServices;

namespace PlayBackCSharp.Forms
{
    public partial class MainForm : Form
    {
        #region Fields

        private IntPtr _loginID = IntPtr.Zero;
        private IntPtr _playHandle = IntPtr.Zero;
        private NET_DEVICEINFO _deviceInfo;
        private int _channelCount = 0;
        private bool _isPlaying = false;
        private PlaySpeed _currentSpeed = PlaySpeed.SPEED_NORMAL;
        private System.Windows.Forms.Timer? _playbackTimer;

        // Playback position tracking
        private uint _currentPlaybackSize = 0;
        private uint _totalPlaybackSize = 0;

        // Callback delegates (must be kept alive to prevent GC)
        private NetSDK.fDisConnect? _disconnectCallback;
        private NetSDK.fDisConnect? _reconnectCallback;
        private NetSDK.fDownLoadPosCallBack? _playPosCallback;
        private NetSDK.fDataCallBack? _dataCallback;

        #endregion

        #region Constructor

        public MainForm()
        {
            InitializeComponent();
            InitializeSDK();
            SetupTimer();
        }

        #endregion

        #region Initialization

        private void InitializeSDK()
        {
            try
            {
                // Setup callbacks
                _disconnectCallback = OnDisconnect;
                _reconnectCallback = OnReconnect;

                // Initialize SDK
                bool result = NetSDK.CLIENT_Init(_disconnectCallback, IntPtr.Zero);
                if (!result)
                {
                    MessageBox.Show("Failed to initialize SDK!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Set auto reconnect
                NetSDK.CLIENT_SetAutoReconnect(_reconnectCallback, IntPtr.Zero);

                // Get SDK version
                uint version = NetSDK.CLIENT_GetSDKVersion();
                Text = $"Dahua Playback - SDK v{version >> 24}.{(version >> 16) & 0xFF}.{version & 0xFFFF}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SDK initialization error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupTimer()
        {
            _playbackTimer = new System.Windows.Forms.Timer
            {
                Interval = 500 // Update every 500ms
            };
            _playbackTimer.Tick += PlaybackTimer_Tick;
        }

        #endregion

        #region Event Handlers

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if (_loginID != IntPtr.Zero)
            {
                MessageBox.Show("Already logged in!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string ip = $"{txtIP1.Text}.{txtIP2.Text}.{txtIP3.Text}.{txtIP4.Text}";
                ushort port = (ushort)numPort.Value;
                string username = txtUsername.Text;
                string password = txtPassword.Text;

                _deviceInfo = new NET_DEVICEINFO();
                _loginID = NetSDK.CLIENT_Login(ip, port, username, password, ref _deviceInfo, IntPtr.Zero);

                if (_loginID == IntPtr.Zero)
                {
                    uint error = NetSDK.CLIENT_GetLastError();
                    MessageBox.Show($"Login failed: {NetSDK.GetErrorMessage(error)}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _channelCount = _deviceInfo.byChanNum;

                // Update UI
                btnLogin.Enabled = false;
                btnLogout.Enabled = true;
                grpPlayback.Enabled = true;

                // Populate channel combo box
                cmbChannel.Items.Clear();
                for (int i = 0; i < _channelCount; i++)
                {
                    cmbChannel.Items.Add($"Channel {i}");
                }
                if (cmbChannel.Items.Count > 0)
                    cmbChannel.SelectedIndex = 0;

                MessageBox.Show($"Login successful!\nChannels: {_channelCount}", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Login error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            StopPlayback();

            if (_loginID != IntPtr.Zero)
            {
                NetSDK.CLIENT_Logout(_loginID);
                _loginID = IntPtr.Zero;
            }

            // Update UI
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            grpPlayback.Enabled = false;
            cmbChannel.Items.Clear();

            MessageBox.Show("Logged out successfully!", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPlay_Click(object sender, EventArgs e)
        {
            if (_isPlaying)
            {
                MessageBox.Show("Already playing!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int channel = cmbChannel.SelectedIndex;
                if (channel < 0)
                {
                    MessageBox.Show("Please select a channel!", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get time range
                NET_TIME startTime = NET_TIME.FromDateTime(dtpStartDate.Value.Date + dtpStartTime.Value.TimeOfDay);
                NET_TIME endTime = NET_TIME.FromDateTime(dtpEndDate.Value.Date + dtpEndTime.Value.TimeOfDay);

                // Setup callback
                _playPosCallback = OnPlayPosition;
                _dataCallback = OnDataCallback;

                // Start playback
                if (chkServerMode.Checked)
                {
                    // Server mode
                    int direction = cmbDirection.SelectedIndex; // 0=forward, 1=backward
                    _playHandle = NetSDK.CLIENT_PlayBackByTimeEx(
                        _loginID,
                        channel,
                        ref startTime,
                        ref endTime,
                        panelVideo.Handle,
                        _playPosCallback,
                        IntPtr.Zero,
                        _dataCallback,
                        direction,
                        IntPtr.Zero);
                }
                else
                {
                    // Direct mode
                    _playHandle = NetSDK.CLIENT_PlayBackByTime(
                        _loginID,
                        channel,
                        ref startTime,
                        ref endTime,
                        panelVideo.Handle,
                        _playPosCallback,
                        IntPtr.Zero);
                }

                if (_playHandle == IntPtr.Zero)
                {
                    uint error = NetSDK.CLIENT_GetLastError();
                    MessageBox.Show($"Playback failed: {NetSDK.GetErrorMessage(error)}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _isPlaying = true;
                _playbackTimer?.Start();

                // Update UI
                btnPlay.Enabled = false;
                btnStop.Enabled = true;
                btnPause.Enabled = true;
                grpSpeed.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Playback error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            StopPlayback();
        }

        private void BtnPause_Click(object sender, EventArgs e)
        {
            if (_playHandle != IntPtr.Zero)
            {
                bool pause = btnPause.Text == "Pause";
                NetSDK.CLIENT_PausePlayBack(_playHandle, pause);
                btnPause.Text = pause ? "Resume" : "Pause";
            }
        }

        private void BtnSlowDown_Click(object sender, EventArgs e)
        {
            if (_currentSpeed > PlaySpeed.SPEED_MIN)
            {
                _currentSpeed--;
                SetPlaySpeed(_currentSpeed);
            }
        }

        private void BtnSpeedUp_Click(object sender, EventArgs e)
        {
            if (_currentSpeed < PlaySpeed.SPEED_MAX)
            {
                _currentSpeed++;
                SetPlaySpeed(_currentSpeed);
            }
        }

        private void BtnNormalSpeed_Click(object sender, EventArgs e)
        {
            _currentSpeed = PlaySpeed.SPEED_NORMAL;
            SetPlaySpeed(_currentSpeed);
        }

        private void BtnCapture_Click(object sender, EventArgs e)
        {
            if (_playHandle == IntPtr.Zero)
            {
                MessageBox.Show("No playback in progress!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using SaveFileDialog sfd = new SaveFileDialog
                {
                    Filter = "JPEG Image|*.jpg|BMP Image|*.bmp",
                    FileName = $"Capture_{DateTime.Now:yyyyMMdd_HHmmss}.jpg"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    bool result = NetSDK.CLIENT_CapturePicture(_playHandle, sfd.FileName);
                    if (result)
                    {
                        MessageBox.Show("Picture captured successfully!", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        uint error = NetSDK.CLIENT_GetLastError();
                        MessageBox.Show($"Capture failed: {NetSDK.GetErrorMessage(error)}", "Error",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Capture error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TrackPosition_Scroll(object sender, EventArgs e)
        {
            if (_playHandle == IntPtr.Zero)
                return;

            // Calculate byte offset from percentage
            if (_totalPlaybackSize > 0)
            {
                uint offsetByte = (_totalPlaybackSize * (uint)trackPosition.Value) / 100;
                NetSDK.CLIENT_SeekPlayBack(_playHandle, 0, offsetByte);
            }
        }

        private void BtnAdvanced_Click(object sender, EventArgs e)
        {
            if (_loginID == IntPtr.Zero)
            {
                MessageBox.Show("Please login first!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using PlaybackByTimeForm advancedForm = new PlaybackByTimeForm(_loginID, _channelCount);
            advancedForm.ShowDialog(this);
        }

        private void PlaybackTimer_Tick(object? sender, EventArgs e)
        {
            if (_playHandle == IntPtr.Zero)
                return;

            // Update UI with position from callback
            if (_totalPlaybackSize > 0)
            {
                int progress = (int)((_currentPlaybackSize * 100) / _totalPlaybackSize);
                trackPosition.Maximum = 100;
                trackPosition.Value = Math.Min(progress, 100);

                // Display as percentage
                lblPosition.Text = $"Progress: {progress}%";
            }
        }

        #endregion

        #region Callbacks

        private void OnDisconnect(IntPtr lLoginID, string pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnDisconnect(lLoginID, pchDVRIP, nDVRPort, dwUser)));
                return;
            }

            MessageBox.Show($"Device disconnected!\nIP: {pchDVRIP}:{nDVRPort}", "Warning",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);

            StopPlayback();
            _loginID = IntPtr.Zero;
            btnLogin.Enabled = true;
            btnLogout.Enabled = false;
            grpPlayback.Enabled = false;
        }

        private void OnReconnect(IntPtr lLoginID, string pchDVRIP, int nDVRPort, IntPtr dwUser)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => OnReconnect(lLoginID, pchDVRIP, nDVRPort, dwUser)));
                return;
            }

            MessageBox.Show($"Device reconnected!\nIP: {pchDVRIP}:{nDVRPort}", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void OnPlayPosition(IntPtr lPlayHandle, uint dwTotalSize, uint dwDownLoadSize, IntPtr dwUser)
        {
            // Store playback position for timer to display
            if (lPlayHandle == _playHandle)
            {
                _totalPlaybackSize = dwTotalSize;
                _currentPlaybackSize = dwDownLoadSize;
            }
        }

        private int OnDataCallback(IntPtr lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr dwUser)
        {
            // Handle data callback if needed
            return 0;
        }

        #endregion

        #region Helper Methods

        private void StopPlayback()
        {
            _playbackTimer?.Stop();

            if (_playHandle != IntPtr.Zero)
            {
                NetSDK.CLIENT_StopPlayBack(_playHandle);
                _playHandle = IntPtr.Zero;
            }

            _isPlaying = false;
            _currentSpeed = PlaySpeed.SPEED_NORMAL;
            _currentPlaybackSize = 0;
            _totalPlaybackSize = 0;

            // Update UI
            btnPlay.Enabled = true;
            btnStop.Enabled = false;
            btnPause.Enabled = false;
            btnPause.Text = "Pause";
            grpSpeed.Enabled = false;
            trackPosition.Value = 0;
            lblPosition.Text = "Progress: 0%";
        }

        private void SetPlaySpeed(PlaySpeed speed)
        {
            if (_playHandle == IntPtr.Zero)
                return;

            float speedValue = speed switch
            {
                PlaySpeed.SPEED_DOWN_8 => 0.125f,
                PlaySpeed.SPEED_DOWN_4 => 0.25f,
                PlaySpeed.SPEED_DOWN_2 => 0.5f,
                PlaySpeed.SPEED_NORMAL => 1.0f,
                PlaySpeed.SPEED_UP_2 => 2.0f,
                PlaySpeed.SPEED_UP_4 => 4.0f,
                PlaySpeed.SPEED_UP_8 => 8.0f,
                _ => 1.0f
            };

            NetSDK.CLIENT_SetPlayBackSpeed(_playHandle, speedValue);
            lblSpeed.Text = $"Speed: {speedValue:F2}x";
        }

        #endregion

        #region Form Events

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            StopPlayback();

            if (_loginID != IntPtr.Zero)
            {
                NetSDK.CLIENT_Logout(_loginID);
                _loginID = IntPtr.Zero;
            }

            NetSDK.CLIENT_Cleanup();
        }

        #endregion
    }
}
