@echo off
setlocal


set INPUT_FILE=%1
set OUTPUT_DIR=%2

cd /d D:\Tool

echo Input : %INPUT_FILE%
echo Output: %OUTPUT_DIR%


set TMP1=%OUTPUT_DIR%\step1.json
set TMP2=%OUTPUT_DIR%\step2.json
set OUT=%OUTPUT_DIR%\result.json

convert.exe save "%INPUT_FILE%" "%TMP1%"
if errorlevel 1 exit /b 1

convert.exe process "%TMP1%" "%TMP2%"
if errorlevel 1 exit /b 1

convert.exe export "%TMP2%" "%OUT%"
if errorlevel 1 exit /b 1

echo Convert Done