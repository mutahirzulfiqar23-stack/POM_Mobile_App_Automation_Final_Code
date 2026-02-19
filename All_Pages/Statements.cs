using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class Statements
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        // Constructor
        public Statements(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // ============================================================
        // Locator for View Financial Year Statements Button
        // Resource ID:
        // com.yappakistan.app.stage:id/tvViewFinancialYearStatements
        // ============================================================

        private By viewFinancialYearBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvViewFinancialYearStatements\").text(\"View\")"
        );

        // ============================================================
        // Click View Button
        // ============================================================

        public void ClickViewFinancialYear()
        {
            var viewButton = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(viewFinancialYearBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            viewButton.Click();
        }
        // ============================================================
        // 1️⃣ Locator - Left Icon (Back Button)
        // Resource ID: com.yappakistan.app.stage:id/ivLeftIcon
        // ============================================================

        private By leftIconBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivLeftIcon\")"
        );

        public void ClickLeftIcon()
        {
            var leftIcon = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(leftIconBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            leftIcon.Click();
        }

        // ============================================================
        // 2️⃣ Locator - View Year To Date Statements
        // Resource ID: com.yappakistan.app.stage:id/tvViewYearToDateStatements
        // ============================================================

        private By viewYearToDateBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvViewYearToDateStatements\").text(\"View\")"
        );

        public void ClickViewYearToDate()
        {
            var viewButton = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(viewYearToDateBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            viewButton.Click();
        }
        private By viewCustomDateBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvViewCustomDateStatements\").text(\"View\")"
);
        public void ClickViewCustomDate()
        {
            var viewButton = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(viewCustomDateBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            viewButton.Click();
        }
        private By viewStatementsBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvViewStatements\").text(\"View\")"
);
        public void ClickViewStatements()
        {
            var viewButton = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(viewStatementsBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            viewButton.Click();
        }
        private By previousYearBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivPreviousYear\")"
        );

        public void ClickPreviousYear()
        {
            var previousYearIcon = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(previousYearBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            previousYearIcon.Click();
        }


    }
}
