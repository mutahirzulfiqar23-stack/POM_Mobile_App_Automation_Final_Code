using OpenQA.Selenium.Appium.Android;
using System;
using System.IO;
using System.Collections.Generic;

namespace POM_Mobile_App_Automate_Stage.Utilities
{
    public static class ScreenRecorderHelper
    {
        private static readonly string VideoFolder = @"D:\ScreenshotFailed";

        public static void StartRecording(AndroidDriver driver)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            try
            {
                // Options for Appium recording
                var options = new Dictionary<string, object>
                {
                    { "timeLimit", "1800" }, // 30 min
                    { "videoSize", "1280x720" },
                    { "bitRate", 5000000 }
                };

                driver.ExecuteScript("mobile: startRecordingScreen", options);
                Console.WriteLine("🎬 Appium recording started...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error starting recording: " + ex.Message);
            }
        }

        public static void StopRecordingAndSave(AndroidDriver driver, string testName)
        {
            if (driver == null) return;

            try
            {
                var result = driver.ExecuteScript("mobile: stopRecordingScreen");
                if (result == null) return;

                string base64Video = result.ToString();
                if (string.IsNullOrEmpty(base64Video)) return;

                byte[] videoBytes = Convert.FromBase64String(base64Video);

                if (!Directory.Exists(VideoFolder))
                    Directory.CreateDirectory(VideoFolder);

                string filePath = Path.Combine(
                    VideoFolder,
                    $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.mp4"
                );

                File.WriteAllBytes(filePath, videoBytes);
                Console.WriteLine("✅ Video saved: " + filePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error stopping recording: " + ex.Message);
            }
        }
    }
}