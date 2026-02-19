using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace POM_Mobile_App_Automate_Stage.DriverSetup
{
    public class WebDriver
    {
        public AndroidDriver LaunchApp(string deviceId, string appPackage, string appActivity)
        {
            var options = new AppiumOptions();
            options.PlatformName = "Android";
            options.AutomationName = "UiAutomator2";
            options.DeviceName = deviceId;
            options.AddAdditionalAppiumOption("udid", deviceId);
            options.AddAdditionalAppiumOption("appPackage", appPackage);
            options.AddAdditionalAppiumOption("appActivity", appActivity);
            options.AddAdditionalAppiumOption("noReset", true);
            options.AddAdditionalAppiumOption("autoGrantPermissions", true);

            var driver = new AndroidDriver(
                new Uri("http://127.0.0.1:4723/wd/hub"),
                options,
                TimeSpan.FromSeconds(180)
            );

            return driver;
        }
    }
}
