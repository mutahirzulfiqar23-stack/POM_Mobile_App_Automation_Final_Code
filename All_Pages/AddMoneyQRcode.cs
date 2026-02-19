using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;



namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class AddMoneyQRcode
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        // Constructor
        public AddMoneyQRcode(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // ============================================================
        // 1️⃣ First ivIcon (instance(0)) → Click
        // UiAutomator Path:
        // new UiSelector().resourceId("com.yappakistan.app.stage:id/ivIcon").instance(0)
        // ============================================================

        private By firstIvIconBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivIcon\").instance(0)"
            );

        public void ClickFirstIvIcon()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(firstIvIconBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }

        // ============================================================
        // 2️⃣ Save To Gallery Button → Click
        // Resource ID:
        // com.yappakistan.app.stage:id/tvSaveToGallery
        // ============================================================

        private By saveToGalleryBy =
            MobileBy.Id("com.yappakistan.app.stage:id/tvSaveToGallery");

        public void ClickSaveToGallery()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(saveToGalleryBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }

        // ============================================================
        // Existing Methods (Kept As Is)
        // ============================================================

        private By secondIvIconBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivIcon\").instance(1)"
            );

        public void ClickSecondIvIcon()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(secondIvIconBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }

        private By leftIconBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void ClickLeftIcon()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(leftIconBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // 1️⃣ Back Button (ivBack)
        // ============================================================

        private By btnBackBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivBack");

        public void TapBackButton()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnBackBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }


        // ============================================================
        // 2️⃣ Navigation Back Button (ivLeftIcon)
        // ============================================================

        private By btnNavigationBackBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void TapNavigationBackButton()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnNavigationBackBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // Scan Button (ivScan)
        // Resource ID: com.yappakistan.app.stage:id/ivScan
        // ============================================================

        private By btnScanBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivScan");

        public void TapScanButton()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnScanBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // Library Button (ivLibrary)
        // Resource ID: com.yappakistan.app.stage:id/ivLibrary
        // ============================================================

        private By btnLibraryBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLibrary");

        public void TapLibraryButton()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnLibraryBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // Select Image From Photo Picker
        // UiAutomator:
        // new UiSelector().className("android.view.View").instance(24)
        // ============================================================

        private By photoPickerItemBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().className(\"android.view.View\").instance(24)"
            );

        public void SelectImageFromLibrary()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(photoPickerItemBy);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // Photo Picker Confirm Button
        // UiAutomator:
        // new UiSelector().className("android.widget.Button").instance(6)
        // ============================================================

        private By btnPhotoPickerConfirmBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().className(\"android.widget.Button\").instance(6)"
            );

        public void TapPhotoPickerConfirmButton()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnPhotoPickerConfirmBy);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // Amount Input Field
        // Resource ID: com.yappakistan.app.stage:id/etAmount
        // ============================================================

        private By txtAmountBy =
            MobileBy.Id("com.yappakistan.app.stage:id/etAmount");

        public void EnterAmount200()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(txtAmountBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();      // Focus on field
            element.Clear();      // Clear existing value
            element.SendKeys("200"); // Enter 200
        }

        // ============================================================
        // Scroll And Tap Pay Button
        // Resource ID: com.yappakistan.app.stage:id/btnConfirm
        // Text: Pay
        // ============================================================

        private By btnConfirmBy =
            MobileBy.Id("com.yappakistan.app.stage:id/btnConfirm");

        public void ScrollAndTapPayButton()
        {
            // Step 1: Scroll until Pay button is visible
            driver.FindElement(MobileBy.AndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnConfirm\"))"
            ));

            // Step 2: Wait until button is visible and enabled
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnConfirmBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            // Step 3: Click Pay button
            element.Click();
        }
        // ============================================================
        // OTP Field
        // Resource ID: com.yappakistan.app.stage:id/otpView
        // Enter OTP: 039167
        // ============================================================

        private By otpFieldBy =
            MobileBy.Id("com.yappakistan.app.stage:id/otpView");

        public void EnterOtp039167()
        {
            // Wait until OTP view is visible
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(otpFieldBy);
                    return el.Displayed ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            // Click OTP area (if needed)
            element.Click();

            // Send OTP directly
            element.SendKeys("039167");
        }

        // ============================================================
        // Header Back Icon (ivLeftIcon)
        // Resource ID: com.yappakistan.app.stage:id/ivLeftIcon
        // ============================================================

        private By imgHeaderBackBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void TapHeaderBackIcon()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(imgHeaderBackBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        // ============================================================
        // Left Header Icon (Back Button)
        // Resource ID: com.yappakistan.app.stage:id/ivLeftIcon
        // ============================================================

        private By leftHeaderIconBy =
            MobileBy.Id("com.yappakistan.app.stage:id/ivLeftIcon");

        public void ClickLeftHeaderIcon()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(leftHeaderIconBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });

            element.Click();
        }
        //// ============================================================
        //// Notification Toolbar (Swipeable)
        //// UiSelector: className("android.view.ViewGroup").instance(4)
        //// ============================================================

        //private By notificationToolbarBy =
        //    MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.view.ViewGroup\").instance(4)");

        //// ============================================================
        //// Notification Toolbar (Swipeable)
        //// UiSelector: className("android.view.ViewGroup").instance(4)
        //// ============================================================

        //private By notificationToolbarBys =
        //    MobileBy.AndroidUIAutomator("new UiSelector().className(\"android.view.ViewGroup\").instance(4)");

        //public void SwipeNotificationToRight()
        //{
        //    // Wait for the toolbar element
        //    var element = wait.Until(d =>
        //    {
        //        try
        //        {
        //            var el = d.FindElement(notificationToolbarBys);
        //            return el.Displayed ? el : null;
        //        }
        //        catch (StaleElementReferenceException)
        //        {
        //            return null;
        //        }
        //    });

        //    // Calculate swipe coordinates
        //    int startX = element.Location.X + 10; // start slightly inside the left edge
        //    int startY = element.Location.Y + element.Size.Height / 2; // vertical center
        //    int endX = element.Location.X + element.Size.Width - 10; // swipe almost to the right edge
        //    int endY = startY;

        //    // Perform swipe to the right
        //    new AppiumTouchAction((AndroidDriver)driver)
        //        .Press(startX, startY)
        //        .Wait(200) // milliseconds
        //        .MoveTo(endX, endY)
        //        .Release()
        //        .Perform();
        //}


    }
}
