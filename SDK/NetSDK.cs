using System.Runtime.InteropServices;

namespace PlayBackCSharp.SDK
{
    /// <summary>
    /// Dahua NetSDK P/Invoke wrapper
    /// </summary>
    public static class NetSDK
    {
        private const string DLL_NAME = "dhnetsdk.dll";

        #region Callback Delegates

        /// <summary>
        /// Disconnect callback delegate
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public delegate void fDisConnect(IntPtr lLoginID,
            [MarshalAs(UnmanagedType.LPStr)] string pchDVRIP,
            int nDVRPort,
            IntPtr dwUser);

        /// <summary>
        /// Playback position callback delegate
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void fDownLoadPosCallBack(IntPtr lPlayHandle, uint dwTotalSize, uint dwDownLoadSize, IntPtr dwUser);

        /// <summary>
        /// Time download position callback delegate
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void fTimeDownLoadPosCallBack(IntPtr lPlayHandle, uint dwTotalSize, uint dwDownLoadSize,
            int index, ref NET_RECORDFILE_INFO recordfileinfo, IntPtr dwUser);

        /// <summary>
        /// Data callback delegate
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate int fDataCallBack(IntPtr lRealHandle, uint dwDataType, IntPtr pBuffer, uint dwBufSize, IntPtr dwUser);

        #endregion

        #region SDK Initialization

        /// <summary>
        /// Initialize SDK
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_Init(fDisConnect cbDisConnect, IntPtr dwUser);

        /// <summary>
        /// Cleanup SDK
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern void CLIENT_Cleanup();

        /// <summary>
        /// Set auto reconnect callback
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern void CLIENT_SetAutoReconnect(fDisConnect cbAutoConnect, IntPtr dwUser);

        /// <summary>
        /// Get SDK version
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern uint CLIENT_GetSDKVersion();

        #endregion

        #region Login/Logout

        /// <summary>
        /// Login to device
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CLIENT_Login(
            string pchDVRIP,
            ushort wDVRPort,
            string pchUserName,
            string pchPassword,
            ref NET_DEVICEINFO lpDeviceInfo,
            IntPtr error);

        /// <summary>
        /// Logout from device
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_Logout(IntPtr lLoginID);

        #endregion

        #region Playback Functions

        /// <summary>
        /// Play back by time (direct mode)
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CLIENT_PlayBackByTime(
            IntPtr lLoginID,
            int nChannelID,
            ref NET_TIME lpStartTime,
            ref NET_TIME lpStopTime,
            IntPtr hWnd,
            fDownLoadPosCallBack cbDownLoadPos,
            IntPtr dwUserData);

        /// <summary>
        /// Play back by time (server mode)
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CLIENT_PlayBackByTimeEx(
            IntPtr lLoginID,
            int nChannelID,
            ref NET_TIME lpStartTime,
            ref NET_TIME lpStopTime,
            IntPtr hWnd,
            fDownLoadPosCallBack cbDownLoadPos,
            IntPtr dwUserData,
            fDataCallBack fDownLoadDataCallBack,
            int nPlayDirection,
            IntPtr Reserved);

        /// <summary>
        /// Multi-channel playback by time
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CLIENT_MultiPlayBackByTime(
            IntPtr lLoginID,
            ref NET_MULTI_PLAYBACK_PARAM pInParam,
            IntPtr pOutParam,
            int waittime);

        /// <summary>
        /// Stop playback
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_StopPlayBack(IntPtr lPlayHandle);

        /// <summary>
        /// Pause playback
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_PausePlayBack(IntPtr lPlayHandle, bool bPause);

        /// <summary>
        /// Set playback position
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_SeekPlayBack(IntPtr lPlayHandle, uint offsettime, uint offsetbyte);

        /// <summary>
        /// Set playback speed
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_SetPlayBackSpeed(IntPtr lPlayHandle, float fSpeed);

        #endregion

