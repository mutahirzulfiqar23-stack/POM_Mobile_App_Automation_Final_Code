using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using POM_Mobile_App_Automate_Stage.All_Pages;
using POM_Mobile_App_Automate_Stage.Utilities;
using SeleniumExtras.WaitHelpers;
using System;
using System.Threading;
using DriverManager = POM_Mobile_App_Automate_Stage.DriverSetup.WebDriver;

namespace POM_Mobile_App_Automate_Stage.CompleteMethod
{
    internal class SanityMain
    {
        private AndroidDriver driver = null!;
        private WebDriverWait wait = null!;
        private readonly bool keepAppOpen = true;

        // ─────────────────────────────────────────────────────────────────────
        //  HELPER: wait until element visible (page-load gate)
        // ─────────────────────────────────────────────────────────────────────
        private IWebElement WaitVisible(By locator) =>
            wait.Until(ExpectedConditions.ElementIsVisible(locator));

        // ─────────────────────────────────────────────────────────────────────
        //  HELPER: wait until element clickable
        // ─────────────────────────────────────────────────────────────────────
        private IWebElement WaitClickable(By locator) =>
            wait.Until(ExpectedConditions.ElementToBeClickable(locator));

        // ─────────────────────────────────────────────────────────────────────
        //  HELPER: shared login sequence (mobile + passcode + dismiss popups)
        // ─────────────────────────────────────────────────────────────────────
        private void Login(
            SignIn signInPage,
            PasscodeVerification passcode,
            string mobile,
            string pin,
            bool rememberMe = true)
        {
            // Wait for Sign-In screen to be ready
            WaitVisible(By.Id("com.yappakistan.app.stage:id/etMobileNumber"));

            signInPage.EnterMobileNumber(mobile);

            if (rememberMe)
                signInPage.DoubleClickRememberSwitch();

            signInPage.ClickSignInButton();

            // Passcode screen
            WaitVisible(By.Id("com.yappakistan.app.stage:id/etPasscode"));
            passcode.ClearAndEnterPasscode(pin);
            passcode.ClickSignInButton();

            // Dismiss optional biometric / confirmation popups
            try
            {
                WaitClickable(By.Id("com.yappakistan.app.stage:id/btnNoThanks"));
                passcode.ClickNoThanksButton();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No Thanks button not present – skipping.");
            }

            try
            {
                WaitClickable(By.Id("com.yappakistan.app.stage:id/btnConfirmation"));
                passcode.ClickConfirmationButton();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Confirmation button not present – skipping.");
            }

            // Gate: Dashboard must be visible before continuing
            WaitVisible(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
        }

        // ─────────────────────────────────────────────────────────────────────
        //  HELPER: shared logout sequence
        // ─────────────────────────────────────────────────────────────────────
        private void Logout(LogoutApp logout)
        {
            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            logout.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvLogout"));
            logout.PerformLogout();

            // Gate: Sign-In screen must return before next login
            WaitVisible(By.Id("com.yappakistan.app.stage:id/etMobileNumber"));
        }

        // ═════════════════════════════════════════════════════════════════════
        //  SETUP
        // ═════════════════════════════════════════════════════════════════════
        [SetUp]
        public void Setup()
        {
            DriverManager mainMethods = new DriverManager();

            driver = mainMethods.LaunchApp(
                "0A171JEC215267",
                "com.yappakistan.app.stage",
                "com.digitify.testyappakistan.onboarding.splash.SplashActivity"
            );

            if (driver == null)
                Assert.Fail("Failed to launch app.");

            // Initialise wait AFTER driver is confirmed non-null
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            Console.WriteLine("App launched successfully.");
            RecordingScript.StartRecording(driver);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  TEST
        // ═════════════════════════════════════════════════════════════════════
        [Test]
        public void CompleteSignInFlow()
        {
            // ── Initialise all page objects once ──────────────────────────────
            SignIn signInPage = new SignIn(driver);
            PasscodeVerification passcode = new PasscodeVerification(driver);
            ForgotPassword forgotPassword = new ForgotPassword(driver);
            FingerPrint fingerprint = new FingerPrint(driver);
            LogoutApp logout = new LogoutApp(driver);
            DashboardFirstTimeKYCPendingZeroBalance dashboard = new DashboardFirstTimeKYCPendingZeroBalance(driver);
            DashboardCompleteBalanceAvailable dashboardFull = new DashboardCompleteBalanceAvailable(driver);
            Young youngPage = new Young(driver);
            AddMoneyQRcode qrPage = new AddMoneyQRcode(driver);
            AddBenefiicary addBeneficiaryPage = new AddBenefiicary(driver);
            Statements statementsPage = new Statements(driver);
            TransactionFeed transactionFeedPage = new TransactionFeed(driver);
            TextCompare textCompare = new TextCompare(driver);
            ProfileAndAccountSection profile = new ProfileAndAccountSection(driver);
            AddDevice addDevice = new AddDevice(driver);
            GeneralUIUXtest uiux = new GeneralUIUXtest(driver);
            SecuritySection security = new SecuritySection(driver);
            AboutSection about = new AboutSection(driver);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 1 – Invalid login shows correct error message
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 1: Invalid login ===");

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etMobileNumber"));
            signInPage.EnterMobileNumber("3211111111");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etPasscode"));
            passcode.EnterPasscode("0987");
            signInPage.ClickSignInButtonPassCode();

            // Wait for error banner
            WaitVisible(By.Id("com.yappakistan.app.stage:id/tvErrorMessage"));
            string errorText = textCompare.GetErrorMessage();
            Console.WriteLine("Error message: " + errorText);

            bool isMatch = textCompare.IsErrorMessageDisplayed(
                "User not found for the specified user type");
            Assert.IsTrue(isMatch, "Expected error message not displayed for invalid login.");

            signInPage.ClickBackButton();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 2 – Valid login then logout
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 2: Valid login + logout ===");

            Login(signInPage, passcode, "3364646412", "3889");
            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 3 – Balance eye + Young tab
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 3: Balance eye + Young tab ===");

            Login(signInPage, passcode, "3364646412", "3889");

            WaitVisible(By.Id("com.yappakistan.app.stage:id/ivBalanceEye"));
            dashboard.ClickOnBalanceEye();

            youngPage.ClickYoungTab();
            youngPage.ClickMainTab();

            // First widget + left icon
            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickFirstWidgetIcon();
            dashboard.ClickOnLeftIcons();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 4 – Send Money: successful transaction
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 4: Send money – success ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickThirdWidgetIcon();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/rvStatements"));
            dashboard.ClickFirstStatementItem();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/coreView"));
            dashboardFull.ClickCoreView();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etAmount"));
            dashboardFull.EnterAmount5();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnPay"));
            dashboardFull.ScrollAndClickPayButton();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etOtp"));
            dashboardFull.EnterOtp("039167");
            dashboardFull.ClickProceedNextButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnGoToDashboard"));
            dashboardFull.ClickGoToDashboard();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/ivDrawer"));

            // ══════════════════════════════════════════════════════════════════
            // FLOW 5 – Send Money: insufficient balance / invalid recipient
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 5: Send money – insufficient balance ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickThirdWidgetIcon();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/rvStatements"));
            dashboard.ClickFirstStatementItem();
            dashboard.ClickOnImage();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etAmount"));
            dashboard.EnterAndClearAmount200();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            dashboard.ClickOnLeftIcons();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            dashboard.ClickOnLeftIcons();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivClose"));
            dashboard.ClickCrossBackButton();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 6 – Recent transactions list
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 6: Recent transactions ===");

            Login(signInPage, passcode, "3364646412", "3889");

            WaitVisible(By.Id("com.yappakistan.app.stage:id/rvTransactions"));
            dashboardFull.ScrollAndClickSecondTransactionItem();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            dashboard.ClickOnLeftIcons();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 7 – Add Money via QR (User 1 – save QR to gallery)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 7: QR code – User 1 ===");

            Login(signInPage, passcode, "3263855579", "0987");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickFirstWidgetIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivSecond"));
            qrPage.ClickSecondIvIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            qrPage.ClickLeftIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivFirst"));
            qrPage.ClickFirstIvIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnSaveGallery"));
            qrPage.ClickSaveToGallery();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            qrPage.TapBackButton();
            qrPage.TapNavigationBackButton();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 8 – Add Money via QR scan (User 2 – scan from library)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 8: QR code scan – User 2 ===");

            Login(signInPage, passcode, "3364646412", "3889");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickFirstWidgetIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivSecond"));
            qrPage.ClickSecondIvIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            qrPage.ClickLeftIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivFirst"));
            qrPage.ClickFirstIvIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnScan"));
            qrPage.TapScanButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnLibrary"));
            qrPage.TapLibraryButton();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/rvImages"));
            qrPage.SelectImageFromLibrary();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnConfirm"));
            qrPage.TapPhotoPickerConfirmButton();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etAmount"));
            qrPage.EnterAmount200();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnPay"));
            qrPage.ScrollAndTapPayButton();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etOtp"));
            dashboardFull.EnterOtp("039167");
            dashboardFull.ClickProceedNextButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnGoToDashboard"));
            dashboardFull.ClickGoToDashboard();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            qrPage.ClickLeftHeaderIcon();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/ivBalanceEye"));
            dashboard.ClickOnBalanceEye();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 9 – Add Beneficiary (invalid IBAN + valid IBAN + nickname)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 9: Add Beneficiary ===");

            Login(signInPage, passcode, "3364646412", "3889");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickThirdWidgetIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivThirdIcon"));
            addBeneficiaryPage.TapThirdIcon();

            // Use ClickAddBeneficiary when one or more beneficiaries already exist
            WaitClickable(By.Id("com.yappakistan.app.stage:id/fabAddBeneficiary"));
            addBeneficiaryPage.ClickAddBeneficiary();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/searchBank"));
            addBeneficiaryPage.SearchModelBankOnly();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBankLogo"));
            addBeneficiaryPage.ClickBankLogo();

            // Invalid account number – should display error
            WaitVisible(By.Id("com.yappakistan.app.stage:id/etAccountNumber"));
            addBeneficiaryPage.EnterBankNumber("00wq133211jh");
            addBeneficiaryPage.ScrollAndClickFindAccount();

            // Valid account number
            WaitVisible(By.Id("com.yappakistan.app.stage:id/etAccountNumber"));
            addBeneficiaryPage.EnterBankNumber("111112287788111");
            addBeneficiaryPage.ScrollAndClickFindAccount();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etNickname"));
            addBeneficiaryPage.EnterNickname("powr1@#");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnConfirm"));
            addBeneficiaryPage.ScrollAndClickConfirm();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etOtp"));
            dashboardFull.EnterOtp("039167");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnSendLater"));
            addBeneficiaryPage.ClickSendLater();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            addBeneficiaryPage.ClickLeftIcon();
            addBeneficiaryPage.ClickLeftIcon();

            // Verify beneficiary now visible in list
            WaitClickable(By.Id("com.yappakistan.app.stage:id/widgetContainer"));
            dashboard.ClickThirdWidgetIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivThirdIcon"));
            addBeneficiaryPage.TapThirdIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            addBeneficiaryPage.ClickLeftIcon();
            addBeneficiaryPage.ClickLeftIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 10 – Statements (all options)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 10: Statements ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            dashboard.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvStatements"));
            dashboard.ClickStatements();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewFinancialYear"));
            statementsPage.ClickViewFinancialYear();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            statementsPage.ClickLeftIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewYearToDate"));
            statementsPage.ClickViewYearToDate();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            statementsPage.ClickLeftIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewCustomDate"));
            statementsPage.ClickViewCustomDate();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            statementsPage.ClickLeftIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewStatements"));
            statementsPage.ClickViewStatements();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            statementsPage.ClickLeftIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvPreviousYear"));
            statementsPage.ClickPreviousYear();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            statementsPage.ClickLeftIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 11 – Transaction Feed + Bottom Navigation
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 11: Transaction feed + bottom nav ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivFilter"));
            transactionFeedPage.ClickFilterIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/amountSlider"));
            transactionFeedPage.ClickAmountSlider();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnApply"));
            transactionFeedPage.ClickApplyFilter();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/navYoung"));
            transactionFeedPage.ClickYoungNavigation();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/fabYapIt"));
            transactionFeedPage.ClickFabYapIt();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/fabYapIt"));
            transactionFeedPage.ClickFabYapIt();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/navItem"));
            transactionFeedPage.ClickNavBarItem();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/navMore"));
            transactionFeedPage.ClickMore();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/navHome"));
            transactionFeedPage.ClickHome();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 12 – Profile & Account: personal details + Refer a Friend
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 12: Profile & Account ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivProfile"));
            profile.ClickProfileIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewPersonalDetails"));
            profile.ClickViewPersonalDetails();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            profile.ClickLeftIcon();  // back from personal details

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            profile.ClickLeftIcon();  // back to dashboard

            // Open drawer again for Refer a Friend
            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            logout.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvReferAFriend"));
            profile.ClickReferAFriend();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnShare"));
            profile.ClickShareButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnQuickShare"));
            profile.ClickQuickShareButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnQuickShareConfirm"));
            profile.ClickQuickShareConfirmButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();

            // ── Alerts & Notifications ────────────────────────────────────────
            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            logout.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvAlerts"));
            profile.ClickAlertsOption();

            // "Navigate up" uses content-desc – requires MobileBy.AccessibilityId
            WaitClickable(MobileBy.AccessibilityId("Navigate up"));
            profile.ClickNavigateUp();

            // ── Settings / Linked Devices (view) ─────────────────────────────
            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            logout.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivSettings"));
            profile.ClickSettingsIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvLinkedDevices"));
            profile.ClickLinkedDevicesView();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();
            profile.ClickBackIcon();

            // ── Settings / Linked Devices (delink) ───────────────────────────
            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            logout.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivSettings"));
            profile.ClickSettingsIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvLinkedDevices"));
            profile.ClickLinkedDevicesView();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnActive"));
            addDevice.ClickActiveButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();
            profile.ClickBackIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 13 – Help & Support (FAQs + WhatsApp)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 13: Help & Support ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivDrawer"));
            logout.ClickDrawerIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvHelpAndSupport"));
            addDevice.ClickHelpAndSupport();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewFaqs"));
            addDevice.ClickViewFaqs();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivCross"));
            addDevice.ClickCrossIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvWhatsappChat"));
            addDevice.ClickWhatsappChat();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivLeftIcon"));
            addDevice.ClickLeftIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 14 – Profile picture update
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 14: Profile picture ===");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivProfile"));
            uiux.ClickProfile();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvViewPersonalDetails"));
            uiux.ClickViewPersonalDetails();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivAdd"));
            uiux.ClickAddIcon();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvChoosePhoto"));
            uiux.ClickChoosePhoto();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/rvImages"));
            qrPage.SelectImageFromLibrary();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnConfirm"));
            qrPage.TapPhotoPickerConfirmButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnUsePhoto"));
            GeneralUIUXtest generalUI = new GeneralUIUXtest(driver);
            generalUI.ClickUsePhoto();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 15 – Security: Change Passcode (user 3229009909)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 15: Change Passcode ===");

            Login(signInPage, passcode, "3229009909", "3022");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivProfile"));
            uiux.ClickProfile();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvChangePasscode"));
            security.ClickChangePasscode();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvForgotPin"));
            forgotPassword.ClickForgotSecurityPin();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etPin"));
            forgotPassword.CreateNewPin("3023");

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etConfirmPin"));
            forgotPassword.ConfirmNewPin("3023");

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etOtp"));
            forgotPassword.EnterOtp("039167");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnDone"));
            forgotPassword.ClickDoneButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 16 – About section: T&C + social media links
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 16: About section ===");

            Login(signInPage, passcode, "3364646412", "3889");

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivProfile"));
            uiux.ClickProfile();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/tvTermsAndConditions"));
            about.ClickViewTermsAndConditions();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivCross"));
            about.ClickCrossButton();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivTwitter"));
            about.ClickFollowTwitterAndReturn();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivInstagram"));
            about.ClickFollowInstagramAndReturn();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivFacebook"));
            about.ClickLikeUsAndReturn();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();

            Logout(logout);

            // ══════════════════════════════════════════════════════════════════
            // FLOW 17 – Biometric / Touch ID + Notifications switch
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 17: Biometric + Notifications ===");

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etMobileNumber"));
            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            WaitVisible(By.Id("com.yappakistan.app.stage:id/etPasscode"));
            passcode.ClearAndEnterPasscode("3889");
            passcode.ClickSignInButton();

            // Touch ID prompt appears BEFORE the NoThanks/Confirmation popups
            WaitClickable(By.Id("com.yappakistan.app.stage:id/btnUseTouchID"));
            fingerprint.ClickUseTouchID();

            // Dismiss remaining popups
            try
            {
                WaitClickable(By.Id("com.yappakistan.app.stage:id/btnNoThanks"));
                passcode.ClickNoThanksButton();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("No Thanks button not present – skipping.");
            }

            try
            {
                WaitClickable(By.Id("com.yappakistan.app.stage:id/btnConfirmation"));
                passcode.ClickConfirmationButton();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Confirmation button not present – skipping.");
            }

            WaitVisible(By.Id("com.yappakistan.app.stage:id/ivDrawer"));

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivProfile"));
            uiux.ClickProfile();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/switchNotifications"));
            fingerprint.ClickNotificationsSwitch();

            WaitClickable(By.Id("com.yappakistan.app.stage:id/ivBack"));
            profile.ClickBackIcon();

            Logout(logout);
        }

        // ═════════════════════════════════════════════════════════════════════
        //  TEARDOWN
        // ═════════════════════════════════════════════════════════════════════
        [TearDown]
        public void TearDown()
        {
            string testName = TestContext.CurrentContext.Test.Name;
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            RecordingScript.StopRecordingAndSave(testName);

            if (testStatus == TestStatus.Failed)
            {
                Console.WriteLine("[TearDown] Test FAILED – capturing screenshot.");
                RecordingFailedScreenshots.CaptureScreenshot(driver, testName);
            }

            if (!keepAppOpen)
            {
                if (driver != null)
                {
                    try { driver.Quit(); }
                    catch (Exception ex) { Console.WriteLine("Error quitting driver: " + ex.Message); }
                    finally
                    {
                        driver.Dispose();
                        driver = null!;
                    }
                }
            }
            else
            {
                Console.WriteLine("TearDown executed. Driver left open, app remains running.");
            }
        }
    }
}
