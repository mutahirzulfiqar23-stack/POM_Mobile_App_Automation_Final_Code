using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using POM_Mobile_App_Automate_Stage.All_Pages;
using POM_Mobile_App_Automate_Stage.DriverSetup;
using System;
using System.Threading;

namespace POM_Mobile_App_Automate_Stage.CompleteMethod
{
    public class MainDashboardFirstTimeKYCPendingZeroBalance
    {
        private AndroidDriver? driver;
        private bool keepAppOpen = true;

        [SetUp]
        public void Setup()
        {
            WebDriver mainMethods = new WebDriver();

            driver = mainMethods.LaunchApp(
                "0A171JEC215267",
                "com.yappakistan.app.stage",
                "com.digitify.testyappakistan.onboarding.splash.SplashActivity"
            );

            Console.WriteLine("App launched successfully.");
        }

        [Test]
        public void CompleteSignInFlow()
        {
            Thread.Sleep(3000); // optional, wait for splash

            SignIn signInPage = new SignIn(driver!);
            PasscodeVerification passcode = new PasscodeVerification(driver!);
            //   ForgotPassword forgotPassword = new ForgotPassword(driver!);
            ForgotPassword forgotPassword = new ForgotPassword(driver);
            var logout = new LogoutApp(driver);
            DashboardFirstTimeKYCPendingZeroBalance dashboard =
               new DashboardFirstTimeKYCPendingZeroBalance(driver!);
            DashboardCompleteBalanceAvailable dashboardBalance =
    new DashboardCompleteBalanceAvailable(driver);
            var dashboards = new DashboardCompleteBalanceAvailable(driver);
            // Assuming you already have driver initialized
            var dashboardx = new DashboardCompleteBalanceAvailable(driver);
            var dashboardy = new DashboardCompleteBalanceAvailable(driver);


            //// NEW Remember ID Button and Not remeber Button//////////////////////////
            //signInPage.EnterMobileNumber("3211111111");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();
            //// Enter passcode
            //passcode.EnterPasscode("0987");
            //signInPage.ClickSignInButtonPassCode();

            //// Back to main
            //signInPage.ClickBackButton();

            ////Not Remember Button////////////////////////////////////////////
            //signInPage.EnterMobileNumber("3211111111");
            //signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();
            //// Enter passcode
            //passcode.EnterPasscode("0987");
            //signInPage.ClickSignInButtonPassCode();

            //// Back to main
            //signInPage.ClickBackButton();



            //// NEW Forgot Password and Reset Password od a USER/////////////////////////////
            //signInPage.EnterMobileNumber("3335863504");
            //signInPage.ClickSignInButton();
            //forgotPassword.ClickForgotSecurityPin();
            //// Create PIN
            //forgotPassword.CreateNewPin("8741");

            //// Confirm PIN
            //forgotPassword.ConfirmNewPin("8741");

            //// OTP
            // forgotPassword.EnterOtp("039167");

            //// Confirm after OTP
            //forgotPassword.ClickDoneButton();


            //// ---------- Sign In Again ----------
            //signInPage.EnterMobileNumber("3335863504");
            ////signInPage.DoubleClickRememberSwitch();
            //signInPage.ClickSignInButton();

            //// Enter passcode
            //passcode.ClearAndEnterPasscode("8741");
            //passcode.ClickSignInButton();

            //// Optional popups
            //passcode.ClickNoThanksButton();
            //passcode.ClickConfirmationButton();


            ////Logout from Mobile APP
            //// logout.ClickDrawerIcon();
            ////logout.PerformLogout(); 

            ////NEW ---------- Dashboard Zero  Balance------------------------------------------

            ////NEW Verify that PKR 0.00 is displayed when the user has no balance.


            //// Click Balance Eye
            //dashboard.ClickOnBalanceEye();

            //// NEW Verify that quick action buttons (Add money, Send money, Scan QR, Statements) are visible and clickable.

            //// Click Widget Icons
            //dashboard.ClickFirstWidgetIcon();
            ////// Click Left Icon
            //dashboard.ClickOnLeftIcons();
            //dashboard.ClickSecondWidgetIcon();
            ////// Click Left Icon
            //dashboard.ClickOnLeftIcons();
            //dashboard.ClickThirdWidgetIcon();
            ////// Click Left Icon
            //dashboard.ClickOnLeftIcons();
            //dashboard.ClickFourthWidgetIcon();
            //// Click Back icon
            //dashboard.ClickOnBackIcon();



            //// NEW Verify that the user cannot perform Send Money when balance is zero.
            //dashboard.ClickThirdWidgetIcon();

            //// Click first statement item
            //dashboard.ClickFirstStatementItem();

            //// Click image
            //dashboard.ClickOnImage();

            //// Enter 200 and clear it
            //   dashboard.EnterAndClearAmount200();

            ////  Click the Left Icon
            //dashboard.ClickOnLeftIcons();
            ////  Click the Left Icon
            //dashboard.ClickOnLeftIcons();
            ////  // Click Cross / Back Button
            //dashboard.ClickCrossBackButton();

            ////NEW Verify that statements show “No transactions available” if no history exists.

            ////// Open drawer
            //dashboard.ClickDrawerIcon();

            ////// Open statements
            //dashboard.ClickStatements();

            ////// Go back
            //dashboard.ClickLeftNavigationIcon();

            //Logout from Mobile APP
            //  logout.ClickDrawerIcon();
            // logout.PerformLogout();

            ////Dashboard – Complete (Balance Available)/////////////////
            // ---------- Sign In Again ----------
            signInPage.EnterMobileNumber("3364646412");
            //signInPage.DoubleClickRememberSwitch();
            signInPage.ClickSignInButton();

            // Enter passcode
            passcode.ClearAndEnterPasscode("0987");
            passcode.ClickSignInButton();

            // Optional popups
            passcode.ClickNoThanksButton();
            passcode.ClickConfirmationButton();

            ///////Verify that available balance is displayed correctly with currency and decimal formatting.(1)
            // Click Balance Eye
            dashboard.ClickOnBalanceEye();

            //// NEW Verify that the user can perform Send Money when balance is available.
            dashboard.ClickThirdWidgetIcon();

            ////// Click first statement item
            dashboard.ClickFirstStatementItem();


            dashboardy.ClickCoreView();


            // dashboard.ClickThirdWidgetIcon();
            dashboardx.EnterAmount5();

            // Click Pay button
            dashboardy.ScrollAndClickPayButton();

            dashboardy.EnterOtp("039167");

            dashboardy.ClickProceedNextButton();

            dashboardy.ClickGoToDashboard();





        }

        [TearDown]
        public void TearDown()
        {
            if (!keepAppOpen && driver != null)
            {
                try { driver.Quit(); }
                catch (Exception ex) { Console.WriteLine("Error quitting driver: " + ex.Message); }
                finally { driver.Dispose(); driver = null; }
            }
            else
            {
                Console.WriteLine("Driver not disposed, app remains open for manual testing.");
            }
        }
    }
}
