@echo off
REM =========================================================
REM Jenkins CI/CD Batch Script for C# Appium Tests
REM =========================================================

REM Navigate to project folder
cd C:\Users\Mutahir\source\repos\POM_Mobile_App_Automate_Stage

REM Clean old test results
if exist TestResults (
    del /Q TestResults\*.trx
    del /Q TestResults\*.xml
) else (
    mkdir TestResults
)

REM Optional: start Appium server (if needed)
REM "C:\Program Files\Appium\Appium.exe" --address 127.0.0.1 --port 4723

REM Run tests and generate TRX
dotnet test POM_Mobile_App_Automate_Stage.csproj --logger "trx;LogFileName=TestResults\test_results.trx"

REM Optional: convert TRX → JUnit XML for Jenkins (requires trx2junit)
REM trx2junit TestResults\test_results.trx -o TestResults\test_results.xml

pause