using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class TextCompare
    {
        private readonly AndroidDriver driver; // non-generic
        private readonly WebDriverWait wait;

        // Constructor
        public TextCompare(AndroidDriver driver)
        {
            if (driver == null)
                throw new ArgumentNullException(nameof(driver));

            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
        }

        // Locator for the error TextView
        private By errorMessageBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvError\")"
        );

        // Method to get the text of the error message
        public string GetErrorMessage()
        {
            try
            {
                var errorElement = wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(errorMessageBy);
                        return el.Displayed ? el : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });

                return errorElement?.Text;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Error element did not appear within 30 seconds.");
                return null;
            }
        }

        // Method to compare the error message with expected text
        public bool IsErrorMessageDisplayed(string expectedText)
        {
            string actualText = GetErrorMessage();
            if (actualText == null)
            {
                Console.WriteLine("Error element not found!");
                return false;
            }

            bool match = actualText.Equals(expectedText, StringComparison.OrdinalIgnoreCase);
            Console.WriteLine($"Expected: '{expectedText}', Actual: '{actualText}', Match: {match}");
            return match;
        }
        // Locator for the snackbar TextView
        private By snackbarTextBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/snackbar_text\")"
        );

        // Method to get the snackbar text
        public string GetSnackbarText()
        {
            try
            {
                var snackbarElement = wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(snackbarTextBy);
                        return el.Displayed ? el : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });

                return snackbarElement?.Text;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Snackbar did not appear within 30 seconds.");
                return null;
            }
        }

        // Method to assert snackbar text
        // Throws an exception if text does not match
        public void AssertSnackbarText(string expectedText)
        {
            string actualText = GetSnackbarText();
            if (actualText == null)
            {
                throw new Exception("Snackbar element not found!");
            }

            if (!actualText.Equals(expectedText, StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception($"Snackbar text did not match! Expected: '{expectedText}', Actual: '{actualText}'");
            }

            Console.WriteLine($"Snackbar text matched successfully: '{actualText}'");
        }
    }

}

