using System.Runtime.InteropServices;

namespace PlayBackCSharp.SDK
{
    /// <summary>
    /// Dahua Play SDK P/Invoke wrapper
    /// </summary>
    public static class PlaySDK
    {
        private const string DLL_NAME = "dhplay.dll";

        #region Enums

        public enum PicFormats
        {
            PIC_BMP = 0,
            PIC_JPEG = 1
        }

        public enum PlayDirection
        {
            FORWARD = 0,
            BACKWARD = 1
        }

        #endregion

        #region Callback Delegates

        /// <summary>
        /// Draw callback delegate
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void DrawFun(int nPort, IntPtr hDc, IntPtr nUser);

        #endregion

        #region Stream Operations

        /// <summary>
        /// Open stream
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_OpenStream(int nPort, IntPtr pFileHeadBuf, uint nSize, uint nBufPoolSize);

        /// <summary>
        /// Close stream
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_CloseStream(int nPort);

        /// <summary>
        /// Set stream open mode
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_SetStreamOpenMode(int nPort, uint nMode);

        #endregion

        #region Playback Control

        /// <summary>
        /// Start play
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_Play(int nPort, IntPtr hWnd);

        /// <summary>
        /// Stop play
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_Stop(int nPort);

        /// <summary>
        /// Pause play
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_Pause(int nPort, uint nPause);

        /// <summary>
        /// Play one frame
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_OneByOne(int nPort);

        #endregion

        #region Data Input

        /// <summary>
        /// Input data to player
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_InputData(int nPort, IntPtr pBuf, uint nSize);

        #endregion

        #region Speed Control

        /// <summary>
        /// Set play speed
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_SetPlaySpeed(int nPort, float fCoff);

        /// <summary>
        /// Set play direction
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_SetPlayDirection(int nPort, uint emDirection);

        #endregion

        #region Buffer Management

        /// <summary>
        /// Reset buffer
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_ResetBuffer(int nPort, uint nBufType);

        /// <summary>
        /// Get buffer value
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern uint PLAY_GetBufferValue(int nPort, uint nBufType);

        /// <summary>
        /// Get source buffer remain
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern uint PLAY_GetSourceBufferRemain(int nPort);

        #endregion

        #region Picture Capture

        /// <summary>
        /// Capture picture (extended)
        /// </summary>
        [DllImport(DLL_NAME, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_CatchPicEx(int nPort, string sFileName, PicFormats ePicfomat);

        /// <summary>
        /// Get picture as BMP (extended)
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_GetPicBMPEx(
            int nPort,
            IntPtr pBmpBuf,
            uint dwBufSize,
            ref uint pBmpSize,
            int nWidth,
            int nHeight,
            int nRgbType);

        #endregion

        #region Query Information

        /// <summary>
        /// Query information
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_QueryInfo(int nPort, int cmdType, IntPtr buf, int buflen, ref int returnlen);

        #endregion

        #region Rendering

        /// <summary>
        /// Register draw callback
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_RigisterDrawFun(int nPort, DrawFun DrawFuncb, IntPtr pUserData);

        /// <summary>
        /// Render private data (for intelligent analysis)
        /// </summary>
        [DllImport(DLL_NAME, CallingConvention = CallingConvention.StdCall)]
        public static extern bool PLAY_RenderPrivateData(int nPort, bool bTrue, int nReserve);

        #endregion
    }
}
