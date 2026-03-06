using System;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class RecordingFailedScreenshots
    {
        // ─────────────────────────────────────────────────────────────
        // CONFIGURATION
        // ─────────────────────────────────────────────────────────────

        // Local folder where failure screenshots are saved
        private const string LocalScreenshotFolder = @"D:\ScreenshotFailed";

        // ─────────────────────────────────────────────────────────────
        // PUBLIC API
        // ─────────────────────────────────────────────────────────────

        /// <summary>
        /// Captures a PNG screenshot and saves it to D:\ScreenshotFailed.
        /// Call this inside [TearDown] only when the test has failed.
        /// </summary>
        /// <param name="driver">The active AndroidDriver.</param>
        /// <param name="testName">Used to name the screenshot file.</param>
        public static void CaptureScreenshot(AndroidDriver driver, string testName)
        {
            if (driver == null)
            {
                Console.WriteLine("[RecordingFailedScreenshots] Driver is null – cannot capture screenshot.");
                return;
            }

            try
            {
                // Ensure the output folder exists
                Directory.CreateDirectory(LocalScreenshotFolder);

                // Build a unique, timestamped file name
                string safeTestName = SanitiseFileName(testName);
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string screenshotPath = Path.Combine(LocalScreenshotFolder,
                                                      $"FAILED_{safeTestName}_{timestamp}.png");

                // Take the screenshot
                Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();
                screenshot.SaveAsFile(screenshotPath);

                Console.WriteLine($"[RecordingFailedScreenshots] Screenshot saved to: {screenshotPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[RecordingFailedScreenshots] Failed to capture screenshot: {ex.Message}");
            }
        }

        /// <summary>
        /// Convenience overload: captures a screenshot with an optional extra label
        /// appended to the file name (e.g. the step that failed).
        /// </summary>
        public static void CaptureScreenshot(AndroidDriver driver, string testName, string stepLabel)
        {
            CaptureScreenshot(driver, $"{testName}_{stepLabel}");
        }

        // ─────────────────────────────────────────────────────────────
        // HELPERS
        // ─────────────────────────────────────────────────────────────

        private static string SanitiseFileName(string name)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c, '_');
            return name;
        }
    }
}
