using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class AddBenefiicary
    {
        private readonly AndroidDriver driver;
        private readonly WebDriverWait wait;

        // Constructor
        public AddBenefiicary(AndroidDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        // ============================================================
        // 1️⃣ Third ivIcon (instance(2)) → Click
        // UiAutomator:
        // new UiSelector().resourceId("com.yappakistan.app.stage:id/ivIcon").instance(2)
        // ============================================================

        private By thirdIconBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivIcon\").instance(2)"
            );

        public void TapThirdIcon()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(thirdIconBy);
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
        // 2️⃣ Add Beneficiary Button → Click
        // Resource ID:
        // com.yappakistan.app.stage:id/btnAddBeneficiary
        // ============================================================

        private By btnAddBeneficiaryBy =
            MobileBy.Id("com.yappakistan.app.stage:id/btnAddBeneficiary");

        public void TapAddBeneficiaryButton()
        {
            var element = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(btnAddBeneficiaryBy);
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
        // 3️⃣ Search TextView → Click, Input "model bank", and Select
        // Resource ID:
        // com.yappakistan.app.stage:id/tvSearch
        // ============================================================

        private By searchTextViewBy =
            MobileBy.AndroidUIAutomator(
                "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvSearch\")"
            );

        public void SearchAndSelectWithKeyboard(string searchText)
        {
            // 1️⃣ Tap the Search TextView to open keyboard
            var searchTextView = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(searchTextViewBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch (StaleElementReferenceException)
                {
                    return null;
                }
            });
            searchTextView.Click();

            // 2️⃣ Send keys to the currently focused element
            var activeElement = driver.SwitchTo().ActiveElement();
            activeElement.Clear();
            activeElement.SendKeys(searchText);

            // Optional: hide keyboard after typing
            try
            {
                driver.HideKeyboard();
            }
            catch { /* ignore if already hidden */ }

            // 3️⃣ Wait for the dropdown suggestion and click it
            // Locate by resource-id (tvBankName) and exact text
            By dropdownItemBy = MobileBy.AndroidUIAutomator(
                $"new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvBankName\").text(\"{searchText}\")"
            );

            var dropdownItem = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(dropdownItemBy);
                    // Sometimes the TextView itself is not clickable, click parent
                    if (!el.Enabled || !el.Displayed || !el.Selected)
                    {
                        el = el.FindElement(MobileBy.XPath("..")); // go to parent layout
                    }
                    return el.Displayed && el.Enabled ? el : null;
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

            // 4️⃣ Click the dropdown item
            dropdownItem.Click();
        }
        // ============================================================
        // Scroll to Bank Logo and Click
        // Resource ID: com.yappakistan.app.stage:id/ivBankLogo
        // ============================================================

        private By bankLogoBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivBankLogo\")"
        );

        public void ScrollToAndClickBankLogo()
        {
            try
            {
                // Use UiScrollable to scroll through a scrollable parent
                string uiScrollable = "new UiScrollable(new UiSelector().scrollable(true))" +
                                      ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivBankLogo\"))";

                var element = driver.FindElement(MobileBy.AndroidUIAutomator(uiScrollable));

                // Wait until element is displayed and enabled
                wait.Until(d => element.Displayed && element.Enabled);

                // Click the bank logo
                element.Click();
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Bank logo not found on the screen.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while scrolling and clicking bank logo: {ex.Message}");
            }
        }

        // ============================================================
        // 1️⃣ Bank Account Input → Click, input value, clear, input again
        // Resource ID: com.yappakistan.app.stage:id/etBankNumber
        // ============================================================

        private By bankAccountInputBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/etBankNumber\")"
        );

        public void EnterBankAccount(string firstValue, string secondValue)
        {
            // Wait for the input field
            var inputElement = wait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(bankAccountInputBy);
                    return (el.Displayed && el.Enabled) ? el : null;
                }
                catch
                {
                    return null;
                }
            });

            // 1️⃣ Click to focus
            inputElement.Click();

            // 2️⃣ Input first value
            inputElement.Clear();
            inputElement.SendKeys(firstValue);

            // 3️⃣ Clear and input second value
            inputElement.Clear();
            inputElement.SendKeys(secondValue);
        }

        // ============================================================
        // 2️⃣ Toolbar Title → Verify text
        // Resource ID: com.yappakistan.app.stage:id/toolbarTitle
        // ============================================================

        private By toolbarTitleBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/toolbarTitle\")"
        );

        public bool VerifyToolbarTitle(string expectedText)
        {
            try
            {
                var titleElement = wait.Until(d =>
                {
                    try
                    {
                        var el = d.FindElement(toolbarTitleBy);
                        return el.Displayed ? el : null;
                    }
                    catch
                    {
                        return null;
                    }
                });

                return titleElement.Text.Equals(expectedText, StringComparison.OrdinalIgnoreCase);
            }
            catch
            {
                return false;
            }
        }


        // Locator for Model Bank
        // ============================================================

        private By modelBankBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvBankName\").text(\"Model Bank\")"
        );

        // ============================================================
        // Modern Scroll (W3C Action – Appium 2)
        // ============================================================

        // ============================================================
        // Scroll directly to "Model Bank" and click it
        // ============================================================

        // ============================================================
        // Scroll to "Model Bank" and select it
        // ============================================================

        public void ScrollAndSelectModelBank()
        {
            // Scroll until Model Bank is visible
            string uiScroll =
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector()" +
                ".resourceId(\"com.yappakistan.app.stage:id/tvBankName\")" +
                ".text(\"Model Bank\"));";

            var bankElement = wait.Until(d =>
                d.FindElement(MobileBy.AndroidUIAutomator(uiScroll))
            );

            // Click it (or its parent if not clickable)
            try
            {
                bankElement.Click();
            }
            catch
            {
                bankElement.FindElement(By.XPath("..")).Click();
            }
        }
        private By searchContentBy = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/flSearchContent\")"
        );

        public void SearchModelBankOnly()
        {
            // 1️⃣ Click search container
            var searchElement = wait.Until(d =>
            {
                var el = d.FindElement(searchContentBy);
                return (el.Displayed && el.Enabled) ? el : null;
            });

            searchElement.Click();

            // 2️⃣ Type "Model Bank"
            var activeElement = driver.SwitchTo().ActiveElement();
            activeElement.Clear();
            activeElement.SendKeys("Model Bank");

            // Optional: press Enter instead of hiding keyboard
            // activeElement.SendKeys(Keys.Enter);

            // OR hide keyboard if needed
            try
            {
                driver.HideKeyboard();
            }
            catch { }
        }
        ///Model Bank Click 
        private By modelBankTextBy = MobileBy.AndroidUIAutomator(
         "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/tvBankName\").text(\"Model Bank\")"
     );

        public void ClickModelBank()
        {
            // Wait until the element is visible
            var bankElement = wait.Until(d =>
            {
                var el = d.FindElement(modelBankTextBy);
                return (el.Displayed && el.Enabled) ? el : null;
            });

            try
            {
                // Try clicking the TextView directly
                bankElement.Click();
            }
            catch
            {
                // If not clickable, click its parent layout
                var parent = bankElement.FindElement(By.XPath(".."));
                parent.Click();
            }
        }
        private By bankLogoBye = MobileBy.AndroidUIAutomator(
            "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivBankLogo\")"
        );

        public void ClickBankLogo()
        {
            // Wait until the element is visible
            var logoElement = wait.Until(d =>
            {
                var el = d.FindElement(bankLogoBye);
                return (el.Displayed && el.Enabled) ? el : null;
            });

            try
            {
                // Try clicking the ImageView itself
                logoElement.Click();
            }
            catch
            {
                // If not clickable, click its parent layout
                var parent = logoElement.FindElement(By.XPath(".."));
                parent.Click();
            }
        }
        private By bankNumberBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/etBankNumber\")"
);
        public void EnterBankNumber(string bankNumber)
        {
            // 1️⃣ Scroll to the element if needed (optional)
            string uiScroll =
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/etBankNumber\"));";

            var bankNumberElement = wait.Until(d =>
            {
                return d.FindElement(MobileBy.AndroidUIAutomator(uiScroll));
            });

            // 2️⃣ Click the field to focus
            bankNumberElement.Click();

            // 3️⃣ Clear any existing text
            bankNumberElement.Clear();

            // 4️⃣ Enter the new bank number
            bankNumberElement.SendKeys(bankNumber);

            // Optional: hide keyboard
            try
            {
                driver.HideKeyboard();
            }
            catch { }
        }
        private By findAccountBtnBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnFindAccount\")"
);
        public void ScrollAndClickFindAccount()
        {
            // 1️⃣ Scroll until the button is visible
            string uiScroll =
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnFindAccount\"));";

            var buttonElement = wait.Until(d =>
                d.FindElement(MobileBy.AndroidUIAutomator(uiScroll))
            );

            // 2️⃣ Click the button
            buttonElement.Click();
        }
        private By nickNameBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/etAccountHolderNickName\")"
);
        public void EnterNickname(string nickname)
        {
            // 1️⃣ Scroll to the element if needed
            string uiScroll =
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/etAccountHolderNickName\"));";

            var nickElement = wait.Until(d =>
                d.FindElement(MobileBy.AndroidUIAutomator(uiScroll))
            );

            // 2️⃣ Click to focus
            nickElement.Click();

            // 3️⃣ Clear any existing text
            nickElement.Clear();

            // 4️⃣ Input the nickname
            nickElement.SendKeys(nickname);

            // Optional: hide keyboard
            try
            {
                driver.HideKeyboard();
            }
            catch { }
        }
        private By btnConfirmBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnAddBeneficiary\")"
);
        public void ScrollAndClickConfirm()
        {
            // 1️⃣ Scroll to the Confirm button
            string uiScroll =
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnAddBeneficiary\"));";

            var confirmButton = wait.Until(d =>
                d.FindElement(MobileBy.AndroidUIAutomator(uiScroll))
            );

            // 2️⃣ Click the button
            confirmButton.Click();
        }
        private By btnSendLaterBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/btnSendLater\")"
);
        public void ClickSendLater()
        {
            // Wait until the button is visible and enabled
            var sendLaterButton = wait.Until(d =>
            {
                var el = d.FindElement(btnSendLaterBy);
                return (el.Displayed && el.Enabled) ? el : null;
            });

            // Click the button
            sendLaterButton.Click();
        }
        private By addBeneficiaryBy = MobileBy.AndroidUIAutomator(
    "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/addBeneficiary\")"
);
        public void ClickAddBeneficiary()
        {
            // Wait until the element is visible and enabled
            var addButton = wait.Until(d =>
            {
                var el = d.FindElement(addBeneficiaryBy);
                return (el.Displayed && el.Enabled) ? el : null;
            });

            // Click the ImageView
            addButton.Click();
        }
        private By leftIconBy = MobileBy.AndroidUIAutomator(
     "new UiSelector().resourceId(\"com.yappakistan.app.stage:id/ivLeftIcon\")"
 );


        public void ClickLeftIcon()
        {
            // Wait until the element is visible and enabled
            var leftIcon = wait.Until(d =>
            {
                var el = d.FindElement(leftIconBy);
                return (el.Displayed && el.Enabled) ? el : null;
            });

            // Click the ImageView
            leftIcon.Click();
        }


    }
}



