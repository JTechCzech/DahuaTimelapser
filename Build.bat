@echo off
echo Building PlayBackCSharp...
dotnet build PlayBackCSharp.sln -c Release
if %errorlevel% == 0 (
    echo.
    echo Build successful!
    echo Output: bin\Release\net8.0-windows\PlayBackCSharp.exe
) else (
    echo.
    echo Build failed!
)
pause
