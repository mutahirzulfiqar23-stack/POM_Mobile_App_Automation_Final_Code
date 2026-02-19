using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class SignIn
    {
        private AndroidDriver driver;

        public SignIn(AndroidDriver driver)
        {
            this.driver = driver;
        }


        // Enter any mobile number
        public void EnterMobileNumber(string mobileNumber)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var mobileInput = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("com.yappakistan.app.stage:id/etMobileNumber")
                )
            );

            mobileInput.Click();
            mobileInput.Clear();
            mobileInput.SendKeys(mobileNumber);

            Console.WriteLine("Mobile number entered: " + mobileNumber);
        }

        // Toggle the Remember switch twice
        public void DoubleClickRememberSwitch()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var rememberSwitch = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("com.yappakistan.app.stage:id/swRemember")
                )
            );

            rememberSwitch.Click();
            Thread.Sleep(100);
            //rememberSwitch.Click();

            Console.WriteLine("Remember ID switch toggled twice.");
        }

        // Scroll (if needed) and click Sign In using UiScrollable
        public void ClickSignInButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Use Android UIAutomator scroll to bring Sign In button into view
            var signInBtn = wait.Until(d =>
                driver.FindElement(MobileBy.AndroidUIAutomator(
                    "new UiScrollable(new UiSelector().scrollable(true))" +
                    ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnLogin\"));"
                ))
            );

            signInBtn.Click();
            Console.WriteLine("Sign In button clicked.");
        }
        // Scroll (if needed) and click Sign In button after entering passcode
        public void ClickSignInButtonPassCode()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Scroll to the Sign In button using UiScrollable and click it
            var signInBtn = wait.Until(d =>
                driver.FindElement(MobileBy.AndroidUIAutomator(
                    "new UiScrollable(new UiSelector().scrollable(true))" +
                    ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnSignIn\"))"
                ))
            );

            signInBtn.Click();
            Console.WriteLine("Sign In button clicked after entering passcode.");
        }



        // Click the back button
        public void ClickBackButton()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            var backBtn = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("com.yappakistan.app.stage:id/ivBack")
                )
            );

            backBtn.Click();
            Console.WriteLine("Back button clicked.");
        }

        // Optional: Check for error message (non-registered number)
        public string GetErrorMessage()
        {
            try
            {
                var errorElement = driver.FindElement(By.Id("com.yappakistan.app.stage:id/tvError"));
                return errorElement.Text;
            }
            catch
            {
                return string.Empty;
            }
        }

        // Optional: Check if Sign In button is enabled
        public bool IsSignInButtonEnabled()
        {
            var signInBtn = driver.FindElement(By.Id("com.yappakistan.app.stage:id/btnLogin"));
            return signInBtn.Enabled;
        }
    }
}
