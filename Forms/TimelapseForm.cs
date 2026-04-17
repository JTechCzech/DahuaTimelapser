using PlayBackCSharp.SDK;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Globalization;

namespace PlayBackCSharp.Forms
{
    public partial class TimelapseForm : Form
    {
        #region Fields

        private IntPtr _loginID = IntPtr.Zero;
        private NET_DEVICEINFO _deviceInfo;
        private int _channelCount = 0;
        private bool _isExporting = false;
        private CancellationTokenSource? _exportCancellation;
        private bool _isLoadingPreview = false;
        private List<DateTime> _previewTimestamps = new List<DateTime>();

        // Callback delegates (must be kept alive to prevent GC)
        private NetSDK.fDisConnect? _disconnectCallback;
        private NetSDK.fDisConnect? _reconnectCallback;

        #endregion

        #region Constructor

        public TimelapseForm()
        {
            InitializeComponent();
            InitializeSDK();
            InitializeTimelapseControls();
        }

        private void InitializeTimelapseControls()
        {
            // Set default time range (last hour)
            dateTimePickerEnd.Value = DateTime.Now;
            dateTimePickerStart.Value = DateTime.Now.AddHours(-1);

            // Set default timelapse interval
            numericUpDownFrameInterval.Minimum = 1;
            numericUpDownFrameInterval.Maximum = 3600; // Max 1 hour interval
            numericUpDownFrameInterval.Value = 30; // Default 30 seconds

            // Default to image export
            radioButtonTimelapseImg.Checked = true;

            // Setup preview trackbar
            trackBarPreview.Minimum = 0;
            trackBarPreview.Maximum = 100;
            trackBarPreview.Value = 0;
            trackBarPreview.Scroll += TrackBarPreview_Scroll;

            // Setup preview picture box
            pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPreview.BackColor = Color.Black;

            // Setup video export controls
            numericUpDownVidFPS.Minimum = 1;
            numericUpDownVidFPS.Maximum = 120;
            numericUpDownVidFPS.Value = 5;
            numericUpDownVidFPS.DecimalPlaces = 0;

            numericUpDownVidL.Minimum = 1;
            numericUpDownVidL.Maximum = 3600; // Max 1 hour video
            numericUpDownVidL.Value = 5;
            numericUpDownVidL.DecimalPlaces = 0;

            // Event handlers for video mode
            radioButtonTimelapseVid.CheckedChanged += RadioButtonTimelapseVid_CheckedChanged;
            radioButtonTimelapseImg.CheckedChanged += RadioButtonTimelapseImg_CheckedChanged;

            numericUpDownVidFPS.Enter += NumericUpDownVidFPS_Enter;
            numericUpDownVidL.Enter += NumericUpDownVidL_Enter;

            numericUpDownVidFPS.ValueChanged += NumericUpDownVidFPS_ValueChanged;
            numericUpDownVidL.ValueChanged += NumericUpDownVidL_ValueChanged;
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
                Text = $"Dahua Timelapse - SDK v{version >> 24}.{(version >> 16) & 0xFF}.{version & 0xFFFF}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"SDK initialization error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                grpConnection.Enabled = false;
                btnLogout.Enabled = true;
                grpTimelapse.Enabled = true;

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
            if (_loginID != IntPtr.Zero)
            {
                NetSDK.CLIENT_Logout(_loginID);
                _loginID = IntPtr.Zero;
            }

            // Update UI
            grpConnection.Enabled = true;
            btnLogout.Enabled = false;
            grpTimelapse.Enabled = false;

            MessageBox.Show("Logged out successfully!", "Info",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async void BtnExport_Click(object sender, EventArgs e)
        {
            if (_loginID == IntPtr.Zero)
            {
                MessageBox.Show("Please login first!", "Warning",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isExporting)
            {
                // Cancel export
                _exportCancellation?.Cancel();
                return;
            }

            // Validate input
            DateTime startTime = dateTimePickerStart.Value;
            DateTime endTime = dateTimePickerEnd.Value;
            int intervalSeconds = (int)numericUpDownFrameInterval.Value;

            if (endTime <= startTime)
            {
                MessageBox.Show("End time must be after start time!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (intervalSeconds <= 0)
            {
                MessageBox.Show("Interval must be greater than 0!", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Check export mode
            if (radioButtonTimelapseVid.Checked)
            {
                // Video export - select output file
                using SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "MP4 Video|*.mp4|AVI Video|*.avi|All Files|*.*",
                    DefaultExt = "mp4",
                    FileName = $"timelapse_{DateTime.Now:yyyyMMdd_HHmmss}.mp4"
                };

                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                string outputFile = saveDialog.FileName;
                int fps = (int)numericUpDownVidFPS.Value;

                // Start video export
                await ExportTimelapseVideo(startTime, endTime, intervalSeconds, outputFile, fps);
            }
            else
            {
                // Image export - select output folder
                using FolderBrowserDialog folderDialog = new FolderBrowserDialog
                {
                    Description = "Select folder to save timelapse images",
                    ShowNewFolderButton = true
                };

                if (folderDialog.ShowDialog() != DialogResult.OK)
                    return;

                string outputFolder = folderDialog.SelectedPath;

                // Start image export
                await ExportTimelapseImages(startTime, endTime, intervalSeconds, outputFolder);
            }
        }

        private async void TrackBarPreview_Scroll(object sender, EventArgs e)
        {
            if (_loginID == IntPtr.Zero)
            {
                MessageBox.Show("Please login first!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_isLoadingPreview)
                return;

            DateTime startTime = dateTimePickerStart.Value;
            DateTime endTime = dateTimePickerEnd.Value;

            if (endTime <= startTime)
            {
                MessageBox.Show("End time must be after start time!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Calculate time based on trackbar position
            double percentage = trackBarPreview.Value / 100.0;
            TimeSpan duration = endTime - startTime;
            DateTime targetTime = startTime.Add(TimeSpan.FromSeconds(duration.TotalSeconds * percentage));

            // Update status
            lblPreviewTime.Text = $"Loading: {targetTime:yyyy-MM-dd HH:mm:ss}";

            // Load preview image from camera
            await LoadPreviewImage(targetTime);
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

            _loginID = IntPtr.Zero;
            grpConnection.Enabled = true;
            btnLogout.Enabled = false;
            grpTimelapse.Enabled = false;
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

        #endregion

        #region Timelapse Export

        private async Task ExportTimelapseVideo(DateTime startTime, DateTime endTime, int intervalSeconds, string outputFile, int fps)
        {
            _exportCancellation = new CancellationTokenSource();
            _isExporting = true;

            // Create frames folder next to output video
            string? videoDirectory = Path.GetDirectoryName(outputFile);
            if (string.IsNullOrEmpty(videoDirectory))
                videoDirectory = Directory.GetCurrentDirectory();

            string framesFolder = Path.Combine(videoDirectory, "frames");
            Directory.CreateDirectory(framesFolder);

            // Check if we should keep frames after encoding
            bool keepFrames = radioButtonTimelapseImg.Checked;

            try
            {
                // Update UI
                btnExport.Text = "Cancel";
                progressBarExport.Value = 0;
                progressBarExport.Maximum = 100;
                lblExportInfo.Text = "Preparing video export...";

                // Step 1: Export frames to frames folder
                lblExportInfo.Text = "Downloading frames...";
                await ExportFramesToFolder(startTime, endTime, intervalSeconds, framesFolder);

                if (_exportCancellation.Token.IsCancellationRequested)
                {
                    MessageBox.Show("Export cancelled!", "Cancelled",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Step 2: Create video using ffmpeg
                lblExportInfo.Text = "Creating video with ffmpeg...";
                progressBarExport.Value = 50;

                bool success = await CreateVideoWithFFmpeg(framesFolder, outputFile, fps);

                if (success && !_exportCancellation.Token.IsCancellationRequested)
                {
                    lblExportInfo.Text = "Video export completed!";
                    progressBarExport.Value = 100;

                    // Delete frames folder if not keeping images
                    if (!keepFrames)
                    {
                        try
                        {
                            if (Directory.Exists(framesFolder))
                                Directory.Delete(framesFolder, true);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Failed to delete frames folder: {ex.Message}");
                        }

                        MessageBox.Show($"Video export completed!\nSaved to:\n{outputFile}", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Video export completed!\nSaved to:\n{outputFile}\n\nFrames saved to:\n{framesFolder}", "Success",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else if (!success)
                {
                    MessageBox.Show("FFmpeg failed to create video. Make sure ffmpeg.exe is in the application directory.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Video export error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isExporting = false;
                btnExport.Text = "Export";
                progressBarExport.Value = 0;
                lblExportInfo.Text = "";
                _exportCancellation?.Dispose();
                _exportCancellation = null;
            }
        }

        private async Task<bool> CreateVideoWithFFmpeg(string framesFolder, string outputFile, int fps)
        {
            try
            {
                // Find ffmpeg.exe in application directory
                string appDir = AppDomain.CurrentDomain.BaseDirectory;
                string ffmpegPath = Path.Combine(appDir, "ffmpeg.exe");

                if (!File.Exists(ffmpegPath))
                {
                    Debug.WriteLine($"FFmpeg not found at: {ffmpegPath}");
                    return false;
                }

                // Create input file list for ffmpeg (more reliable than glob on Windows)
                string fileListPath = Path.Combine(framesFolder, "filelist.txt");
                var frameFiles = Directory.GetFiles(framesFolder, "frame_*.jpg")
                    .OrderBy(f => f)
                    .ToList();

                int totalFrames = frameFiles.Count;

                // Write file list
                using (StreamWriter writer = new StreamWriter(fileListPath))
                {
                    foreach (string file in frameFiles)
                    {
                        // FFmpeg expects forward slashes and special escaping
                        string escapedPath = file.Replace("\\", "/").Replace("'", "'\\''");
                        writer.WriteLine($"file '{escapedPath}'");
                    }
                }

                // Build ffmpeg command
                // -f concat: concatenate files from list
                // -safe 0: allow absolute paths
                // -r: output framerate
                // -c:v libx264: H.264 codec
                // -pix_fmt yuv420p: pixel format for compatibility
                // -progress pipe:2: output progress to stderr
                // -y: overwrite output file
                string arguments = $"-f concat -safe 0 -i \"{fileListPath}\" -r {fps} -c:v libx264 -pix_fmt yuv420p -progress pipe:2 -y \"{outputFile}\"";

                Debug.WriteLine($"FFmpeg command: {ffmpegPath} {arguments}");

                // Start ffmpeg process
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = ffmpegPath,
                    Arguments = arguments,
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using (Process process = new Process { StartInfo = startInfo })
                {
                    process.Start();

                    // Track progress
                    Stopwatch encodingStopwatch = Stopwatch.StartNew();

                    var progressTask = Task.Run(async () =>
                    {
                        string? line;
                        int currentFrame = 0;

                        while ((line = await process.StandardError.ReadLineAsync()) != null)
                        {
                            // Parse FFmpeg progress output
                            // Example: "frame=  120 fps= 30 q=28.0 size=  512kB time=00:00:04.00 bitrate=1048.6kbits/s speed=1.5x"

                            if (line.Contains("frame="))
                            {
                                var match = Regex.Match(line, @"frame=\s*(\d+)");
                                if (match.Success && int.TryParse(match.Groups[1].Value, out currentFrame))
                                {
                                    // Update progress (50-100% for encoding)
                                    int progress = 50 + (int)((currentFrame * 50.0) / totalFrames);
                                    Invoke(() => progressBarExport.Value = Math.Min(100, progress));

                                    // Calculate remaining time
                                    if (currentFrame > 0)
                                    {
                                        double elapsedSeconds = encodingStopwatch.Elapsed.TotalSeconds;
                                        double secondsPerFrame = elapsedSeconds / currentFrame;
                                        int framesRemaining = totalFrames - currentFrame;
                                        double estimatedSecondsRemaining = framesRemaining * secondsPerFrame;
                                        TimeSpan remainingTime = TimeSpan.FromSeconds(estimatedSecondsRemaining);

                                        // Update info label with encoding progress and remaining time
                                        Invoke(() => lblExportInfo.Text = $"Encoding video: {currentFrame}/{totalFrames} frames - {remainingTime:hh\\:mm\\:ss}");
                                    }
                                }
                            }

                            Debug.WriteLine($"FFmpeg: {line}");
                        }
                    });

                    await progressTask;
                    await process.WaitForExitAsync();

                    return process.ExitCode == 0;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"FFmpeg error: {ex.Message}");
                return false;
            }
        }

        private async Task ExportFramesToFolder(DateTime startTime, DateTime endTime, int intervalSeconds, string outputFolder)
        {
            // Calculate all timestamps
            List<DateTime> timestamps = CalculateTimestamps(startTime, endTime, intervalSeconds);
            int totalFrames = timestamps.Count;

            if (totalFrames == 0)
                return;

            // Channel 0 by default
            int channel = 0;

            // Get window handle on UI thread for rendering
            IntPtr windowHandle = pictureBoxPreview.Handle;

            // Track export statistics
            long totalBytesDownloaded = 0;
            System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

            for (int i = 0; i < totalFrames; i++)
            {
                if (_exportCancellation.Token.IsCancellationRequested)
                    break;

                DateTime frameTime = timestamps[i];
                string filename = Path.Combine(outputFolder, $"frame_{i:D6}_{frameTime:yyyyMMdd_HHmmss}.jpg");

                // Capture frame at specific time
                bool success = await Task.Run(() => CaptureFrameAtTime(channel, frameTime, filename, windowHandle));

                if (success && File.Exists(filename))
                {
                    FileInfo fileInfo = new FileInfo(filename);
                    totalBytesDownloaded += fileInfo.Length;
                }
                else
                {
                    Debug.WriteLine($"Failed to capture frame at {frameTime}");
                }

                // Update progress (0-50% for frame export)
                int progress = (int)((i + 1) * 50.0 / totalFrames);
                progressBarExport.Value = progress;

                // Calculate statistics
                double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                double speedMBps = elapsedSeconds > 0 ? (totalBytesDownloaded / (1024.0 * 1024.0)) / elapsedSeconds : 0;

                // Estimate remaining time
                int framesRemaining = totalFrames - (i + 1);
                double avgTimePerFrame = elapsedSeconds / (i + 1);
                double estimatedSecondsRemaining = framesRemaining * avgTimePerFrame;
                TimeSpan remainingTime = TimeSpan.FromSeconds(estimatedSecondsRemaining);

                // Update info label
                lblExportInfo.Text = $"Downloading {i + 1}/{totalFrames} - {speedMBps:F2}MB/s - {remainingTime:hh\\:mm\\:ss}";

                // Allow UI to update
                await Task.Delay(10);
            }
        }

        private async Task ExportTimelapseImages(DateTime startTime, DateTime endTime, int intervalSeconds, string outputFolder)
        {
            _exportCancellation = new CancellationTokenSource();
            _isExporting = true;

            try
            {
                // Update UI
                btnExport.Text = "Cancel";
                progressBarExport.Value = 0;
                progressBarExport.Maximum = 100;

                // Calculate all timestamps
                List<DateTime> timestamps = CalculateTimestamps(startTime, endTime, intervalSeconds);
                int totalFrames = timestamps.Count;

                if (totalFrames == 0)
                {
                    MessageBox.Show("No frames to export!", "Info",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Channel 0 by default - could be made configurable
                int channel = 0;

                // Get window handle on UI thread for rendering
                IntPtr windowHandle = pictureBoxPreview.Handle;

                // Track export statistics
                DateTime exportStartTime = DateTime.Now;
                long totalBytesDownloaded = 0;
                System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();

                for (int i = 0; i < totalFrames; i++)
                {
                    if (_exportCancellation.Token.IsCancellationRequested)
                    {
                        MessageBox.Show($"Export cancelled! Exported {i} of {totalFrames} frames.", "Cancelled",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }

                    DateTime frameTime = timestamps[i];
                    string filename = Path.Combine(outputFolder, $"frame_{i:D6}_{frameTime:yyyyMMdd_HHmmss}.jpg");

                    // Capture frame at specific time
                    bool success = await Task.Run(() => CaptureFrameAtTime(channel, frameTime, filename, windowHandle));

                    if (success && File.Exists(filename))
                    {
                        // Add file size to total
                        FileInfo fileInfo = new FileInfo(filename);
                        totalBytesDownloaded += fileInfo.Length;
                    }
                    else
                    {
                        // Log error but continue
                        Debug.WriteLine($"Failed to capture frame at {frameTime}");
                    }

                    // Update progress
                    int progress = (int)((i + 1) * 100.0 / totalFrames);
                    progressBarExport.Value = progress;

                    // Calculate statistics
                    double elapsedSeconds = stopwatch.Elapsed.TotalSeconds;
                    double speedMBps = elapsedSeconds > 0 ? (totalBytesDownloaded / (1024.0 * 1024.0)) / elapsedSeconds : 0;

                    // Estimate remaining time
                    int framesRemaining = totalFrames - (i + 1);
                    double avgTimePerFrame = elapsedSeconds / (i + 1);
                    double estimatedSecondsRemaining = framesRemaining * avgTimePerFrame;
                    TimeSpan remainingTime = TimeSpan.FromSeconds(estimatedSecondsRemaining);

                    // Update info label
                    lblExportInfo.Text = $"Downloading {i + 1}/{totalFrames} - {speedMBps:F2}MB/s - {remainingTime:hh\\:mm\\:ss}";

                    // Allow UI to update
                    await Task.Delay(10);
                }

                if (!_exportCancellation.Token.IsCancellationRequested)
                {
                    lblExportInfo.Text = "Export completed!";
                    MessageBox.Show($"Export completed!\n{totalFrames} frames saved to:\n{outputFolder}", "Success",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Export error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _isExporting = false;
                btnExport.Text = "Export";
                progressBarExport.Value = 0;
                lblExportInfo.Text = "";
                _exportCancellation?.Dispose();
                _exportCancellation = null;
            }
        }

        private List<DateTime> CalculateTimestamps(DateTime startTime, DateTime endTime, int intervalSeconds)
        {
            List<DateTime> timestamps = new List<DateTime>();
            DateTime current = startTime;

            while (current <= endTime)
            {
                timestamps.Add(current);
                current = current.AddSeconds(intervalSeconds);
            }

            return timestamps;
        }

        private bool CaptureFrameAtTime(int channel, DateTime frameTime, string outputPath, IntPtr windowHandle)
        {
            IntPtr playHandle = IntPtr.Zero;

            try
            {
                // Debug output
                Debug.WriteLine("=== CaptureFrameAtTime ===");
                Debug.WriteLine($"Channel: {channel}");
                Debug.WriteLine($"Time: {frameTime:yyyy-MM-dd HH:mm:ss}");
                Debug.WriteLine($"LoginID: {_loginID}");
                Debug.WriteLine($"WindowHandle: {windowHandle}");
                Debug.WriteLine($"Output: {outputPath}");

                // Ensure output directory exists
                string? directory = Path.GetDirectoryName(outputPath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                    Debug.WriteLine($"Created directory: {directory}");
                }

                // Convert to NET_TIME - use 3 second window (shorter)
                NET_TIME startTime = NET_TIME.FromDateTime(frameTime);
                NET_TIME endTime = NET_TIME.FromDateTime(frameTime.AddSeconds(3));

                Debug.WriteLine($"Starting playback from {frameTime:HH:mm:ss} to {frameTime.AddSeconds(3):HH:mm:ss}");

                playHandle = NetSDK.CLIENT_PlayBackByTime(
                    _loginID,
                    channel,
                    ref startTime,
                    ref endTime,
                    windowHandle,
                    null,
                    IntPtr.Zero);

                if (playHandle == IntPtr.Zero)
                {
                    uint errorCode = NetSDK.CLIENT_GetLastError();
                    Debug.WriteLine($"CLIENT_PlayBackByTime failed: {NetSDK.GetErrorMessage(errorCode)} (0x{errorCode:X8})");
                    return false;
                }

                Debug.WriteLine($"Playback started. Handle: {playHandle}");

                // Wait in small increments and try to capture as soon as possible
                Debug.WriteLine("Waiting for first frame...");
                int attempts = 0;
                int maxAttempts = 20; // Max 2 seconds (20 x 100ms)
                bool captured = false;

                while (attempts < maxAttempts && !captured)
                {
                    Thread.Sleep(100); // Short sleep
                    attempts++;

                    // Try to capture
                    captured = NetSDK.CLIENT_CapturePictureEx(playHandle, outputPath, NetSDK.NET_CAPTURE_FORMATS.NET_CAPTURE_JPEG);

                    if (captured && File.Exists(outputPath))
                    {
                        long fileSize = new FileInfo(outputPath).Length;
                        if (fileSize > 0)
                        {
                            Debug.WriteLine($"SUCCESS! First frame captured after {attempts * 100}ms, Size: {fileSize} bytes");
                            return true;
                        }
                    }
                }

                // If we got here, capture failed
                uint finalError = NetSDK.CLIENT_GetLastError();
                Debug.WriteLine($"Failed to capture after {attempts * 100}ms: {NetSDK.GetErrorMessage(finalError)} (0x{finalError:X8})");
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"EXCEPTION: {ex.Message}");
                Debug.WriteLine($"Stack: {ex.StackTrace}");
                return false;
            }
            finally
            {
                // Stop playback
                if (playHandle != IntPtr.Zero)
                {
                    NetSDK.CLIENT_StopPlayBack(playHandle);
                    Debug.WriteLine("Playback stopped.");
                }
            }
        }

        #endregion

        #region Preview

        private async Task LoadPreviewImage(DateTime targetTime)
        {
            _isLoadingPreview = true;

            try
            {
                // Get window handle on UI thread
                IntPtr windowHandle = pictureBoxPreview.Handle;

                // Capture frame at target time - returns image bytes directly
                int channel = 0; // Channel 0 by default
                byte[]? imageBytes = await Task.Run(() => CaptureFrameToMemory(channel, targetTime, windowHandle));

                if (imageBytes != null && imageBytes.Length > 0)
                {
                    // Load image from memory into picture box
                    using (var ms = new MemoryStream(imageBytes))
                    {
                        pictureBoxPreview.Image?.Dispose();
                        pictureBoxPreview.Image = Image.FromStream(ms);
                    }

                    lblPreviewTime.Text = targetTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    // Show error in picture box
                    pictureBoxPreview.Image?.Dispose();
                    pictureBoxPreview.Image = null;
                    lblPreviewTime.Text = $"No data: {targetTime:yyyy-MM-dd HH:mm:ss}";
                }
            }
            catch (Exception ex)
            {
                pictureBoxPreview.Image?.Dispose();
                pictureBoxPreview.Image = null;
                lblPreviewTime.Text = "Error";
                Debug.WriteLine($"Preview load error: {ex.Message}");
            }
            finally
            {
                _isLoadingPreview = false;
            }
        }

        private byte[]? CaptureFrameToMemory(int channel, DateTime frameTime, IntPtr windowHandle)
        {
            // Use temp file for capture, then read to memory and delete
            string tempPath = Path.Combine(Path.GetTempPath(), $"preview_{Guid.NewGuid()}.jpg");

            try
            {
                bool success = CaptureFrameAtTime(channel, frameTime, tempPath, windowHandle);

                if (success && File.Exists(tempPath))
                {
                    // Read file into memory
                    byte[] bytes = File.ReadAllBytes(tempPath);
                    return bytes;
                }

                return null;
            }
            finally
            {
                // Always clean up temp file
                try
                {
                    if (File.Exists(tempPath))
                        File.Delete(tempPath);
                }
                catch { }
            }
        }

        #endregion

        #region Video Export Logic

        private void RadioButtonTimelapseVid_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTimelapseVid.Checked)
            {
                // Show video controls
                numericUpDownVidFPS.Visible = true;
                lblVidFPS.Visible = true;
                numericUpDownVidL.Visible = true;
                lblVidL.Visible = true;

                // Calculate initial values
                CalculateVideoLength();
            }
        }

        private void RadioButtonTimelapseImg_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonTimelapseImg.Checked)
            {
                // Hide video controls
                numericUpDownVidFPS.Visible = false;
                lblVidFPS.Visible = false;
                numericUpDownVidL.Visible = false;
                lblVidL.Visible = false;
            }
        }

        private void NumericUpDownVidFPS_Enter(object sender, EventArgs e)
        {
            // Activate FPS, make length readonly and calculate it
            numericUpDownVidFPS.ReadOnly = false;
            numericUpDownVidL.ReadOnly = true;
            CalculateVideoLength();
        }

        private void NumericUpDownVidL_Enter(object sender, EventArgs e)
        {
            // Activate length, make FPS readonly and calculate it
            numericUpDownVidL.ReadOnly = false;
            numericUpDownVidFPS.ReadOnly = true;
            CalculateVideoFPS();
        }

        private void NumericUpDownVidFPS_ValueChanged(object sender, EventArgs e)
        {
            // Only recalculate if FPS is active (not readonly)
            if (!numericUpDownVidFPS.ReadOnly)
            {
                CalculateVideoLength();
            }
        }

        private void NumericUpDownVidL_ValueChanged(object sender, EventArgs e)
        {
            // Only recalculate if length is active (not readonly)
            if (!numericUpDownVidL.ReadOnly)
            {
                CalculateVideoFPS();
            }
        }

        private void CalculateVideoLength()
        {
            DateTime startTime = dateTimePickerStart.Value;
            DateTime endTime = dateTimePickerEnd.Value;
            int intervalSeconds = (int)numericUpDownFrameInterval.Value;
            decimal fps = numericUpDownVidFPS.Value;

            if (endTime <= startTime || intervalSeconds <= 0 || fps <= 0)
                return;

            // Calculate number of frames
            TimeSpan duration = endTime - startTime;
            int totalFrames = (int)(duration.TotalSeconds / intervalSeconds);

            // Calculate video length in seconds
            decimal videoLength = totalFrames / fps;

            // Update video length control
            numericUpDownVidL.ValueChanged -= NumericUpDownVidL_ValueChanged;
            numericUpDownVidL.Value = Math.Max(1, Math.Min(numericUpDownVidL.Maximum, videoLength));
            numericUpDownVidL.ValueChanged += NumericUpDownVidL_ValueChanged;
        }

        private void CalculateVideoFPS()
        {
            DateTime startTime = dateTimePickerStart.Value;
            DateTime endTime = dateTimePickerEnd.Value;
            int intervalSeconds = (int)numericUpDownFrameInterval.Value;
            decimal videoLength = numericUpDownVidL.Value;

            if (endTime <= startTime || intervalSeconds <= 0 || videoLength <= 0)
                return;

            // Calculate number of frames
            TimeSpan duration = endTime - startTime;
            int totalFrames = (int)(duration.TotalSeconds / intervalSeconds);

            // Calculate FPS
            decimal fps = totalFrames / videoLength;

            // Update FPS control
            numericUpDownVidFPS.ValueChanged -= NumericUpDownVidFPS_ValueChanged;
            numericUpDownVidFPS.Value = Math.Max(1, Math.Min(numericUpDownVidFPS.Maximum, fps));
            numericUpDownVidFPS.ValueChanged += NumericUpDownVidFPS_ValueChanged;
        }

        #endregion

        #region Form Events

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Cancel any ongoing export
            if (_isExporting)
            {
                _exportCancellation?.Cancel();

                // Wait a moment for cancellation
                Task.Delay(500).Wait();
            }

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
