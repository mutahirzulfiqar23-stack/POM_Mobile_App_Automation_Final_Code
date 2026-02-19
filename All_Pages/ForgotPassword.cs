using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class ForgotPassword
    {
        private AndroidDriver driver;
        private WebDriverWait wait;

        public ForgotPassword(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // ✅ Forgot PIN
        public void ClickForgotSecurityPin()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.Id("com.yappakistan.app.stage:id/tvForgotPassword")
            )).Click();
        }

        // ✅ Click keypad digit
        private void ClickDigit(string digit)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(
                MobileBy.AndroidUIAutomator(
                    $"new UiSelector().className(\"android.widget.Button\").text(\"{digit}\")"
                )
            )).Click();
        }

        // ✅ Enter PIN
        private void EnterPin(string pin)
        {
            foreach (char d in pin)
                ClickDigit(d.ToString());
        }

        // ✅ Create PIN
        public void CreateNewPin(string pin)
        {
            EnterPin(pin);
            ClickConfirmButton();
        }

        // ✅ Confirm PIN
        public void ConfirmNewPin(string pin)
        {
            EnterPin(pin);
            ClickConfirmButton();
        }

        // ✅ CLICK OTP BOX → TYPE OTP
        public void EnterOtp(string otp)
        {
            // 1️⃣ Tap OTP container
            var otpContainer = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.Id("com.yappakistan.app.stage:id/otpView")
            ));
            otpContainer.Click();

            // 2️⃣ Wait for keyboard + focus
            Thread.Sleep(700);

            // 3️⃣ Find the focused EditText
            var otpInput = wait.Until(d =>
            {
                try
                {
                    return d.FindElement(MobileBy.AndroidUIAutomator(
                        "new UiSelector().className(\"android.widget.EditText\").focused(true)"
                    ));
                }
                catch
                {
                    return null;
                }
            });

            if (otpInput == null)
                throw new NoSuchElementException("Focused OTP EditText not found");

            otpInput.SendKeys(otp);
        }

        // ✅ Confirm button
        public void ClickConfirmButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(
                By.Id("com.yappakistan.app.stage:id/btnPasscode")
            )).Click();
        }
       // Click "Done" button
                public void ClickDoneButton()
        {
            var doneBtn = wait.Until(ExpectedConditions.ElementToBeClickable(
                By.Id("com.yappakistan.app.stage:id/btnDone")
            ));
            doneBtn.Click();
            Console.WriteLine("\"Done\" button clicked.");
        }

    }
}
