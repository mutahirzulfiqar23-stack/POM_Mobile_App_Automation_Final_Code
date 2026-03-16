using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class Tapagains
    {
        private AndroidDriver driver;

        public Tapagains(AndroidDriver driver)
        {
            this.driver = driver;
        }

        // Click on the clIcon element
        public void ClickClIcon()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Locate the element using XPath
                IWebElement clIcon = wait.Until(d =>
                    d.FindElement(By.XPath("(//android.view.ViewGroup[@resource-id='com.yappakistan.app.stage:id/clIcon'])[2]"))
                );

                clIcon.Click();
                Console.WriteLine("clIcon clicked successfully.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to click clIcon: " + e.Message);
            }
        }

        // Click on the tvIbanCopy element 5 times
        public void ClickTvIbanCopyMultipleTimes(int times = 5)
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Locate the element
                IWebElement tvIbanCopy = wait.Until(d =>
                    d.FindElement(By.Id("com.yappakistan.app.stage:id/tvIbanCopy"))
                );

                for (int i = 0; i < times; i++)
                {
                    tvIbanCopy.Click();
                    Console.WriteLine($"tvIbanCopy clicked {i + 1} time(s).");
                    System.Threading.Thread.Sleep(500); // small pause between clicks
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to click tvIbanCopy: " + e.Message);
            }
        }
    }
}