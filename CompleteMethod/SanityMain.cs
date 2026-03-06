using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.BiDi.Script;
using POM_Mobile_App_Automate_Stage.All_Pages;
using POM_Mobile_App_Automate_Stage.Utilities;
using System;
using System.Threading;
using System.Threading.Channels;
using System.Timers;
using System.Xml.Linq;
using static NUnit.Framework.Constraints.Tolerance;
using DriverManager = POM_Mobile_App_Automate_Stage.DriverSetup.WebDriver;

//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POM_Mobile_App_Automate_Stage.CompleteMethod
{
    internal class SanityMain
    {
        private AndroidDriver driver = null!;
        // private AndroidDriver? driver;
        private readonly bool keepAppOpen = true; // Keep the app open after tests

        [SetUp]
        public void Setup()
        {
            DriverManager mainMethods = new DriverManager();

            // ScreenRecorderHelper.StartRecording(driver);
            driver = mainMethods.LaunchApp(
                "0A171JEC215267",
                "com.yappakistan.app.stage",
                "com.digitify.testyappakistan.onboarding.splash.SplashActivity"
            );

            if (driver == null)
                Assert.Fail("Failed to launch app.");

            Console.WriteLine("App launched successfully.");
            // ── START RECORDING ──────────────────────────────────────
            // Video will be saved to D:\ScreenshotFailed\CompleteTestVideo
            RecordingScript.StartRecording(driver);


        }

        [Test]
        public void CompleteSignInFlow()
        {
            Thread.Sleep(3000); // Optional, wait for splash

            // =============================
            // Initialize Page Objects
            // Each page object receives the driver instance
            // =============================

            // Login / Authentication Pages
            SignIn signInPage = new SignIn(driver);                  // Sign In screen
            PasscodeVerification passcode = new PasscodeVerification(driver);  // Passcode verification screen
            ForgotPassword forgotPassword = new ForgotPassword(driver);        // Forgot password flow
            FingerPrint fingerprint = new FingerPrint(driver);      // Fingerprint / Touch ID screen

            // Logout
            LogoutApp logout = new LogoutApp(driver);               // Logout functionality

            // =============================
            // Dashboard Pages
            // =============================

            DashboardFirstTimeKYCPendingZeroBalance dashboard =
                new DashboardFirstTimeKYCPendingZeroBalance(driver);  // First-time login dashboard (KYC pending, zero balance)

            DashboardCompleteBalanceAvailable dashboardBalance =
                new DashboardCompleteBalanceAvailable(driver);        // Dashboard with completed KYC & available balance

            // Duplicate instances (consider removing if not required)
            var dashboardx = new DashboardCompleteBalanceAvailable(driver);  // Additional dashboard instance
            var dashboardy = new DashboardCompleteBalanceAvailable(driver);  // Additional dashboard instance


            // =============================
            // Feature Pages
            // =============================

            Young youngPage = new Young(driver);                    // Young user feature page

            AddMoneyQRcode qrPage = new AddMoneyQRcode(driver);     // Add money via QR code

            AddBenefiicary addBeneficiaryPage = new AddBenefiicary(driver);  // Add beneficiary page
            AddBenefiicary addBeneficiary = new AddBenefiicary(driver);      // Duplicate instance (can reuse one)

            Statements statementsPage = new Statements(driver);     // Account statements page
            TransactionFeed transactionFeedPage = new TransactionFeed(driver); // Transaction feed/history page

            // =============================
            // Utility / Validation Pages
            // =============================

            var textCompare = new TextCompare(driver);              // Text comparison / validation utility

            ProfileAndAccountSection profile = new ProfileAndAccountSection(driver); // Profile & account settings

            AddDevice addDevice = new AddDevice(driver);            // Add new device flow

            GeneralUIUXtest uiux = new GeneralUIUXtest(driver);     // UI/UX validation tests

            SecuritySection security = new SecuritySection(driver); // Security settings section

            AboutSection about = new AboutSection(driver);          // About section


            // ---------------- Example Flow ----------------
            //////////////////////////////////  //Incorrect credentials show proper error message.Sanity////////////////
             //=============================
             //Login Flow - Invalid User Scenario
             //=============================

            //// Enter mobile number in the Sign In screen
            //signInPage.EnterMobileNumber("3211111111");

            //// Toggle the "Remember Me" switch (double click if required by app behavior)
            //signInPage.DoubleClickRememberSwitch();

            //// Click the Sign In button to proceed
            //signInPage.ClickSignInButton();

            //// =============================
            //// Passcode Verification
            //// =============================

            //// Enter 4-digit passcode
            //passcode.EnterPasscode("0987");

            //// Click Sign In button after entering passcode
            //signInPage.ClickSignInButtonPassCode();

            //// =============================
            //// Error Message Validation
            //// =============================

            //// Capture the displayed error message text
            //string errorText = textCompare.GetErrorMessage();
            //Console.WriteLine("Error message is: " + errorText);

            //// Verify that the error message matches expected text
            //bool isMatch = textCompare.IsErrorMessageDisplayed("User not found for the specified user type");
            //Console.WriteLine("Does the error match? " + isMatch);

            //// =============================
            //// Navigation
            //// =============================

            //// Navigate back to the main screen
            //signInPage.ClickBackButton();

            ///////////////////////////////////////////////////// User can log in with valid credentials and Logout Sanity// Sign in

            //// =============================
            //// Login Flow - Valid User Scenario
            //// =============================

            //// Enter registered mobile number on Sign In screen
            //signInPage.EnterMobileNumber("3364646412");

            //// Tap Sign In button to proceed to passcode screen
            //signInPage.ClickSignInButton();


            //// =============================
            //// Passcode Verification
            //// =============================

            //// Clear any existing digits and enter valid passcode
            //passcode.ClearAndEnterPasscode("3889");

            //// Tap Sign In button after entering passcode
            //passcode.ClickSignInButton();


            //// =============================
            //// Post-Login Popups Handling
            //// =============================

            //// If biometric prompt appears, tap "No Thanks"
            //passcode.ClickNoThanksButton();

            //// Confirm any additional confirmation dialog (if displayed)
            //passcode.ClickConfirmationButton();


            //// =============================
            //// Logout Flow
            //// =============================

            //// Open navigation drawer/menu
            //logout.ClickDrawerIcon();

            //// Perform logout action from menu
            //logout.PerformLogout();

            //// Static wait to allow logout process to complete (Not Recommended - replace with explicit wait)
            //Thread.Sleep(5000);

            ////////////// NEW Forgot Password and Reset Password of a USER/////////////////////////////
            ////signInPage.EnterMobileNumber("3364646412");
            ////signInPage.ClickSignInButton();
            ////forgotPassword.ClickForgotSecurityPin();
            ////Thread.Sleep(5000);
            ////// Create PIN
            ////forgotPassword.CreateNewPin("3889");
            ////Thread.Sleep(5000);
            ////// Confirm PIN
            ////forgotPassword.ConfirmNewPin("3889");
            ////Thread.Sleep(5000);
            ////// OTP
            ////forgotPassword.EnterOtp("039167");
            ////Thread.Sleep(5000);
            ////// Confirm after OTP
            ////forgotPassword.ClickDoneButton();
            ////Thread.Sleep(5000);


            //////// ---------- Sign In Again ----------
            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            ////// Enter passcode
            //passcode.ClearAndEnterPasscode("3889");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            ////// Click Balance Eye
            //dashboard.ClickOnBalanceEye();

            //////////////Balances display correctly (Parent & YAP Young).///////////////////////////////////////////
            //youngPage.ClickYoungTab();
            //youngPage.ClickMainTab();

            //////Open “Add Money” option – screen loads without crash.////////////////////////////////////////////
            ////   Click Widget Icons
            //dashboard.ClickFirstWidgetIcon();
            ////// Click Left Icon
            //dashboard.ClickOnLeftIcons();



            /////////////////Balance updates after a transaction.and also show the current balance after transaction //////////////////////
            ////////////  // Open “Send Money” option – screen loads properly.   16
            /////////////   //Send money to a valid user – transaction completes successfully.    16
            /////////////////   //Verify transaction reflects in dashboard.   16

            //dashboard.ClickThirdWidgetIcon();

            ////////// Click first statement item
            //dashboard.ClickFirstStatementItem();


            //dashboardy.ClickCoreView();


            //// dashboard.ClickThirdWidgetIcon();
            //dashboardx.EnterAmount5();

            //// Click Pay button
            //dashboardy.ScrollAndClickPayButton();

            //dashboardy.EnterOtp("039167");

            //dashboardy.ClickProceedNextButton();

            //dashboardy.ClickGoToDashboard();

            //Thread.Sleep(5000);


            //// ///////////////-------------Send Money---------Error message shown for invalid recipient or insufficient balance.

            //// //////// NEW Verify that the user cannot perform Send Money when balance is zero.
            //dashboard.ClickThirdWidgetIcon();

            ////// Click first statement item
            //dashboard.ClickFirstStatementItem();

            ////// Click image
            //dashboard.ClickOnImage();
            //Thread.Sleep(5000);
            ////// Enter 200 and clear it
            //dashboard.EnterAndClearAmount200();

            //Thread.Sleep(5000);

            //////  Click the Left Icon
            //dashboard.ClickOnLeftIcons();
            //Thread.Sleep(5000);
            //////  Click the Left Icon
            //dashboard.ClickOnLeftIcons();
            //Thread.Sleep(5000);
            //////  // Click Cross / Back Button
            //dashboard.ClickCrossBackButton();
            //Thread.Sleep(5000);
            //////  //Logout from Mobile APP
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();
            //Thread.Sleep(5000);



            ////// ////// ---------- Sign In Again and Recent transactions list loads without error. --------------
            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            ////// Enter passcode
            //passcode.ClearAndEnterPasscode("3889");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            //Thread.Sleep(5000);
            //dashboardy.ScrollAndClickSecondTransactionItem();
            //////  Click the Left Icon
            //Thread.Sleep(5000);
            //dashboard.ClickOnLeftIcons();
            //////  //Logout from Mobile APP
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();
            //Thread.Sleep(5000);



            ////////// ---------- Sign In Again and 
            ////////Add money from linked QR code/bank transfer------------User 1 
            ////////  Balance updates instantly. --------------


            //signInPage.EnterMobileNumber("3263855579");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            ////// Enter passcode
            //passcode.ClearAndEnterPasscode("0987");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            //Thread.Sleep(5000);
            ////// Click Widget Icons
            //dashboard.ClickFirstWidgetIcon();



            //// Click second ivIcon
            //qrPage.ClickSecondIvIcon();

            //// Click left/back icon
            //qrPage.ClickLeftIcon();

            //qrPage.ClickFirstIvIcon();
            //Thread.Sleep(5000);
            //qrPage.ClickSaveToGallery();
            //Thread.Sleep(5000);



            //qrPage.TapBackButton();
            //qrPage.TapNavigationBackButton();

            //////////  //Logout from Mobile APP
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();


            //Thread.Sleep(5000);
            ///////////// Sign In  -------------- User 2
            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            ////// Enter passcode
            //passcode.ClearAndEnterPasscode("3889");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            //Thread.Sleep(5000);
            ////// Click Widget Icons
            //dashboard.ClickFirstWidgetIcon();



            //// Click second ivIcon
            //qrPage.ClickSecondIvIcon();

            //// Click left/back icon
            //qrPage.ClickLeftIcon();

            //qrPage.ClickFirstIvIcon();
            //Thread.Sleep(5000);
            //qrPage.TapScanButton();
            //Thread.Sleep(5000);
            //qrPage.TapLibraryButton();
            //Thread.Sleep(5000);
            //qrPage.SelectImageFromLibrary();
            //Thread.Sleep(5000);

            //qrPage.TapPhotoPickerConfirmButton();
            //Thread.Sleep(5000);
            //qrPage.EnterAmount200();
            //Thread.Sleep(5000);
            //qrPage.ScrollAndTapPayButton();

            //dashboardy.EnterOtp("039167");
            //dashboardy.ClickProceedNextButton();
            //dashboardy.ClickGoToDashboard();
            //Thread.Sleep(10000);

            //qrPage.ClickLeftHeaderIcon();

            //////// Click Balance Eye
            //dashboard.ClickOnBalanceEye();

            //////////  //Logout from Mobile APP
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();
            //Thread.Sleep(5000);

            //// Sign In  -------------- ADD Beneficiary
            ////            Verify that the “Add Beneficiary” option is available under the Payments/ Transfers module.Check that the UI elements(fields, buttons, dropdowns) are properly visible and aligned.
            ////Ensure the screen title and instructions(if any) are correct.
            ////Input Validations
            ////Verify required fields: Name, Account Number/ IBAN, Bank Name, Nickname (if applicable).
            ////Check minimum and maximum length validations for beneficiary name and account number.
            ////Enter invalid IBAN / account number → App should show a proper error message.
            ////Enter special characters in name / nickname → Should not be accepted(if restricted).

            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            ////// Enter passcode
            //passcode.ClearAndEnterPasscode("3889");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            //Thread.Sleep(5000);


            //dashboard.ClickThirdWidgetIcon();
            //Thread.Sleep(5000);
            //addBeneficiaryPage.TapThirdIcon();
            //Thread.Sleep(5000);
            ////////////////////////////////When No Beneficiary Exist then use this
            ///////////////////////////////////////addBeneficiaryPage.TapAddBeneficiaryButton();

            //////////////////////////// Click on "Add Beneficiary + Button"  Use when one or more beneficary Existss
            //addBeneficiaryPage.ClickAddBeneficiary();
            //Thread.Sleep(5000);
            //addBeneficiaryPage.SearchModelBankOnly();

            //Thread.Sleep(5000);
            //// Click on the Bank Logo
            //addBeneficiaryPage.ClickBankLogo();
            //Thread.Sleep(5000);

            //// Input bank number
            //addBeneficiaryPage.EnterBankNumber("00wq133211jh");
            //// Scroll to and click "Find account"
            //addBeneficiaryPage.ScrollAndClickFindAccount();
            //Thread.Sleep(5000);

            //// Input Correct bank number
            //addBeneficiaryPage.EnterBankNumber("111113877788111");
            //// Scroll to and click "Find account"
            //Thread.Sleep(5000);
            //addBeneficiaryPage.ScrollAndClickFindAccount();

            ////// Enter nickname
            //addBeneficiaryPage.EnterNickname("po2trJ@#");
            //// Scroll and click "Confirm"
            //Thread.Sleep(5000);
            //addBeneficiaryPage.ScrollAndClickConfirm();
            //dashboardy.EnterOtp("039167");
            //// Click "No, later" button
            //Thread.Sleep(5000);
            //addBeneficiaryPage.ClickSendLater();
            //Thread.Sleep(5000);
            //// Click the left/back icon
            //addBeneficiaryPage.ClickLeftIcon();

            //// Click the left/back icon
            //addBeneficiaryPage.ClickLeftIcon();
            //Thread.Sleep(5000);
            //dashboard.ClickThirdWidgetIcon();


            //addBeneficiaryPage.TapThirdIcon();
            //// Click the left/back icon
            //addBeneficiaryPage.ClickLeftIcon();
            //Thread.Sleep(5000);
            //// Click the left/back icon
            //addBeneficiaryPage.ClickLeftIcon();


            //////////////////////////////////////////////////////// ////Statement page opens.
            ////Transactions listed correctly with date / time / amount.
            ////Verify statement download works(PDF, Excel, etc.).
            ////Correct balance and transaction history displayed.
            ////Error handling if no transactions are available.



            //////// Open drawer
            //dashboard.ClickDrawerIcon();

            //////// Open statements
            //dashboard.ClickStatements();




            //statementsPage.ClickViewFinancialYear();

            ////////// Go back
            //////    dashboard.ClickLeftNavigationIcon();

            //// Click Back Icon
            //statementsPage.ClickLeftIcon();

            //// Click View Year To Date
            //statementsPage.ClickViewYearToDate();

            //// Click Back Icon
            //statementsPage.ClickLeftIcon();
            //statementsPage.ClickViewCustomDate();
            //// Click Back Icon
            //statementsPage.ClickLeftIcon();


            //statementsPage.ClickViewStatements();
            ////   Click Back Icon
            //statementsPage.ClickLeftIcon();

            //statementsPage.ClickPreviousYear();
            //// Click Back Icon
            //statementsPage.ClickLeftIcon();
            //Thread.Sleep(5000);


            ///////////////////////////////////////////Transactions (Feed)

            //////            Sorting / Filter option works.
            //////Transactions details are displayed properly with correct data.
            //////Bottom Navigation
            //////Home → Dashboard loads correctly.
            //////Store → Page loads without crash.
            //////YAP it → Page loads and functional.Cards → Active card listing visible.
            //////More → Profile / Settings page loads.

            //// Click Filter
            //transactionFeedPage.ClickFilterIcon();
            //Thread.Sleep(5000);
            //// Click Amount Slider
            //transactionFeedPage.ClickAmountSlider();
            //Thread.Sleep(5000);
            //transactionFeedPage.ClickApplyFilter();
            //Thread.Sleep(5000);
            ////YourPage page = new YourPage(driver);

            //transactionFeedPage.ClickYoungNavigation();

            //transactionFeedPage.ClickFabYapIt();
            //Thread.Sleep(5000);
            //transactionFeedPage.ClickFabYapIt();
            //Thread.Sleep(5000);
            //transactionFeedPage.ClickNavBarItem();
            //Thread.Sleep(5000);
            //transactionFeedPage.ClickMore();
            //Thread.Sleep(5000);
            //transactionFeedPage.ClickHome();




            //////////////////////            YAP App(Side Menu)
            //////////////////////1
            //////////////////////Profile & Account Section   20
            //////////////////////Verify user profile name and initials are displayed correctly.  20
            //////////////////////Check profile dropdown(if any) works properly.	20
            //////////////////////Ensure "My profile" link opens correct page.	20
            //////////////////////2
            //////////////////////Refer a Friend  20
            //////////////////////Verify the option redirects to the referral flow/ page.  20
            //////////////////////Referral link/ code generation works.	20
            //////////////////////Check share functionality(WhatsApp, email, SMS, etc.). 20
            //////////////////////3
            //////////////////////Alerts and Notifications    20
            //////////////////////Verify notifications are displayed correctly.   20
            //////////////////////Ensure unread/ read states work.	20
            //////////////////////Push notification redirection tested.	20
            //////////////////////4
            //////////////////////Linked Devices(Production) 20
            //////////////////////Verify Linked Devices option is visible in settings.    20
            //////////////////////Screen opens without crash or delay.	20
            //////////////////////Device List
            //////////////////////All currently linked devices are displayed with correct details(e.g., device name, OS, login   20
            //////////////////////time).  20
            //////////////////////The active device is marked correctly (usually the current session).	20
            ///////////////////te or missing entries.    20


            //Thread.Sleep(5000);
            //profile.ClickProfileIcon();
            //Thread.Sleep(5000);
            //profile.ClickViewPersonalDetails();
            //Thread.Sleep(5000);
            //profile.ClickLeftIcon();
            //Thread.Sleep(5000);
            //profile.ClickLeftIcon();
            //Thread.Sleep(5000);
            //logout.ClickDrawerIcon();


            //Thread.Sleep(5000);
            ////// Open Refer a Friend page
            //profile.ClickReferAFriend();

            //Thread.Sleep(5000);
            //// Tap "Share"
            //profile.ClickShareButton();
            //Thread.Sleep(5000);
            //// Tap "Quick Share" in system chooser
            //profile.ClickQuickShareButton();

            //Thread.Sleep(5000);
            //// Click Quick Share confirmation button
            //profile.ClickQuickShareConfirmButton();
            //Thread.Sleep(5000);
            //// Navigate back using left icon
            //profile.ClickBackIcon();
            //Thread.Sleep(5000);
            ////////////back using left icon
            ////////////////////////////profile.ClickLeftIconc();
            //logout.ClickDrawerIcon();
            ////// Click "Alerts and notifications"
            //profile.ClickAlertsOption();

            //Thread.Sleep(5000);
            ////////////////Click Navigate Up button

            //profile.ClickNavigateUp();
            //Thread.Sleep(5000);

            //logout.ClickDrawerIcon();
            //Thread.Sleep(5000);
            //profile.ClickSettingsIcon();
            //Thread.Sleep(5000);
            //profile.ClickLinkedDevicesView();
            //Thread.Sleep(5000);
            //profile.ClickBackIcon();
            //Thread.Sleep(5000);
            //profile.ClickBackIcon();
            //Thread.Sleep(5000);



            //////////            Add Device
            //////////Verify OTP/ 2FA is required for first - time login from a new device. ////Ignore
            //////////New device is shown with correct name and timestamp.Remove / Delink Device
            //////////User can remove a device successfully.
            //////////Ensure current device cannot be removed(if business logic allows only removing other


            //Thread.Sleep(5000);
            //logout.ClickDrawerIcon();
            //Thread.Sleep(5000);
            //profile.ClickSettingsIcon();

            //profile.ClickLinkedDevicesView();
            //Thread.Sleep(5000);
            ////  addDevice.ClickThirdDelinkButton();

            //Thread.Sleep(5000);
            //addDevice.ClickActiveButton();

            //profile.ClickBackIcon();
            //profile.ClickBackIcon();





            ////////////            Manage Widgets
            ////////////Verify widget settings open properly.
            ////////////Enable / disable widgets updates UI correctly.
            ////////////Check changes persist after re - login.
            ////////////Live Chat via WhatsApp
            ////////////Verify WhatsApp chat opens with correct support number.
            ////////////Message redirection works.
            ////////////Help and Support
            ////////////Ensure FAQs/ help articles open properly.
            ////////////Contact support form / chat works.
            ////////////Logout
            ////////////Verify logout works smoothly.
            ////////////Session is cleared(user cannot access app without login).
            ////////////Re - login works without issues.

            //Thread.Sleep(5000);
            //logout.ClickDrawerIcon();

            //addDevice.ClickHelpAndSupport();
            //addDevice.ClickViewFaqs();
            //Thread.Sleep(5000);
            //addDevice.ClickCrossIcon();
            //Thread.Sleep(5000);
            //addDevice.ClickWhatsappChat();
            //Thread.Sleep(5000);
            //addDevice.ClickLeftIcon();



            //////////////////////            Navigation back and forth works properly.
            //////////////////////Verify Personal details page opens correctly and displays accurate user info.
            //////////////////////Check profile picture update(if allowed) works.


            //Thread.Sleep(5000);
            //uiux.ClickProfile();
            //Thread.Sleep(5000);
            //uiux.ClickViewPersonalDetails();

            //profile.ClickBackIcon();

            //uiux.ClickAddIcon();
            //Thread.Sleep(5000);

            //uiux.ClickChoosePhoto();  // App's choose photo option
            //Thread.Sleep(5000);

            //qrPage.SelectImageFromLibrary();
            //Thread.Sleep(5000);

            //qrPage.TapPhotoPickerConfirmButton();
            //Thread.Sleep(5000);
            //GeneralUIUXtest generalUI = new GeneralUIUXtest(driver);
            //Thread.Sleep(5000);

            //generalUI.ClickUsePhoto();
            //Thread.Sleep(5000);
            //profile.ClickBackIcon();
            //Thread.Sleep(5000);
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();
            //Thread.Sleep(5000);

            ////            Security Section
            ////Privacy link opens and loads the correct content.
            ////Security Pin:
            ////Change PIN flow works successfully.
            ////App validates incorrect / weak PINs properly.


            //signInPage.EnterMobileNumber("3229009909");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            //////// Enter passcode
            //passcode.ClearAndEnterPasscode("3122");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            //Thread.Sleep(5000);
            //uiux.ClickProfile();

            ////Thread.Sleep(5000);
            //// Tap "More" tab
            //security.ClickChangePasscode();     // Click "Change Passcode"



            //forgotPassword.ClickForgotSecurityPin();
            //forgotPassword.CreateNewPin("3022");
            //Thread.Sleep(5000);
            ////// Confirm PIN
            //forgotPassword.ConfirmNewPin("3022");

            ////// OTP
            //forgotPassword.EnterOtp("039167");

            ////// Confirm after OTP
            //forgotPassword.ClickDoneButton();
            //Thread.Sleep(5000);
            //profile.ClickBackIcon();
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();
            //Thread.Sleep(5000);


            ////            About Section
            ////Terms & Conditions page opens properly.
            ////Social media links redirect correctly:
            ////            Instagram opens the YAP official page.
            ////Twitter opens the YAP official page.
            ////Facebook opens the YAP official page.
            ////LinkedIn opens the YAP official page.


            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            //////// Enter passcode
            //passcode.ClearAndEnterPasscode("3889");
            //passcode.ClickSignInButton();

            ////// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();
            //uiux.ClickProfile();

            //about.ClickViewTermsAndConditions();
            //about.ClickCrossButton();             // Close using cross icon
            //Thread.Sleep(5000);
            //about.ClickFollowTwitterAndReturn();
            //Thread.Sleep(5000);
            //about.ClickFollowInstagramAndReturn();
            //Thread.Sleep(5000);
            //about.ClickLikeUsAndReturn();
            //Thread.Sleep(5000);
            //profile.ClickBackIcon();
            //logout.ClickDrawerIcon();
            //logout.PerformLogout();

            ////////            Enable / disable works smoothly.
            ////////Re - login with biometric is successful.
            ////////App correctly falls back to PIN if biometric disabled.


            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            ////// Enter passcode
            passcode.ClearAndEnterPasscode("3889");
            Thread.Sleep(5000);
            passcode.ClickSignInButton();
            Thread.Sleep(5000);

            fingerprint.ClickUseTouchID();
            Thread.Sleep(5000);
            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            Thread.Sleep(5000);
            uiux.ClickProfile();
           
            // Simple Click
            fingerprint.ClickNotificationsSwitch();
            profile.ClickBackIcon();
             logout.ClickDrawerIcon();
            logout.PerformLogout();
        }


        [TearDown]
        public void TearDown()
        {
            // 1. Grab the test name and result status from NUnit
            string testName = TestContext.CurrentContext.Test.Name;
            var testStatus = TestContext.CurrentContext.Result.Outcome.Status;

            // 2. STOP RECORDING and save the video
            //    Video goes to: D:\ScreenshotFailed\CompleteTestVideo\<testName>_<timestamp>.mp4
            RecordingScript.StopRecordingAndSave(testName);

            // 3. If the test FAILED, capture a screenshot of the current screen
            //    Screenshot goes to: D:\ScreenshotFailed\FAILED_<testName>_<timestamp>.png
            if (testStatus == TestStatus.Failed)
            {
                Console.WriteLine("[TearDown] Test FAILED – capturing screenshot.");
                RecordingFailedScreenshots.CaptureScreenshot(driver, testName);
            }

            // 4. Optional: keep the app open (existing behaviour)
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


