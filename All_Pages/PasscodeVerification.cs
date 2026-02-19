using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class PasscodeVerification
    {
        private AndroidDriver driver;
        private WebDriverWait wait;

        public PasscodeVerification(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Click any digit on passcode keypad (0–9)
        public void ClickDigit(string digit)
        {
            var digitBtn = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    MobileBy.AndroidUIAutomator(
                        $"new UiSelector().className(\"android.widget.Button\").text(\"{digit}\")"
                    )
                )
            );

            digitBtn.Click();
            Console.WriteLine($"Digit {digit} clicked.");
        }

        // Enter full passcode (e.g., "0000" or "1234")
        public void EnterPasscode(string passcode)
        {
            foreach (char digit in passcode)
            {
                ClickDigit(digit.ToString());
            }

            Console.WriteLine("Passcode entered: " + passcode);
        }

        // Clear previous passcode and enter new passcode
        public void ClearAndEnterPasscode(string newPasscode)
        {
            Console.WriteLine("Clearing previous passcode...");

            // Assuming a "clear" button exists on the keypad with resource-id "btnClear"
            try
            {
                var clearBtn = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.Id("com.yappakistan.app.stage:id/btnClear")
                    )
                );
                clearBtn.Click();
                Console.WriteLine("Previous passcode cleared.");
            }
            catch
            {
                // If no clear button, just continue and overwrite digits
                Console.WriteLine("No clear button found, will overwrite digits.");
            }

           // Thread.Sleep(500); // small delay to let UI update
            EnterPasscode(newPasscode);
        }

        // Click Sign In button
        public void ClickSignInButton()
        {
            var signInBtn = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("com.yappakistan.app.stage:id/btnSignIn")
                )
            );

            signInBtn.Click();
            Console.WriteLine("Sign In button clicked.");
        }

        // Click "No, thanks" button
        public void ClickNoThanksButton()
        {
            try
            {
                var noThanksBtn = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.Id("com.yappakistan.app.stage:id/tvNoThanks")
                    )
                );

                noThanksBtn.Click();
                Console.WriteLine("\"No, thanks\" button clicked.");
            }
            catch
            {
                Console.WriteLine("\"No, thanks\" button not found, skipping.");
            }
        }

        // Click "Yes, keep me updated" confirmation button
        public void ClickConfirmationButton()
        {
            try
            {
                var confirmBtn = wait.Until(
                    ExpectedConditions.ElementToBeClickable(
                        By.Id("com.yappakistan.app.stage:id/btnConfirmation")
                    )
                );

                confirmBtn.Click();
                Console.WriteLine("\"Yes, keep me updated\" button clicked.");
            }
            catch
            {
                Console.WriteLine("Confirmation button not found, skipping.");
            }
        }
    }
}
