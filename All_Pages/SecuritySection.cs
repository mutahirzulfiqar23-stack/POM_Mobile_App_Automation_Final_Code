using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class SecuritySection
    {
        private AndroidDriver driver; // Non-generic
        private WebDriverWait wait;

        public SecuritySection(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locators
        private By MoreTab = By.XPath("//android.widget.TextView[@resource-id='com.yappakistan.app.stage:id/navigation_bar_item_small_label_view' and @text='More']");
        private By ChangePasscode = By.Id("com.yappakistan.app.stage:id/tvChangePasscode");
        private By PasscodeField1 = By.Id("com.yappakistan.app.stage:id/etNewPasscode");       // Replace with actual ID
        private By PasscodeField2 = By.Id("com.yappakistan.app.stage:id/etConfirmPasscode");   // Replace with actual ID

        // Tap the "More" tab (non-clickable)
        public void TapMoreTab()
        {
            wait.Until(d => d.FindElement(MoreTab).Displayed);
            IWebElement element = driver.FindElement(MoreTab);

            int centerX = element.Location.X + element.Size.Width / 2;
            int centerY = element.Location.Y + element.Size.Height / 2;

            // Use Actions API to tap at coordinates
            var actions = new Actions(driver);
            actions.MoveByOffset(centerX, centerY).Click().Perform();
            actions.MoveByOffset(-centerX, -centerY).Perform(); // reset offset
        }

        // Click "Change Passcode" button
        public void ClickChangePasscode()
        {
            wait.Until(d => d.FindElement(ChangePasscode).Displayed);
            driver.FindElement(ChangePasscode).Click();
        }

        // Enter new passcode twice
        public void EnterPasscode(string passcode)
        {
            wait.Until(d => d.FindElement(PasscodeField1).Displayed);
            driver.FindElement(PasscodeField1).SendKeys(passcode);

            wait.Until(d => d.FindElement(PasscodeField2).Displayed);
            driver.FindElement(PasscodeField2).SendKeys(passcode);
        }
    }
}