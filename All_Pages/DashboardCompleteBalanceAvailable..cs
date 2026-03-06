using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class DashboardCompleteBalanceAvailable
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        public DashboardCompleteBalanceAvailable(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        // ---------- Transactions Filter ----------
        private By filterBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().textContains(\"Filter\")"
            );

        public void ClickTransactionsFilter()
        {
            IWebElement filter = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(filterBy);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            filter.Click();
        }

        // ---------- Incoming Transactions Checkbox ----------
        private By cbIncomingBy =
            MobileBy.Id("com.yappakistan.app.stage:id/cbIncoming");

        public void ClickIncomingTransactions()
        {
            IWebElement incoming = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(cbIncomingBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            incoming.Click();
        }

        // ---------- Outgoing Transactions Checkbox ----------
        private By cbOutgoingBy =
            MobileBy.Id("com.yappakistan.app.stage:id/cbOutgoing");

        public void ClickOutgoingTransactions()
        {
            IWebElement outgoing = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(cbOutgoingBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            outgoing.Click();
        }
        // ---------- Apply Filters Button ----------
        private By btnApplyFilterBy =
            MobileBy.Id("com.yappakistan.app.stage:id/btnApplyFilter");

        public void ClickApplyFiltersButton()
        {
            IWebElement applyFilterBtn = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnApplyFilterBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            applyFilterBtn.Click();
        }
        // ---------- Drawer Content View ----------
        private By drawerContentBy =
            MobileBy.XPath(
                "//androidx.drawerlayout.widget.DrawerLayout[@resource-id='com.yappakistan.app.stage:id/drawerLayout']" +
                "/android.view.ViewGroup/android.view.ViewGroup[1]"
            );

        public void ClickDrawerContent()
        {
            IWebElement drawerContent = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(drawerContentBy);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            drawerContent.Click();
        }
        // ---------- Left / Cross Back Button ----------
        private By crossBackButtonBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void ClickOnCrossBackButton()
        {
            IWebElement backBtn = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(crossBackButtonBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            backBtn.Click();
        }
        //public void ScrollAndClickSecondTransaction()
        //{
        //    // Step 1: Scroll until at least one transaction icon is visible
        //    driver.FindElement(
        //        MobileBy.AndroidUIAutomator(
        //            "new UiScrollable(new UiSelector().scrollable(true))" +
        //            ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivTransaction\"))"
        //        )
        //    );

        //    // Step 2: Wait until at least 2 transaction icons are present
        //    var transactionIcons = wait.Until(d =>
        //    {
        //        var elements = d.FindElements(ivTransactionBy);
        //        return elements.Count >= 2 ? elements : null;
        //    });

        //    // Step 3: Click the 2nd transaction icon (instance 1)
        //    transactionIcons[1].Click();
        //}
        // ---------- Transaction Item (lyTransaction) ----------
        private By lyTransactionBy = MobileBy.Id("com.yappakistan.app.stage:id/lyTransaction");

        public void ScrollAndClickSecondTransactionItem()
        {
            // Step 1: Scroll until at least one transaction item is visible
            driver.FindElement(MobileBy.AndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/lyTransaction\"))"
            ));

            // Step 2: Wait until at least 2 transaction items are present
            var transactionItems = wait.Until(d =>
            {
                var elements = d.FindElements(lyTransactionBy);
                return elements.Count >= 2 ? elements : null;
            });

            // Step 3: Click the 2nd transaction item (instance 1)
            transactionItems[1].Click();
        }
        // ---------- Amount Input: Enter 200 ----------
        // ---------- Amount Input Field ----------
        private By etAmountBy =
            MobileBy.Id("com.yappakistan.app.stage:id/etAmount");
        public void EnterAmount5()
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
            amountField.SendKeys("5");  // Enter 200
        }
        // ---------- Pay / Confirm Button ----------
        private By btnConfirmBy =
            MobileBy.Id("com.yappakistan.app.stage:id/btnConfirm");

        public void ScrollAndClickPayButton()
        {
            // Step 1: Scroll until the Pay button is visible
            driver.FindElement(
                MobileBy.AndroidUIAutomator(
                    "new UiScrollable(new UiSelector().scrollable(true))" +
                    ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnConfirm\"))"
                )
            );

            // Step 2: Wait until the element is ready
            IWebElement payButton = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnConfirmBy);
                    return el.Displayed && el.Enabled ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            // Step 3: Click the Pay button
            payButton.Click();
        }

        // ---------- OTP Input Fields ----------
        private By otpBoxesBy =
            MobileBy.XPath(
                "//android.widget.FrameLayout[@resource-id='com.yappakistan.app.stage:id/otpView']/android.widget.TextView"
            );

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
        // ---------- Verify / Proceed Next Button ----------
        private By btnProceedNextBy = MobileBy.Id("com.yappakistan.app.stage:id/proceedNext");

        public void ClickProceedNextButton()
        {
            // Wait until the button is visible and enabled
            IWebElement proceedNextBtn = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnProceedNextBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            // Scroll into view if necessary
            driver.FindElement(
                MobileBy.AndroidUIAutomator(
                    "new UiScrollable(new UiSelector().scrollable(true))" +
                    ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/proceedNext\"))"
                )
            );

            proceedNextBtn.Click();
        }
        // ---------- Core View Element ----------
        private By coreViewBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/coreView\").instance(1)"
        );

        public void ClickCoreView()
        {
            // Scroll into view first (if off-screen)
            driver.FindElement(
                MobileBy.AndroidUIAutomator(
                    "new UiScrollable(new UiSelector().scrollable(true))" +
                    ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/coreView\").instance(1))"
                )
            );

            // Wait until the element is displayed and enabled
            IWebElement coreViewElement = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(coreViewBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            coreViewElement.Click();
        }



        // ---------- Go To Dashboard Button ----------
        private By btnGoToDashboardBy = MobileBy.Id("com.yappakistan.app.stage:id/btnGoToDashboard");

        public void ClickGoToDashboard()
        {
            // Wait until the button is visible and enabled
            IWebElement goToDashboardBtn = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnGoToDashboardBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            // Click the button
            goToDashboardBtn.Click();
        }




    }


}

