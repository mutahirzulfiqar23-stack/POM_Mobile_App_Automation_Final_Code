using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class Notificationswipe
    {
        private AndroidDriver driver;

        public Notificationswipe(AndroidDriver driver)
        {
            this.driver = driver;
        }

        // Swipe notification to right
        public void SwipeNotificationRight()
        {
            try
            {
                IWebElement notification = driver.FindElement(By.Id("com.yappakistan.app.stage:id/toolbarTitle"));

                if (notification.Displayed)
                {
                    int startX = notification.Location.X + 10;
                    int startY = notification.Location.Y + (notification.Size.Height / 2);

                    int endX = startX + notification.Size.Width;

                    new TouchAction(driver)
                        .Press(startX, startY)
                        .Wait(300)
                        .MoveTo(endX, startY)
                        .Release()
                        .Perform();

                    Console.WriteLine("Notification swiped successfully.");
                }
            }
            catch
            {
                Console.WriteLine("No notification found.");
            }
        }

        // Click the 3rd ImageView icon
        public void ClickThirdIcon()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                IWebElement icon = wait.Until(d =>
                    d.FindElement(By.XPath("(//android.widget.ImageView[@resource-id='com.yappakistan.app.stage:id/ivIcon'])[3]"))
                );

                icon.Click();

                Console.WriteLine("Third icon clicked successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to click icon: " + e.Message);
            }
        }

        // Scroll down after opening tab
        public void ScrollDown()
        {
            try
            {
                int startX = driver.Manage().Window.Size.Width / 2;

                int startY = (int)(driver.Manage().Window.Size.Height * 0.8);

                int endY = (int)(driver.Manage().Window.Size.Height * 0.3);

                new TouchAction(driver)
                    .Press(startX, startY)
                    .Wait(500)
                    .MoveTo(startX, endY)
                    .Release()
                    .Perform();

                Console.WriteLine("Scroll performed successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Scroll failed: " + e.Message);
            }
        }
    }
}