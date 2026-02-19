using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class LogoutApp
    {
        private AndroidDriver driver;
        private WebDriverWait wait;

        public LogoutApp(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // ✅ Click the drawer icon (hamburger menu)
        public void ClickDrawerIcon()
        {
            var drawerIcon = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("com.yappakistan.app.stage:id/ivDrawer")
                )
            );
            drawerIcon.Click();
            Console.WriteLine("Drawer icon clicked.");
        }

        // ✅ Click the Logout option in the drawer
        public void ClickLogout()
        {
            var logoutOption = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("com.yappakistan.app.stage:id/tvLogout")
                )
            );
            logoutOption.Click();
            Console.WriteLine("Logout option clicked.");
        }

        // ✅ Click Confirm button in the logout dialog
        public void ConfirmLogout()
        {
            var confirmBtn = wait.Until(
                ExpectedConditions.ElementToBeClickable(
                    By.Id("android:id/button1")
                )
            );
            confirmBtn.Click();
            Console.WriteLine("Logout confirmed.");
        }

        // ✅ Full logout flow
        public void PerformLogout()
        {
            ClickDrawerIcon();
            ClickLogout();
            ConfirmLogout();
        }
    }
}
