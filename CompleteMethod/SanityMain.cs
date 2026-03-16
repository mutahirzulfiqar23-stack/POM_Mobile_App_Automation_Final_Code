using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using POM_Mobile_App_Automate_Stage.All_Pages;
using POM_Mobile_App_Automate_Stage.Utilities;
using System;
using DriverManager = POM_Mobile_App_Automate_Stage.DriverSetup.WebDriver;

namespace POM_Mobile_App_Automate_Stage.CompleteMethod
{
    internal class SanityMain
    {
        private AndroidDriver driver = null!;
        private WebDriverWait wait = null!;
        private readonly bool keepAppOpen = true;

        // ─────────────────────────────────────────────────────────────────────
        // Page Objects — declared once, shared across all flows
        // ─────────────────────────────────────────────────────────────────────
        private SignIn signInPage = null!;
        private PasscodeVerification passcode = null!;
        private ForgotPassword forgotPassword = null!;
        private FingerPrint fingerprint = null!;
        private LogoutApp logout = null!;
        private DashboardFirstTimeKYCPendingZeroBalance dashboard = null!;
        private DashboardCompleteBalanceAvailable dashboardFull = null!;
        private Young youngPage = null!;
        private AddMoneyQRcode qrPage = null!;
        private AddBenefiicary addBeneficiaryPage = null!;
        private Statements statementsPage = null!;
        private TransactionFeed transactionFeedPage = null!;
        private TextCompare textCompare = null!;
        private ProfileAndAccountSection profile = null!;
        private AddDevice addDevice = null!;
        private GeneralUIUXtest uiux = null!;
        private SecuritySection security = null!;
        private AboutSection about = null!;
        private Notificationswipe notification = null!;
        // ─────────────────────────────────────────────────────────────────────
        // HELPER: Login
        // All waits live inside the page object methods — nothing here
        // ─────────────────────────────────────────────────────────────────────
        private void Login(string mobile, string pin, bool rememberMe = true)
        {
            Console.WriteLine($"[Login] {mobile}");
            signInPage.EnterMobileNumber(mobile);
            if (rememberMe) signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();
            passcode.ClearAndEnterPasscode(pin);
            passcode.ClickSignInButton();
            // Both popups are optional — page object methods handle their own timeout
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            Console.WriteLine($"[Login] Done: {mobile}");
        }

        // ─────────────────────────────────────────────────────────────────────
        // HELPER: Logout
        // ─────────────────────────────────────────────────────────────────────
        private void Logout()
        {
            Console.WriteLine("[Logout] Starting...");
            logout.ClickDrawerIcon();
            logout.PerformLogout();
            Console.WriteLine("[Logout] Done.");
        }

        // ═════════════════════════════════════════════════════════════════════
        // SETUP
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
                Assert.Fail("Driver failed to launch — check device connection and capabilities.");

            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            // Initialise all page objects after driver is confirmed ready
            signInPage = new SignIn(driver);
            passcode = new PasscodeVerification(driver);
            forgotPassword = new ForgotPassword(driver);
            fingerprint = new FingerPrint(driver);
            logout = new LogoutApp(driver);
            dashboard = new DashboardFirstTimeKYCPendingZeroBalance(driver);
            dashboardFull = new DashboardCompleteBalanceAvailable(driver);
            youngPage = new Young(driver);
            qrPage = new AddMoneyQRcode(driver);
            addBeneficiaryPage = new AddBenefiicary(driver);
            statementsPage = new Statements(driver);
            transactionFeedPage = new TransactionFeed(driver);
            textCompare = new TextCompare(driver);
            profile = new ProfileAndAccountSection(driver);
            addDevice = new AddDevice(driver);
            uiux = new GeneralUIUXtest(driver);
            security = new SecuritySection(driver);
            about = new AboutSection(driver);
             notification = new Notificationswipe(driver);

            Console.WriteLine("App launched. All page objects initialised.");
            RecordingScript.StartRecording(driver);
        }

