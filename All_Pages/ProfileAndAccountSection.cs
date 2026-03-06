using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class ProfileAndAccountSection
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        public ProfileAndAccountSection(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        // ================= LOCATORS =================

        private readonly By profileIcon =
            By.Id("com.yappakistan.app.stage:id/ivProfile");

        private readonly By viewPersonalDetails =
            By.Id("com.yappakistan.app.stage:id/tvViewPersonalDetails");

        private readonly By leftIcon =
            By.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        private readonly By referAFriendText =
          By.Id("com.yappakistan.app.stage:id/tvRefer");

        private readonly By leftIconc =
            By.Id("com.yappakistan.app.stage:id/ivLeftIcon");
        private readonly By shareButton =
         By.Id("com.yappakistan.app.stage:id/btnShare");

        private readonly By quickShareButton =
            By.Id("android:id/chooser_nearby_button");
        // First system button (used for Quick Share confirmation)
        private readonly By quickShareConfirmButton =
            By.XPath("//androidx.compose.ui.platform.ComposeView/android.view.View/android.view.View/android.view.View/android.view.View[1]/android.view.View[1]/android.view.View[1]/android.widget.Button");

        // Left/back icon in app (navigation)
        private readonly By backIcon =
            By.Id("com.yappakistan.app.stage:id/ivLeftIcon");
        private readonly By alertsOption =
           By.Id("com.yappakistan.app.stage:id/tvAlert");

        // Locator for the 5th notification item
        private readonly By fifthNotificationItem =
            By.XPath("(//android.widget.LinearLayout[@resource-id='com.yappakistan.app.stage:id/clNotificationItem'])[5]");

        // OK button on dialog
        private readonly By okButton = By.Id("android:id/button1");

        // Navigate up button (back) using MobileBy for accessibility id
        private readonly By navigateUpButton = MobileBy.AccessibilityId("Navigate up");


        // ================= LOCATOR =================
        private readonly By settingsIcon = By.Id("com.yappakistan.app.stage:id/ivSetting");


        private readonly By linkedDevicesView = By.Id("com.yappakistan.app.stage:id/tvLinkedDevicesView");




        // ================= ACTION METHODS =================

        public void ClickProfileIcon()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(profileIcon)).Click();
        }

        public void ClickViewPersonalDetails()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(viewPersonalDetails)).Click();
        }
        public void ClickLeftIcon()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(leftIcon)).Click();
        }

        private void ClickElement(By locator)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }
        private void ClickElements(By locator)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }
        private void Click(By locator)
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(locator)).Click();
        }
        public void ClickAlertsOption()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(alertsOption)).Click();
        }

        public void ClickFifthNotification()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(fifthNotificationItem)).Click();
        }
        public void ClickOkButton()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(okButton)).Click();
        }

        public void ClickNavigateUp()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(navigateUpButton)).Click();
        }
        public void ClickSettingsIcon()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(settingsIcon)).Click();
        }

        public void ClickLinkedDevicesView()
        {
            wait.Until(ExpectedConditions.ElementToBeClickable(linkedDevicesView)).Click();
        }
        // ================= ACTION METHODS =================

        /// <summary>
        /// Click on the "Refer a Friend" text
        /// </summary>
        public void ClickReferAFriend()
        {
            ClickElement(referAFriendText);
        }

        /// <summary>
        /// Click on the left/back icon
        /// </summary>
        public void ClickLeftIconc()
        {
            ClickElement(leftIcon);
        }
        public void ClickShareButton()
        {
            ClickElement(shareButton);
        }

        /// <summary>
        /// Click the "Quick Share" button in system chooser
        /// </summary>
        public void ClickQuickShareButton()
        {
            ClickElement(quickShareButton);
        }
        public void ClickQuickShareConfirmButton()
        {
            Click(quickShareConfirmButton);
        }

        /// <summary>
        /// Click the back/left navigation icon
        /// </summary>
        public void ClickBackIcon()
        {
            Click(backIcon);
        }
    }
}