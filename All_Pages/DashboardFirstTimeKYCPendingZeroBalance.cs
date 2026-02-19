using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class DashboardFirstTimeKYCPendingZeroBalance
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        public DashboardFirstTimeKYCPendingZeroBalance(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        // ---------- Balance Eye ----------
        private By ivBalanceEyeBy = MobileBy.Id("com.yappakistan.app.stage:id/ivBalanceEye");

        public void ClickOnBalanceEye()
        {
            IWebElement balanceEye = wait.Until(d =>
            {
                var el = d.FindElement(ivBalanceEyeBy);
                return el.Displayed ? el : null;
            });

            balanceEye.Click();
        }

        //// ---------- Left Icon ----------
        //private By ivLeftIconBy = MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        //public void ClickOnLeftIcon()
        //{
        //    IWebElement leftIcon = wait.Until(d =>
        //    {
        //        var el = d.FindElement(ivLeftIconBy);
        //        return el.Displayed && el.Enabled ? el : null;
        //    });

        //    leftIcon.Click();
        //}

        // ---------- Widget Icons (All) ----------
        private By widgetIconsBy = MobileBy.Id("com.yappakistan.app.stage:id/ivWidgetIcon");

        public void ClickFirstWidgetIcon()
        {
            var widgetIcons = wait.Until(d =>
            {
                var elements = d.FindElements(widgetIconsBy);
                return elements.Count >= 1 ? elements : null;
            });

            widgetIcons[0].Click(); // 1st widget icon
        }

        public void ClickSecondWidgetIcon()
        {
            var widgetIcons = wait.Until(d =>
            {
                var elements = d.FindElements(widgetIconsBy);
                return elements.Count >= 2 ? elements : null;
            });

            widgetIcons[1].Click(); // 2nd widget icon
        }

        public void ClickThirdWidgetIcon()
        {
            var widgetIcons = wait.Until(d =>
            {
                var elements = d.FindElements(widgetIconsBy);
                return elements.Count >= 3 ? elements : null;
            });

            widgetIcons[2].Click(); // 3rd widget icon
        }

        public void ClickFourthWidgetIcon()
        {
            var widgetIcons = wait.Until(d =>
            {
                var elements = d.FindElements(widgetIconsBy);
                return elements.Count >= 4 ? elements : null;
            });

            widgetIcons[3].Click(); // 4th widget icon
        }
        // ---------- Back Icon ----------
        private By ivBackBy = MobileBy.Id("com.yappakistan.app.stage:id/ivBack");

        public void ClickOnBackIcon()
        {
            IWebElement backIcon = wait.Until(d =>
            {
                var el = d.FindElement(ivBackBy);
                return el.Displayed && el.Enabled ? el : null;
            });

            backIcon.Click();
        }
        // ---------- Core View Icons ----------
        private By coreViewBy = MobileBy.Id("com.yappakistan.app.stage:id/coreView");

        public void ClickSecondCoreView()
        {
            var coreViews = wait.Until(d =>
            {
                var elements = d.FindElements(coreViewBy);
                return elements.Count >= 2 ? elements : null; // wait until at least 2 exist
            });

            coreViews[1].Click(); // 2nd element (instance 1)
        }
        //// ---------- Amount Input Field ----------
        //private By etAmountBy = MobileBy.Id("com.yappakistan.app.stage:id/etAmount");

        //public void EnterAndClearAmount200()
        //{
        //    IWebElement amountField = wait.Until(d =>
        //    {
        //        try
        //        {
        //            var el = d.FindElement(etAmountBy);
        //            return (el.Displayed && el.Enabled) ? el : null;
        //        }
        //        catch (StaleElementReferenceException)
        //        {
        //            return null;
        //        }
        //    });

        //    amountField.Click();          // Focus field
        //    amountField.Clear();          // Ensure empty
        //    amountField.SendKeys("200");  // Enter 200

        //    // Small pause to let UI register input
        //    System.Threading.Thread.Sleep(500);

        //    amountField.Clear();          // Clear 200
        //}

        // ---------- Left Icon ----------
        private By ivLeftIconBys = MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void ClickOnLeftIcons()
        {
            IWebElement leftIcon = wait.Until(d =>
            {
                var el = d.FindElement(ivLeftIconBys);
                return el.Displayed && el.Enabled ? el : null;
            });

            leftIcon.Click();
        }
        // ---------- Cross / Back Button ----------
        private By crossBackButtonBy = MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void ClickCrossBackButton()
        {
            IWebElement crossBackButton = wait.Until(d =>
            {
                var el = d.FindElement(crossBackButtonBy);
                return el.Displayed && el.Enabled ? el : null;
            });

            crossBackButton.Click();
        }
        //// ---------- Drawer Icon ----------
        //private By ivDrawerBy = MobileBy.Id("com.yappakistan.app.stage:id/ivDrawer");

        //public void ClickOnDrawerIcon()
        //{
        //    IWebElement drawerIcon = wait.Until(d =>
        //    {
        //        var el = d.FindElement(ivDrawerBy);
        //        return el.Displayed && el.Enabled ? el : null;
        //    });

        //    drawerIcon.Click();
        //}
        //// ---------- Statements ----------
        //private By tvStatementsBy =
        //    MobileBy.Id("com.yappakistan.app.stage:id/tvStatements");

        //public void ClickOnStatements()
        //{
        //    IWebElement statements = wait.Until(d =>
        //    {
        //        var el = d.FindElement(tvStatementsBy);
        //        return el.Displayed ? el : null;
        //    });

        //    statements.Click();
        //}
        // ---------- Statement List Item (First) ----------
        private By clItemMainBy =
            MobileBy.Id("com.yappakistan.app.stage:id/clItemMain");

        public void ClickFirstStatementItem()
        {
            IWebElement firstItem = wait.Until(d =>
            {
                var elements = d.FindElements(clItemMainBy);
                return elements.Count >= 1 ? elements[0] : null;
            });

            firstItem.Click();
        }
        // ---------- Amount Input Field ----------
        private By etAmountBy =
            MobileBy.Id("com.yappakistan.app.stage:id/etAmount");

        public void EnterAndClearAmount200()
        {
            IWebElement amountField = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(etAmountBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            amountField.Click();          // Focus
            amountField.Clear();          // Ensure empty
            amountField.SendKeys("2000000000000");  // Enter 200

            System.Threading.Thread.Sleep(500); // Let UI update

            amountField.Clear();          // Clear 200
        }


        // ---------- Image ----------
        private By ivImageBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivImage");

        public void ClickOnImage()
        {
            IWebElement image = wait.Until(d =>
            {
                var el = d.FindElement(ivImageBy);
                return el.Displayed ? el : null;
            });

            image.Click();
        }
        //// ---------- Left Navigation Icon ----------
        //private By ivLeftIconByc =
        //    MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        //public void ClickLeftNavigationIcon()
        //{
        //    IWebElement leftNavIcon = wait.Until(d =>
        //    {
        //        var el = d.FindElement(ivLeftIconByc);
        //        return el.Displayed && el.Enabled ? el : null;
        //    });

        //    leftNavIcon.Click();
        //}
        // ---------- Drawer Icon ----------
        private By ivDrawerBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivDrawer");

        public void ClickDrawerIcon()
        {
            IWebElement drawerIcon = wait.Until(d =>
            {
                var el = d.FindElement(ivDrawerBy);
                return el.Displayed && el.Enabled ? el : null;
            });

            drawerIcon.Click();
        }
        // ---------- Statements ----------
        private By tvStatementsBy =
            MobileBy.Id("com.yappakistan.app.stage:id/tvStatements");

        public void ClickStatements()
        {
            IWebElement statements = wait.Until(d =>
            {
                var el = d.FindElement(tvStatementsBy);
                return el.Displayed ? el : null;
            });

            statements.Click();
        }

        // ---------- Left Navigation Icon ----------
        private By ivLeftIconBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void ClickLeftNavigationIcon()
        {
            IWebElement leftNavIcon = wait.Until(d =>
            {
                var el = d.FindElement(ivLeftIconBy);
                return el.Displayed && el.Enabled ? el : null;
            });

            leftNavIcon.Click();
        }






    }
}
