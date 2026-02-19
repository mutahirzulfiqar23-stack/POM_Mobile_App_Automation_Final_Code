using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class OpenAppp
    {
        public AndroidDriver LaunchApp()
        {
            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.AutomationName = "UiAutomator2";

            // Device and app properties
            options.AddAdditionalAppiumOption("deviceName", "0A171JEC215267");
            options.AddAdditionalAppiumOption("appPackage", "com.yappakistan.app.stage");
            options.AddAdditionalAppiumOption("appActivity", "com.digitify.testyappakistan.onboarding.splash.SplashActivity");

            // Launch driver
            var driver = new AndroidDriver(
                new Uri("http://127.0.0.1:4723/wd/hub"),
                options,
                TimeSpan.FromSeconds(180)
            );

            return driver;
        }
    }
}
