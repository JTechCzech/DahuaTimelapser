using System.Runtime.InteropServices;

namespace PlayBackCSharp.SDK
{
    /// <summary>
    /// Time structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct NET_TIME
    {
        public uint dwYear;      // Year
        public uint dwMonth;     // Month
        public uint dwDay;       // Date
        public uint dwHour;      // Hour
        public uint dwMinute;    // Minute
        public uint dwSecond;    // Second

        public DateTime ToDateTime()
        {
            try
            {
                return new DateTime((int)dwYear, (int)dwMonth, (int)dwDay,
                    (int)dwHour, (int)dwMinute, (int)dwSecond);
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        public static NET_TIME FromDateTime(DateTime dt)
        {
            return new NET_TIME
            {
                dwYear = (uint)dt.Year,
                dwMonth = (uint)dt.Month,
                dwDay = (uint)dt.Day,
                dwHour = (uint)dt.Hour,
                dwMinute = (uint)dt.Minute,
                dwSecond = (uint)dt.Second
            };
        }
    }

    /// <summary>
    /// Extended time structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct NET_TIME_EX
    {
        public uint dwYear;         // Year
        public uint dwMonth;        // Month
        public uint dwDay;          // Date
        public uint dwHour;         // Hour
        public uint dwMinute;       // Minute
        public uint dwSecond;       // Second
        public uint dwMillisecond;  // Millisecond
        public uint dwUTC;          // UTC
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public uint[] dwReserved;   // Reserved
    }

    /// <summary>
    /// Device information structure
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct NET_DEVICEINFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 48)]
        public byte[] sSerialNumber;    // Serial number
        public byte byAlarmInPortNum;   // Alarm input amount
        public byte byAlarmOutPortNum;  // Alarm output amount
        public byte byDiskNum;          // HDD amount
        public byte byDVRType;          // DVR type
        public byte byChanNum;          // Channel amount
    }

    /// <summary>
    /// Snapshot from record - input parameter
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct NET_IN_SNAP_PIC_FROM_REC
    {
        public uint dwSize;                     // struct size
        public uint nChannel;                   // channel
        public int nStreamType;                 // stream type. 0-main 1-extra
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] byReserved;               // Reserved
        public NET_TIME stuTime;                // snap picture time in the record

        public static NET_IN_SNAP_PIC_FROM_REC Create(uint channel, DateTime time, int streamType = 0)
        {
            return new NET_IN_SNAP_PIC_FROM_REC
            {
                dwSize = (uint)Marshal.SizeOf<NET_IN_SNAP_PIC_FROM_REC>(),
                nChannel = channel,
                nStreamType = streamType,
                byReserved = new byte[4],
                stuTime = NET_TIME.FromDateTime(time)
            };
        }
    }

    /// <summary>
    /// Snapshot from record - output parameter
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct NET_OUT_SNAP_PIC_FROM_REC
    {
        public uint dwSize;                     // struct size
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        public byte[] byReserved;               // Reserved
        public IntPtr pPicBuf;                  // picture buffer point, need user alloc memory
        public uint nBufLen;                    // picture buffer size, need user input
        public uint nRetLen;                    // picture actual size
    }

    /// <summary>
    /// Record file information
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct NET_RECORDFILE_INFO
    {
        public uint ch;                             // Channel number
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 124)]
        public byte[] filename;                     // File name (124 bytes)
        public uint framenum;                       // Total number of file frames
        public uint size;                           // File size in Kbytes
        public NET_TIME starttime;                  // Start time
        public NET_TIME endtime;                    // End time
        public uint driveno;                        // HDD number
        public uint startcluster;                   // Start cluster
        public byte nRecordFileType;                // Record type: 0-normal, 1-alarm, 2-motion, etc.
        public byte bImportantRecID;                // 0-normal, 1-important
        public byte bHint;                          // Document indexing
        public byte bRecType;                       // 0-main stream, 1-sub1, 2-sub2, 3-sub3

        // Helper property to get filename as string
        public string FileName => filename != null ? System.Text.Encoding.ASCII.GetString(filename).TrimEnd('\0') : string.Empty;
    }

    /// <summary>
    /// Multi-channel playback parameters
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct NET_MULTI_PLAYBACK_PARAM
    {
        public uint dwSize;                             // Structure size
        public IntPtr hWnd;                             // Window handle
        public NET_TIME stStartTime;                    // Start time
        public NET_TIME stStopTime;                     // Stop time
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        public int[] nChannelIDs;                       // Channel IDs
        public int nChannelCount;                       // Channel count
        public IntPtr fDownLoadDataCallBack;            // Download callback
        public IntPtr dwUserData;                       // User data
        public int nPlayDirection;                      // Play direction: 0-forward, 1-backward
        public int nWaittime;                           // Wait time
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public byte[] byReserved;                       // Reserved
    }

    /// <summary>
    /// Playback enums
    /// </summary>
    public enum PlayBackType
    {
        EM_FILEBACK,
        EM_TIMEBACK,
        EM_MULTYBACK
    }

    public enum PlayBackMode
    {
        EM_DIRECTMODE,
        EM_SERVERMODE
    }

    public enum PlayBackStream
    {
        EM_BOTH_STREAM,
        EM_MAIN_STREAM,
        EM_EXTRA_STREAM
    }

    public enum PlayBackDirection
    {
        EM_FORWARD,
        EM_BACKWARD
    }

    public enum PlaySpeed
    {
        SPEED_MIN = 0,
        SPEED_DOWN_8 = SPEED_MIN,   // 1/8 speed
        SPEED_DOWN_4,                // 1/4 speed
        SPEED_DOWN_2,                // 1/2 speed
        SPEED_NORMAL,                // Normal speed
        SPEED_UP_2,                  // 2x speed
        SPEED_UP_4,                  // 4x speed
        SPEED_UP_8,                  // 8x speed
        SPEED_MAX = SPEED_UP_8
    }

    public enum QueryType
    {
        EM_LISTALL,
        EM_ALARMLOCAL,
        EM_VIDEOMOTION
    }

    /// <summary>
    /// Device type constants
    /// </summary>
    public static class DeviceConstants
    {
        public const int DH_SERIALNO_LEN = 48;
        public const int MAX_CHANNUM = 16;
        public const int MAX_DISKNUM = 16;
        public const int MAX_ALARMIN = 128;
        public const int MAX_ALARMOUT = 64;
    }
}
