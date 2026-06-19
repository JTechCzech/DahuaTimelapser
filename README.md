# Dahua Timelapser

Dahua Timelapser is a C# application for creating timelapse videos from Dahua IP cameras.  
It works by reading continuous recordings from Dahua-compatible devices (including SD card storage) and extracting frames at defined intervals.

The extracted frames are saved locally and can later be exported either as image sequences or compiled into a video using built-in FFmpeg integration.

---

## Features

- Reads continuous Dahua camera recordings
- Extracts frames directly from recorded footage
- Saves frames locally to your PC
- Export options:
  - Image sequence (frames only)
  - Timelapse video (via integrated FFmpeg)
- Built-in FFmpeg pipeline for video encoding
- Works with Dahua protocol streams / recordings
- Optimized for fast sequential frame processing

---

## How It Works

The application connects to a Dahua camera or accesses its recorded stream (e.g., SD card recordings via protocol access).  
It then iterates through the recording timeline and extracts frames at user-defined intervals.

These frames are stored on disk and can later be:

- Kept as raw images for manual processing
- Or encoded into a timelapse video using FFmpeg integration

---

## Requirements

- Windows (recommended)
- .NET runtime (version depends on build)
- Access to Dahua camera or NVR
- FFmpeg (included in release or installed separately if building from source)

### For source builds

If you are not using the prebuilt release and are compiling the project yourself, you must also download and include the Dahua SDK dependencies.

You can download the required Dahua SDK files here:

[Google Drive](https://drive.google.com/drive/folders/1-FecPF9F4U4Xf044pnS_oHqJYukIlaRm?usp=sharing)

These files **must be placed in the `bin` folder** (next to the compiled executable), otherwise the application will not be able to load the required Dahua libraries at runtime.

Make sure the SDK libraries are correctly referenced and available at runtime, otherwise the application will not function properly.
