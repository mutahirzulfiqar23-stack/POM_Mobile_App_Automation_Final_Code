using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.BiDi.Script;
using POM_Mobile_App_Automate_Stage.All_Pages;
using POM_Mobile_App_Automate_Stage.All_Pages;
using POM_Mobile_App_Automate_Stage.DriverSetup;
using POM_Mobile_App_Automate_Stage.Utilities;
using System;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Interop;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Transactions;
using POM_Mobile_App_Automate_Stage.Utilities;

//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace POM_Mobile_App_Automate_Stage.CompleteMethod
{
    internal class SanityMain
    {
        private AndroidDriver? driver;
        private readonly bool keepAppOpen = true; // Keep the app open after tests

        [SetUp]
        public void Setup()
        {
            WebDriver mainMethods = new WebDriver();

            driver = mainMethods.LaunchApp(
                "0A171JEC215267",
                "com.yappakistan.app.stage",
                "com.digitify.testyappakistan.onboarding.splash.SplashActivity"
            );

            if (driver == null)
                Assert.Fail("Failed to launch app.");

            Console.WriteLine("App launched successfully.");

            // 🎬 Start Recording
            ScreenRecorderHelper.StartRecording(driver);
        }

        [Test]
        public void CompleteSignInFlow()
        {
            Thread.Sleep(3000); // Optional, wait for splash

            // Initialize all page objects
            SignIn signInPage = new SignIn(driver!);
            PasscodeVerification passcode = new PasscodeVerification(driver!);
            ForgotPassword forgotPassword = new ForgotPassword(driver!);
            LogoutApp logout = new LogoutApp(driver!);
            DashboardFirstTimeKYCPendingZeroBalance dashboard =
                new DashboardFirstTimeKYCPendingZeroBalance(driver!);
            DashboardCompleteBalanceAvailable dashboardBalance =
                new DashboardCompleteBalanceAvailable(driver!);

            var dashboardx = new DashboardCompleteBalanceAvailable(driver);

            var dashboardy = new DashboardCompleteBalanceAvailable(driver);

            Young youngPage = new Young(driver!);

            AddMoneyQRcode qrPage = new AddMoneyQRcode(driver!);


            AddBenefiicary addBeneficiaryPage = new AddBenefiicary(driver!);
            AddBenefiicary addBeneficiary = new AddBenefiicary(driver);
            Statements statementsPage = new Statements(driver);
            TransactionFeed transactionFeedPage = new TransactionFeed(driver);
            // Inside your test method
            var textCompare = new TextCompare(driver);



            // ---------------- Example Flow ----------------
            //////////////////////////////////  //Incorrect credentials show proper error message.Sanity////////////////
            signInPage.EnterMobileNumber("3211111111");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();
            // Enter passcode
            passcode.EnterPasscode("0987");
            signInPage.ClickSignInButtonPassCode();


            // Get the error message text
            string errorText = textCompare.GetErrorMessage();
            Console.WriteLine("Error message is: " + errorText);

            // Check if it matches expected
            bool isMatch = textCompare.IsErrorMessageDisplayed("User not found for the specified user type");
            Console.WriteLine("Does the error match? " + isMatch);
            //// Back to main
            signInPage.ClickBackButton();

            /////////////////////////////////////////////////// User can log in with valid credentials and Logout Sanity// Sign in

            signInPage.EnterMobileNumber("3364646412");
            signInPage.ClickSignInButton();

            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();
           
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();

            //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();
            Thread.Sleep(5000);

            //////// NEW Forgot Password and Reset Password of a USER/////////////////////////////
            //signInPage.EnterMobileNumber("3364646412");
            //signInPage.ClickSignInButton();
            //forgotPassword.ClickForgotSecurityPin();
            //// Create PIN
            //forgotPassword.CreateNewPin("3897");

            //// Confirm PIN
            //forgotPassword.ConfirmNewPin("3897");

            //// OTP
            //forgotPassword.EnterOtp("039167");

            //// Confirm after OTP
            //forgotPassword.ClickDoneButton();
            //Thread.Sleep(5000);


            //// ---------- Sign In Again ----------
            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            //// Enter passcode
            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            //// Click Balance Eye
            dashboard.ClickOnBalanceEye();

            //////////Balances display correctly (Parent & YAP Young).///////////////////////////////////////////
            youngPage.ClickYoungTab();
            youngPage.ClickMainTab();

            //Open “Add Money” option – screen loads without crash.////////////////////////////////////////////
            //   Click Widget Icons
            dashboard.ClickFirstWidgetIcon();
            //// Click Left Icon
            dashboard.ClickOnLeftIcons();



            ///////////////Balance updates after a transaction.and also show the current balance after transaction //////////////////////
            //////////  // Open “Send Money” option – screen loads properly.   16
            ///////////   //Send money to a valid user – transaction completes successfully.    16
            ///////////////   //Verify transaction reflects in dashboard.   16

            dashboard.ClickThirdWidgetIcon();

            ////// Click first statement item
            dashboard.ClickFirstStatementItem();


            dashboardy.ClickCoreView();


            // dashboard.ClickThirdWidgetIcon();
            dashboardx.EnterAmount200();

            // Click Pay button
            dashboardy.ScrollAndClickPayButton();

            dashboardy.EnterOtp("039167");

            dashboardy.ClickProceedNextButton();

            dashboardy.ClickGoToDashboard();
            //Logout from Mobile APP
            Thread.Sleep(5000);
            //logout.ClickDrawerIcon();
            // logout.PerformLogout();
            Thread.Sleep(5000);

            // ///////////////-------------Send Money---------Error message shown for invalid recipient or insufficient balance.

            // //////// NEW Verify that the user cannot perform Send Money when balance is zero.
            dashboard.ClickThirdWidgetIcon();

            //// Click first statement item
            dashboard.ClickFirstStatementItem();

            //// Click image
            dashboard.ClickOnImage();

            //// Enter 200 and clear it
            dashboard.EnterAndClearAmount200();



            ////  Click the Left Icon
            dashboard.ClickOnLeftIcons();
            ////  Click the Left Icon
            dashboard.ClickOnLeftIcons();
            ////  // Click Cross / Back Button
            dashboard.ClickCrossBackButton();
            Thread.Sleep(5000);
            ////  //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();
            Thread.Sleep(5000);



            // ////// ---------- Sign In Again and Recent transactions list loads without error. --------------
            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            //// Enter passcode
            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            Thread.Sleep(5000);
            dashboardy.ScrollAndClickSecondTransactionItem();
            ////  Click the Left Icon
            Thread.Sleep(5000);
            dashboard.ClickOnLeftIcons();
            ////  //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();
            Thread.Sleep(5000);



            //////// ---------- Sign In Again and 
            //////Add money from linked QR code/bank transfer------------User 1 
            //////  Balance updates instantly. --------------


            signInPage.EnterMobileNumber("3263855579");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            //// Enter passcode
            passcode.ClearAndEnterPasscode("0987");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            Thread.Sleep(5000);
            //// Click Widget Icons
            dashboard.ClickFirstWidgetIcon();



            // Click second ivIcon
            qrPage.ClickSecondIvIcon();

            // Click left/back icon
            qrPage.ClickLeftIcon();

            qrPage.ClickFirstIvIcon();
            Thread.Sleep(5000);
            qrPage.ClickSaveToGallery();
            Thread.Sleep(5000);



            qrPage.TapBackButton();
            qrPage.TapNavigationBackButton();

            //////  //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();


            ////Thread.Sleep(5000);
            /////////// Sign In  -------------- User 2
            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            //// Enter passcode
            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            Thread.Sleep(5000);
            //// Click Widget Icons
            dashboard.ClickFirstWidgetIcon();



            // Click second ivIcon
            qrPage.ClickSecondIvIcon();

            // Click left/back icon
            qrPage.ClickLeftIcon();

            qrPage.ClickFirstIvIcon();
            Thread.Sleep(5000);
            qrPage.TapScanButton();
            Thread.Sleep(5000);
            qrPage.TapLibraryButton();
            Thread.Sleep(5000);
            qrPage.SelectImageFromLibrary();
            Thread.Sleep(5000);

            qrPage.TapPhotoPickerConfirmButton();
            Thread.Sleep(5000);
            qrPage.EnterAmount200();
            Thread.Sleep(5000);
            qrPage.ScrollAndTapPayButton();

            dashboardy.EnterOtp("039167");
            dashboardy.ClickProceedNextButton();
            dashboardy.ClickGoToDashboard();
            Thread.Sleep(10000);

            qrPage.ClickLeftHeaderIcon();

            ////// Click Balance Eye
            dashboard.ClickOnBalanceEye();

            ////////  //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();
            Thread.Sleep(5000);

            // Sign In  -------------- ADD Beneficiary
            //            Verify that the “Add Beneficiary” option is available under the Payments/ Transfers module.Check that the UI elements(fields, buttons, dropdowns) are properly visible and aligned.
            //Ensure the screen title and instructions(if any) are correct.
            //Input Validations
            //Verify required fields: Name, Account Number/ IBAN, Bank Name, Nickname (if applicable).
            //Check minimum and maximum length validations for beneficiary name and account number.
            //Enter invalid IBAN / account number → App should show a proper error message.
            //Enter special characters in name / nickname → Should not be accepted(if restricted).

            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            //// Enter passcode
            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();
            dashboard.ClickThirdWidgetIcon();


            addBeneficiaryPage.TapThirdIcon();
            ////////////////////////////When No Beneficiary Exist then use this
            ///////////////////////////////////addBeneficiaryPage.TapAddBeneficiaryButton();

            //////////////////////// Click on "Add Beneficiary + Button"  Use when one or more beneficary Existss
            addBeneficiaryPage.ClickAddBeneficiary();

            addBeneficiaryPage.SearchModelBankOnly();


            // Click on the Bank Logo
            addBeneficiaryPage.ClickBankLogo();


            // Input bank number
            addBeneficiaryPage.EnterBankNumber("00wq123211jh");
            // Scroll to and click "Find account"
            addBeneficiaryPage.ScrollAndClickFindAccount();


            // Input Correct bank number
            addBeneficiaryPage.EnterBankNumber("111112874730111");
            // Scroll to and click "Find account"
            addBeneficiaryPage.ScrollAndClickFindAccount();

            // Enter nickname
            addBeneficiaryPage.EnterNickname("qwerty1234@#");
            // Scroll and click "Confirm"
            addBeneficiaryPage.ScrollAndClickConfirm();
            dashboardy.EnterOtp("039167");
            // Click "No, later" button
            addBeneficiaryPage.ClickSendLater();



            // Click the left/back icon
            addBeneficiaryPage.ClickLeftIcon();

            // Click the left/back icon
            addBeneficiaryPage.ClickLeftIcon();

            dashboard.ClickThirdWidgetIcon();


            addBeneficiaryPage.TapThirdIcon();
            // Click the left/back icon
            addBeneficiaryPage.ClickLeftIcon();

            // Click the left/back icon
            addBeneficiaryPage.ClickLeftIcon();

            //////////  //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();




            ////////////////////////////////////////////////////// ////Statement page opens.
            //Transactions listed correctly with date / time / amount.
            //Verify statement download works(PDF, Excel, etc.).
            //Correct balance and transaction history displayed.
            //Error handling if no transactions are available.



            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            //// Enter passcode
            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();


            ////// Open drawer
            dashboard.ClickDrawerIcon();

            ////// Open statements
            dashboard.ClickStatements();




            statementsPage.ClickViewFinancialYear();

            //////// Go back
            ////    dashboard.ClickLeftNavigationIcon();

            // Click Back Icon
            statementsPage.ClickLeftIcon();

            // Click View Year To Date
            statementsPage.ClickViewYearToDate();

            // Click Back Icon
            statementsPage.ClickLeftIcon();
            statementsPage.ClickViewCustomDate();
            // Click Back Icon
            statementsPage.ClickLeftIcon();


            statementsPage.ClickViewStatements();
            //   Click Back Icon
            statementsPage.ClickLeftIcon();

            statementsPage.ClickPreviousYear();
            // Click Back Icon
            statementsPage.ClickLeftIcon();

            ////////////  //Logout from Mobile APP
            logout.ClickDrawerIcon();
            logout.PerformLogout();


            ///////////////////////////////////////Transactions (Feed)

            //            Sorting / Filter option works.
            //Transactions details are displayed properly with correct data.
            //Bottom Navigation
            //Home → Dashboard loads correctly.
            //Store → Page loads without crash.
            //YAP it → Page loads and functional.Cards → Active card listing visible.
            //More → Profile / Settings page loads.


            signInPage.EnterMobileNumber("3364646412");
            signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            ////// Enter passcode
            passcode.ClearAndEnterPasscode("3897");
            passcode.ClickSignInButton();

            //// Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();


            // Click Filter
            transactionFeedPage.ClickFilterIcon();

            // Click Amount Slider
            transactionFeedPage.ClickAmountSlider();

            transactionFeedPage.ClickApplyFilter();

            //YourPage page = new YourPage(driver);

            transactionFeedPage.ClickYoungNavigation();

            transactionFeedPage.ClickFabYapIt();
            transactionFeedPage.ClickFabYapIt();
            transactionFeedPage.ClickNavBarItem();
            transactionFeedPage.ClickMore();
            transactionFeedPage.ClickHome();
        }

        //        [TearDown]
        //        public void TearDown()
        //        {
        //            // Do not close or dispose driver if we want the app open---------
        //            if (!keepAppOpen)
        //            {
        //                if (driver != null)
        //                {
        //                    try { driver.Quit(); }
        //                    catch (Exception ex) { Console.WriteLine("Error quitting driver: " + ex.Message); }
        //                    finally { driver.Dispose(); driver = null; }
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("TearDown executed. Driver left open, app remains running.");
        //            }
        //        }
        //    }
        //}
        //[TearDown]
        //public void TearDown()
        //{
        //    var status = TestContext.CurrentContext.Result.Outcome.Status;
        //    var testName = TestContext.CurrentContext.Test.Name;

        //    if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
        //    {
        //        Console.WriteLine("Test Failed. Capturing Screenshot...");

        //        ScreenshotHelper.CaptureScreenshot(driver!, testName);
        //    }

        //    if (!keepAppOpen && driver != null)
        //    {
        //        try { driver.Quit(); }
        //        catch (Exception ex) { Console.WriteLine("Error quitting driver: " + ex.Message); }
        //        finally { driver.Dispose(); driver = null; }
        //    }
        //    else
        //    {
        //        Console.WriteLine("TearDown executed. App remains open.");
        //    }
        //}
        [TearDown]
        public void TearDown()
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var testName = TestContext.CurrentContext.Test.Name;

            // Stop recording and save video
            ScreenRecorderHelper.StopRecordingAndSave(driver!, testName);

            if (status == NUnit.Framework.Interfaces.TestStatus.Failed)
            {
                ScreenshotHelper.CaptureScreenshot(driver!, testName);
            }

            if (!keepAppOpen && driver != null)
            {
                driver.Quit();
                driver.Dispose();
                driver = null;
            }
        }
    }
}


//////////////Statement page opens.
////Transactions listed correctly with date / time / amount.


//////// Open drawer
//dashboard.ClickDrawerIcon();

//////// Open statements
//dashboard.ClickStatements();

//////// Go back
////    dashboard.ClickLeftNavigationIcon();