        // ═════════════════════════════════════════════════════════════════════
        // TEST
        // ═════════════════════════════════════════════════════════════════════

        // ═════════════════════════════════════════════════════════════════════
        // TEST
        // ═════════════════════════════════════════════════════════════════════
        [Test]
        public void CompleteSignInFlow()
        {
            // ══════════════════════════════════════════════════════════════════
            // FLOW 1 — Invalid login shows correct error message
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 1: Invalid login error message ===");

            signInPage.EnterMobileNumber("3211111111");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();
            passcode.EnterPasscode("0987");
            signInPage.ClickSignInButtonPassCode();

            string errorText = textCompare.GetErrorMessage();
            Console.WriteLine("Error message received: " + errorText);

            Assert.IsTrue(
                textCompare.IsErrorMessageDisplayed("User not found for the specified user type"),
                "Expected error message not displayed for invalid login."
            );

            signInPage.ClickBackButton();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 2 — Valid login then logout
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 2: Valid login + logout ===");

            Login("3364646412", "3889");
            Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 3 — Balance eye + Young tab + widget navigation
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 3: Balance eye + Young tab ===");

            Login("3364646412", "3889");

            dashboard.ClickOnBalanceEye();
            youngPage.ClickYoungTab();
            youngPage.ClickMainTab();
            dashboard.ClickFirstWidgetIcon();
            dashboard.ClickOnLeftIcons();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 4 — Send Money: successful transaction
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 4: Send money – success ===");

            dashboard.ClickThirdWidgetIcon();
            dashboard.ClickFirstStatementItem();
            dashboardFull.ClickCoreView();
            dashboardFull.EnterAmount5();
            dashboardFull.ScrollAndClickPayButton();
            dashboardFull.EnterOtp("039167");
            dashboardFull.ClickProceedNextButton();
            dashboardFull.ClickGoToDashboard();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 5 — Send Money: error for invalid recipient / zero balance
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 5: Send money – insufficient balance ===");

            dashboard.ClickThirdWidgetIcon();
            dashboard.ClickFirstStatementItem();
            dashboard.ClickOnImage();
            dashboard.EnterAndClearAmount200();
            dashboard.ClickOnLeftIcons();
            dashboard.ClickOnLeftIcons();
            Thread.Sleep(5000);
            dashboard.ClickCrossBackButton();

            Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 6 — Recent transactions list loads without error
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 6: Recent transactions ===");

            Login("3364646412", "3889");

            dashboardFull.ScrollAndClickSecondTransactionItem();
            dashboard.ClickOnLeftIcons();

            Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 7 — Add Money via QR: User 1 saves QR to gallery
            // ══════════════════════════════════════════════════════════════════
            // FLOW 7 — Add Money via QR: User 1 saves QR to gallery
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 7: QR code – User 1 (save to gallery) ===");

            Login("3263855579", "0987");

            dashboard.ClickFirstWidgetIcon();
            qrPage.ClickSecondIvIcon();
            qrPage.ClickLeftIcon();
            qrPage.ClickFirstIvIcon();
            qrPage.ClickSaveToGallery();
            qrPage.TapBackButton();
            qrPage.TapNavigationBackButton();

            Logout();

            //// ══════════════════════════════════════════════════════════════════
            //// ══════════════════════════════════════════════════════════════════
            //// FLOW 8 — Add Money via QR: User 2 scans from library + pays
            //// ══════════════════════════════════════════════════════════════════
            // Console.WriteLine("=== FLOW 8: QR code – User 2 (scan + pay) ===");

            Login("3364646412", "3889");

            dashboard.ClickFirstWidgetIcon();
            qrPage.ClickSecondIvIcon();
            qrPage.ClickLeftIcon();
            qrPage.ClickFirstIvIcon();
            qrPage.TapScanButton();
            qrPage.TapLibraryButton();
            qrPage.SelectImageFromLibrary();
            qrPage.TapPhotoPickerConfirmButton();
            qrPage.EnterAmount200();
            qrPage.ScrollAndTapPayButton();
            dashboardFull.EnterOtp("039167");
            dashboardFull.ClickProceedNextButton();



            dashboardFull.ClickGoToDashboard();

            //  qrPage.ClickLeftIcon();
            notification.ClickThirdIcon();
            Tapagains tapActions = new Tapagains(driver);

            // Click the clIcon once
            tapActions.ClickClIcon();

            // Click tvIbanCopy 5 times
            tapActions.ClickTvIbanCopyMultipleTimes();
            qrPage.ClickLeftIcon();
            qrPage.ClickLeftIcon();
            dashboard.ClickOnBalanceEye();

             Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 9 — Add Beneficiary (invalid IBAN → error, valid IBAN → saved)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 9: Add Beneficiary ===");

            Login("3364646412", "3889");

            dashboard.ClickThirdWidgetIcon();
            addBeneficiaryPage.TapThirdIcon();

            // NOTE: Use TapAddBeneficiaryButton() if NO beneficiaries exist yet
            //       Use ClickAddBeneficiary()    if one or more already exist
            addBeneficiaryPage.ClickAddBeneficiary();

            addBeneficiaryPage.SearchModelBankOnly();
            addBeneficiaryPage.ClickBankLogo();

            // Invalid IBAN — app must show error
            addBeneficiaryPage.EnterBankNumber("00wq133211jh");
            addBeneficiaryPage.ScrollAndClickFindAccount();

            // Valid IBAN
            addBeneficiaryPage.EnterBankNumber("111112274588111");
            addBeneficiaryPage.ScrollAndClickFindAccount();

            addBeneficiaryPage.EnterNickname("poiuy@#");
            addBeneficiaryPage.ScrollAndClickConfirm();
            dashboardFull.EnterOtp("039167");
            addBeneficiaryPage.ClickSendLater();
            addBeneficiaryPage.ClickLeftIcon();
            addBeneficiaryPage.ClickLeftIcon();

            // Verify beneficiary appears in the list
            dashboard.ClickThirdWidgetIcon();
            addBeneficiaryPage.TapThirdIcon();
            addBeneficiaryPage.ClickLeftIcon();
            addBeneficiaryPage.ClickLeftIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 10 — Statements (all statement types)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 10: Statements ===");

            dashboard.ClickDrawerIcon();
            dashboard.ClickStatements();

            statementsPage.ClickViewFinancialYear();
            statementsPage.ClickLeftIcon();

            statementsPage.ClickViewYearToDate();
            statementsPage.ClickLeftIcon();

            statementsPage.ClickViewCustomDate();
            statementsPage.ClickLeftIcon();

            statementsPage.ClickViewStatements();
            statementsPage.ClickLeftIcon();

            statementsPage.ClickPreviousYear();
            statementsPage.ClickLeftIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 11 — Transaction Feed + Bottom Navigation
            // ══════════════════════════════════════════════════════════════════
            // FLOW 11 — Transaction Feed + Bottom Navigation
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 11: Transaction feed + bottom nav ===");

            transactionFeedPage.ClickFilterIcon();
            transactionFeedPage.ClickAmountSlider();
            transactionFeedPage.ClickApplyFilter();
            transactionFeedPage.ClickYoungNavigation();
            transactionFeedPage.ClickFabYapIt();
            transactionFeedPage.ClickFabYapIt();
            transactionFeedPage.ClickNavBarItem();
            transactionFeedPage.ClickMore();
            transactionFeedPage.ClickHome();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 12 — Profile: personal details
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 12: Profile – personal details ===");

            profile.ClickProfileIcon();
            profile.ClickViewPersonalDetails();
            profile.ClickLeftIcon();  // back from personal details
            profile.ClickLeftIcon();  // back to dashboard

            // ── Refer a Friend ────────────────────────────────────────────────
            Console.WriteLine("=== FLOW 12b: Refer a Friend ===");

            logout.ClickDrawerIcon();
            profile.ClickReferAFriend();
            profile.ClickShareButton();
            profile.ClickQuickShareButton();
            profile.ClickQuickShareConfirmButton();
            profile.ClickBackIcon();

            // ── Alerts & Notifications ────────────────────────────────────────
            Console.WriteLine("=== FLOW 12c: Alerts & Notifications ===");

            logout.ClickDrawerIcon();
            profile.ClickAlertsOption();
            profile.ClickNavigateUp();

            // ── Linked Devices: view ──────────────────────────────────────────
            Console.WriteLine("=== FLOW 12d: Linked Devices – view ===");

            logout.ClickDrawerIcon();
            profile.ClickSettingsIcon();
            profile.ClickLinkedDevicesView();
            profile.ClickBackIcon();
            profile.ClickBackIcon();

            // ── Linked Devices: delink ────────────────────────────────────────
            Console.WriteLine("=== FLOW 12e: Linked Devices – delink ===");

            logout.ClickDrawerIcon();
            profile.ClickSettingsIcon();
            profile.ClickLinkedDevicesView();
            addDevice.ClickActiveButton();
            profile.ClickBackIcon();
            profile.ClickBackIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 13 — Help & Support (FAQs + WhatsApp chat)
            // ══════════════════════════════════════════════════════════════════

            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 13: Help & Support ===");

            logout.ClickDrawerIcon();
            addDevice.ClickHelpAndSupport();
            addDevice.ClickViewFaqs();
            addDevice.ClickCrossIcon();
            addDevice.ClickWhatsappChat();
            addDevice.ClickLeftIcon();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 14 — Profile picture update
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 14: Profile picture update ===");

            uiux.ClickProfile();
            uiux.ClickViewPersonalDetails();
            profile.ClickBackIcon();
            uiux.ClickAddIcon();
            uiux.ClickChoosePhoto();
            qrPage.SelectImageFromLibrary();
            qrPage.TapPhotoPickerConfirmButton();
            uiux.ClickUsePhoto();
            profile.ClickBackIcon();

            Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 15 — Security: Change Passcode (user 3229009909)
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 15: Change Passcode ===");

            Login("3229009909", "3022");

            uiux.ClickProfile();
            security.ClickChangePasscode();
            forgotPassword.ClickForgotSecurityPin();
            forgotPassword.CreateNewPin("3023");
            forgotPassword.ConfirmNewPin("3023");
            forgotPassword.EnterOtp("039167");
            forgotPassword.ClickDoneButton();
            profile.ClickBackIcon();

            Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 16 — About section: T&C + all social media links
            // ══════════════════════════════════════════════════════════════════

            // ══════════════════════════════════════════════════════════════════
            // FLOW 16 — About section: T&C + all social media links
            // ══════════════════════════════════════════════════════════════════
            Console.WriteLine("=== FLOW 16: About section ===");

            Login("3364646412", "3889");

            uiux.ClickProfile();
            about.ClickViewTermsAndConditions();
            about.ClickCrossButton();
            about.ClickFollowTwitterAndReturn();
            about.ClickFollowInstagramAndReturn();
            about.ClickLikeUsAndReturn();
            profile.ClickBackIcon();

            Logout();

            // ══════════════════════════════════════════════════════════════════
            // FLOW 17 — Biometric / Touch ID + Notifications switch
            // IMPORTANT: Touch ID prompt fires BEFORE NoThanks/Confirmation
            // ══════════════════════════════════════════════════════════════════
            //Console.WriteLine("=== FLOW 17: Biometric + Notifications ===");

            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();
            //passcode.ClearAndEnterPasscode("3889");
            //passcode.ClickSignInButton();

            //// Touch ID appears first — must click before other popups
            //fingerprint.ClickUseTouchID();

            //// Remaining optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();

            //uiux.ClickProfile();
            //fingerprint.ClickNotificationsSwitch();
            //profile.ClickBackIcon();

            //Logout();
        }

        // ═════════════════════════════════════════════════════════════════════
        // TEARDOWN
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