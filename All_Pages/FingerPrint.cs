using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class FingerPrint
    {
        private readonly AndroidDriver driver;

        // Constructor
        public FingerPrint(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
        }

        // Locator
        private readonly By btnUseTouchID =
            By.Id("com.yappakistan.app.stage:id/btnConfirmation");

        // Click Method
        public void ClickUseTouchID()
        {
            driver.FindElement(btnUseTouchID).Click();
        }


        // =============================
        // Locators
        // =============================

        // App Notifications Switch
        private readonly By swNotifications =
            By.Id("com.yappakistan.app.stage:id/swNotifications");

        // Touch ID Authentication Switch
        private readonly By swAuthentication =
            By.Id("com.yappakistan.app.stage:id/swAuthentication");


        // =============================
        // Actions
        // =============================

        // Click App Notifications Switch
        public void ClickNotificationsSwitch()
        {
            driver.FindElement(swNotifications).Click();
        }

        // Click Touch ID Authentication Switch
        public void ClickAuthenticationSwitch()
        {
            driver.FindElement(swAuthentication).Click();
        }


        // =============================
        // Optional: Toggle Only If Needed
        // =============================

        // Enable/Disable Notifications safely
        public void SetNotifications(bool shouldBeEnabled)
        {
            var element = driver.FindElement(swNotifications);
            bool isChecked = element.GetAttribute("checked") == "true";

            if (isChecked != shouldBeEnabled)
            {
                element.Click();
            }
        }

        // Enable/Disable Touch ID safely
        public void SetAuthentication(bool shouldBeEnabled)
        {
            var element = driver.FindElement(swAuthentication);
            bool isChecked = element.GetAttribute("checked") == "true";

            if (isChecked != shouldBeEnabled)
            {
                element.Click();
            }
        }
    }
}