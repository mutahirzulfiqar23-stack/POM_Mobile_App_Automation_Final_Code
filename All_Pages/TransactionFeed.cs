using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class TransactionFeed
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        // Constructor
        public TransactionFeed(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // ============================================================
        // 1️⃣ Filter Icon
        // Resource ID: com.yappakistan.app.stage:id/ivFilter
        // ============================================================

        private By filterIconBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivFilter\")"
        );

        public void ClickFilterIcon()
        {
            var filterIcon = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(filterIconBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            filterIcon.Click();
        }

        // ============================================================
        // 2️⃣ Amount Range Slider
        // Resource ID: com.yappakistan.app.stage:id/rsbAmount
        // ============================================================

        private By amountSliderBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/rsbAmount\")"
        );

        public void ClickAmountSlider()
        {
            var amountSlider = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(amountSliderBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            try
            {
                // Try direct click
                amountSlider.Click();
            }
            catch
            {
                // If not clickable, click parent
                amountSlider.FindElement(By.XPath("..")).Click();
            }
    
        }

        private By applyFilterBtnBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnApplyFilter\").text(\"Apply filters\")"
);
        public void ClickApplyFilter()
        {
            var applyButton = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(applyFilterBtnBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            applyButton.Click();
        }
        private By youngNavBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/navigation_bar_item_small_label_view\").text(\"Young\")"
);
        public void ClickYoungNavigation()
        {
            var youngElement = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(youngNavBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            try
            {
                // Try direct click (just in case)
                youngElement.Click();
            }
            catch
            {
                // Click parent since TextView is not clickable
                youngElement.FindElement(By.XPath("..")).Click();
            }
        }
        // Locator for fabYapIt button
        private By fabYapItBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/fabYapIt\")"
        );

        // Method to click the button
        public void ClickFabYapIt()
        {
            // Wait until the button is visible and enabled
            var fabElement = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(fabYapItBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            // Click the button
            fabElement.Click();
        }
        private By navBarItemBy = MobileBy.AndroidUIAutomator(
        "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/navigation_bar_item_icon_view\").instance(3)"
    );

        // Method to click the navigation bar icon
        public void ClickNavBarItem()
        {
            var navBarElement = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(navBarItemBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            navBarElement.Click();
        }
        // Locator for the "More" label
        private By moreLabelBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().text(\"More\")"
        );

        // Method to click "More"
        public void ClickMore()
        {
            var moreElement = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(moreLabelBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (NoSuchElementException)
                {
                    return null;
                }
            });

            moreElement.Click();
        }
        // Locator for the "Home" TextView in the bottom navigation
        private By homeTextBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().text(\"Home\")"
        );

        // Method to click on "Home"
        public void ClickHome()
        {
            try
            {
                var homeElement = wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(homeTextBy);
                        return el.Displayed ? el : null;
                    }
                    catch (NoSuchElementException)
                    {
                        return null;
                    }
                });

                if (homeElement == null)
                    throw new Exception("Home element not found on the screen.");

                homeElement.Click();
                Console.WriteLine("Clicked on Home successfully.");
            }
            catch (WebDriverTimeoutException)
            {
                throw new Exception("Home element did not appear within 30 seconds.");
            }
        }
    }

}





