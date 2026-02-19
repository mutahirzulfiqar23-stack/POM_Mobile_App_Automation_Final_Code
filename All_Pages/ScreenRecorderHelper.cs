using OpenQA.Selenium.Appium.Android;
using System;
using System.IO;

namespace POM_Mobile_App_Automate_Stage.Utilities
{
    public static class ScreenRecorderHelper
    {
        private static readonly string VideoFolder = @"C:\ScreenshotFailed";

        public static void StartRecording(AndroidDriver driver)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            driver.StartRecordingScreen();
            Console.WriteLine("Screen recording started...");
        }

        public static void StopRecordingAndSave(AndroidDriver driver, string testName)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            if (!Directory.Exists(VideoFolder))
                Directory.CreateDirectory(VideoFolder);

            string base64Video = driver.StopRecordingScreen();

            byte[] videoBytes = Convert.FromBase64String(base64Video);

            string fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.mp4";
            string filePath = Path.Combine(VideoFolder, fileName);

            File.WriteAllBytes(filePath, videoBytes);

            Console.WriteLine($"Screen recording saved: {filePath}");
        }
    }
}
