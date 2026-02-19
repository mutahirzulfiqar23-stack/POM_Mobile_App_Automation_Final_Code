using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using System;
using System.IO;

namespace POM_Mobile_App_Automate_Stage.Utilities
{
    public static class ScreenshotHelper
    {
        private static readonly string ScreenshotFolder = @"C:\ScreenshotFailed";

        public static string CaptureScreenshot(AndroidDriver driver, string testName)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            if (!Directory.Exists(ScreenshotFolder))
                Directory.CreateDirectory(ScreenshotFolder);

            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string fileName = $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            string filePath = Path.Combine(ScreenshotFolder, fileName);

            // ✅ Selenium 4 way
            screenshot.SaveAsFile(filePath);

            Console.WriteLine($"Screenshot saved: {filePath}");

            return filePath;
        }
    }
}
