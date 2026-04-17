@echo off
echo Copying Dahua SDK DLLs to output directory...
echo.

set SDK_PATH=..\..\..\Bin\Win64
set OUTPUT_DEBUG=bin\Debug\net8.0-windows
set OUTPUT_RELEASE=bin\Release\net8.0-windows

if not exist "%SDK_PATH%" (
    echo ERROR: SDK path not found: %SDK_PATH%
    pause
    exit /b 1
)

REM Create output directories if they don't exist
if not exist "%OUTPUT_DEBUG%" mkdir "%OUTPUT_DEBUG%"
if not exist "%OUTPUT_RELEASE%" mkdir "%OUTPUT_RELEASE%"

echo Copying to Debug...
copy "%SDK_PATH%\dhnetsdk.dll" "%OUTPUT_DEBUG%\" /Y
copy "%SDK_PATH%\dhplay.dll" "%OUTPUT_DEBUG%\" /Y
copy "%SDK_PATH%\*.dll" "%OUTPUT_DEBUG%\" /Y

echo.
echo Copying to Release...
copy "%SDK_PATH%\dhnetsdk.dll" "%OUTPUT_RELEASE%\" /Y
copy "%SDK_PATH%\dhplay.dll" "%OUTPUT_RELEASE%\" /Y
copy "%SDK_PATH%\*.dll" "%OUTPUT_RELEASE%\" /Y

echo.
echo DLLs copied successfully!
pause
