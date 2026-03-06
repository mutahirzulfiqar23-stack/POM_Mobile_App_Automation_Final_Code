using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class AddDevice
    {
        private AndroidDriver driver;
        private WebDriverWait wait;

        // Constructor
        public AddDevice(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locator
        private By DelinkButtons = By.Id("com.yappakistan.app.stage:id/tvRemove");
        // Locator using UiAutomator (Recommended)
        private By ActiveButton = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvRemove\").text(\"Active\")"
        );

        // Locators
        private By HelpAndSupport = By.Id("com.yappakistan.app.stage:id/tvHelp");
        private By ViewFaqsIcon = By.Id("com.yappakistan.app.stage:id/ivViewFaqs");

        // Locator
        private By WhatsappChatIcon = By.Id("com.yappakistan.app.stage:id/ivViewWhatsappChat");


        // Locator
        private By CrossIcon = By.Id("com.yappakistan.app.stage:id/ivCross");

        // Locator
        private By LeftIcon = By.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        // Click Left Icon
        public void ClickLeftIcon()
        {
            wait.Until(d => d.FindElement(LeftIcon).Displayed);
            driver.FindElement(LeftIcon).Click();
        }

        // Click Cross Icon
        public void ClickCrossIcon()
        {
            wait.Until(d => d.FindElement(CrossIcon).Displayed);
            driver.FindElement(CrossIcon).Click();
        }

        // Normal Click Method (Recommended First)
        public void ClickWhatsappChat()
        {
            wait.Until(d => d.FindElement(WhatsappChatIcon).Displayed);
            driver.FindElement(WhatsappChatIcon).Click();
        }


        // Click Help and Support
        public void ClickHelpAndSupport()
        {
            wait.Until(d => d.FindElement(HelpAndSupport).Displayed);
            driver.FindElement(HelpAndSupport).Click();
        }

        // Click View FAQs Icon
        public void ClickViewFaqs()
        {
            wait.Until(d => d.FindElement(ViewFaqsIcon).Displayed);
            driver.FindElement(ViewFaqsIcon).Click();
        }

        public void ClickActiveButton()
        {
            wait.Until(d => d.FindElement(ActiveButton).Displayed);
            driver.FindElement(ActiveButton).Click();
        }


        // Click third Delink button
        public void ClickThirdDelinkButton()
        {
            wait.Until(d => d.FindElements(DelinkButtons).Count >= 3);

            var delinkList = driver.FindElements(DelinkButtons);

            if (delinkList.Count >= 3)
            {
                delinkList[2].Click();   // third element
            }
            else
            {
                throw new Exception("Less than 3 Delink buttons found.");
            }

        }
    }
}