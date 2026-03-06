using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class AboutSection
    {
        private AndroidDriver driver;
        private WebDriverWait wait;

        public AboutSection(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }
        // Locator for Cross (Close) button
        private By CrossButton = By.Id("com.yappakistan.app.stage:id/ivCross");

        // Method to click on Cross button
        public void ClickCrossButton()
        {
            IWebElement element = wait.Until(d => d.FindElement(CrossButton));

            if (element.Displayed && element.Enabled)
            {
                element.Click();
            }
            else
            {
                throw new Exception("Cross button is not clickable.");
            }
        }
        // Locator for "View Terms and Conditions"
        private By ViewTermsAndConditions =
            By.Id("com.yappakistan.app.stage:id/tvViewTermsAndConditions");

        // Method to click on "View"
        public void ClickViewTermsAndConditions()
        {
            IWebElement element = wait.Until(d => d.FindElement(ViewTermsAndConditions));

            if (element.Displayed && element.Enabled)
            {
                element.Click();
            }
            else
            {
                throw new Exception("View Terms and Conditions button is not clickable.");
            }
        }
        // Locator for Follow Instagram
        private By FollowInstagram =
            By.Id("com.yappakistan.app.stage:id/tvFollowInstagram");

        public void ClickFollowInstagramAndReturn()
        {
            // Click Follow Instagram
            IWebElement element = wait.Until(d => d.FindElement(FollowInstagram));
            element.Click();

            // Wait for external browser/app to open
            System.Threading.Thread.Sleep(5000);

            // Press Android Back button to close external tab/app
            driver.Navigate().Back();

            // Small wait to ensure we are back inside the app
            System.Threading.Thread.Sleep(3000);

            // OPTIONAL: Ensure we switch back to native context
            if (driver.Context != "NATIVE_APP")
            {
                driver.Context = "NATIVE_APP";
            }
        }

        // Locator for "Like us"
        private By LikeUsButton =
            By.Id("com.yappakistan.app.stage:id/tvLikeUs");

        public void ClickLikeUsAndReturn()
        {
            // Click Like Us
            IWebElement element = wait.Until(d => d.FindElement(LikeUsButton));
            element.Click();

            // Wait for external browser/app to open
            System.Threading.Thread.Sleep(5000);

            // Press Android Back button to close browser / external app
            driver.Navigate().Back();

            // Wait to ensure we are back inside the app
            System.Threading.Thread.Sleep(3000);

            // Ensure we are back in Native App context
            if (driver.Context != "NATIVE_APP")
            {
                driver.Context = "NATIVE_APP";
            }
        }
        // Locator for Follow Twitter
        private By FollowTwitter =
            By.Id("com.yappakistan.app.stage:id/tvFollowTwitter");

        public void ClickFollowTwitterAndReturn()
        {
            // Click Follow Twitter
            IWebElement element = wait.Until(d => d.FindElement(FollowTwitter));
            element.Click();

            // Wait for external app/browser to open
            System.Threading.Thread.Sleep(5000);

            // Switch back to Native App if context changed
            foreach (var context in driver.Contexts)
            {
                if (context.Contains("NATIVE"))
                {
                    driver.Context = context;
                    break;
                }
            }

            // Press device back button
            driver.Navigate().Back();

            // Optional small wait to ensure return to app
            System.Threading.Thread.Sleep(3000);
        }
    }
}
    