        #region File Search

        /// <summary>
        /// Find file (begin search)
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr CLIENT_FindFile(
            IntPtr lLoginID,
            int nChannelId,
            int nRecordFileType,
            IntPtr cardid,                      // char* (use IntPtr.Zero for no card)
            ref NET_TIME time_start,
            ref NET_TIME time_end,
            int bTime,                          // BOOL (use 0 or 1)
            int waittime);

        /// <summary>
        /// Find next file
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int CLIENT_FindNextFile(IntPtr lFindHandle, out NET_RECORDFILE_INFO lpFindData);

        /// <summary>
        /// Find close (end search)
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_FindClose(IntPtr lFindHandle);

        #endregion

        #region Download Functions

        /// <summary>
        /// Download by time
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr CLIENT_DownloadByTime(
            IntPtr lLoginID,
            int nChannelId,
            int nRecordFileType,
            ref NET_TIME lpStartTime,
            ref NET_TIME lpEndTime,
            string sSavedFileName,
            fTimeDownLoadPosCallBack cbTimeDownLoadPos,
            IntPtr dwUserData);

        /// <summary>
        /// Stop download
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_StopDownload(IntPtr lFileHandle);

        /// <summary>
        /// Get download position
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern int CLIENT_GetDownloadPos(IntPtr lFileHandle);

        #endregion

        #region Capture Picture

        /// <summary>
        /// Capture picture format types
        /// </summary>
        public enum NET_CAPTURE_FORMATS
        {
            NET_CAPTURE_BMP = 0,        // BMP
            NET_CAPTURE_JPEG = 1,       // 100% quality JPEG
            NET_CAPTURE_JPEG_70 = 2,    // 70% quality JPEG
        }

        /// <summary>
        /// Capture picture from playback
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_CapturePicture(IntPtr lPlayHandle, string pchPicFileName);

        /// <summary>
        /// Capture picture from playback with format specification
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_CapturePictureEx(IntPtr lPlayHandle, string pchPicFileName, NET_CAPTURE_FORMATS eFormat);

        /// <summary>
        /// Snap picture from record by time
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool CLIENT_SnapPicFromRecord(IntPtr lLoginID, ref NET_IN_SNAP_PIC_FROM_REC pInParam, ref NET_OUT_SNAP_PIC_FROM_REC pOutParam, int nWaitTime);

        #endregion

        #region Error Handling

        /// <summary>
        /// Get last error code
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern uint CLIENT_GetLastError();

        #endregion

        #region Helper Methods

        /// <summary>
        /// Get error message from error code
        /// </summary>
        public static string GetErrorMessage(uint errorCode)
        {
            return errorCode switch
            {
                0x00000000 => "No error",
                0x00000001 => "DVR memory is not enough",
                0x00000002 => "DVR hard disk is not enough",
                0x00000003 => "DVR resource allocation error",
                0x00000004 => "DVR initialization error",
                0x00000005 => "DVR has started up",
                0x00000006 => "SDK has not been initialized",
                0x00000007 => "SDK has been initialized",
                0x00000008 => "Cannot allocate channel number",
                0x00000009 => "There is no such type of channel or bad channel number",
                0x0000000A => "Invalid handle",
                0x0000000B => "DVR is not connected",
                0x0000000C => "DVR has not established connection",
                0x0000000D => "Not supported video standard",
                0x0000000E => "Incorrect command or command data",
                0x0000000F => "Invalid port number",
                0x00000010 => "Invalid channel number",
                0x00000011 => "Incorrect password",
                0x00000012 => "Connection error",
                0x00000013 => "Send data timeout",
                0x00000014 => "Receive data timeout",
                0x00000015 => "Operation timeout",
                0x00000016 => "Unsupported function",
                0x00000017 => "Device is busy",
                _ => $"Unknown error (0x{errorCode:X8})"
            };
        }

        #endregion
    }
}
