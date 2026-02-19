using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class Young
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        public Young(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        // ---------- Young Tab ----------
        private By tvYoungBy =
            MobileBy.Id("com.yappakistan.app.stage:id/tvYoung");

        public void ClickYoungTab()
        {
            IWebElement youngTab = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(tvYoungBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            youngTab.Click();
        }

        // ---------- Main Tab ----------
        private By tvMainBy =
            MobileBy.Id("com.yappakistan.app.stage:id/tvMain");

        public void ClickMainTab()
        {
            IWebElement mainTab = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(tvMainBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            mainTab.Click();
        }
    }
}
