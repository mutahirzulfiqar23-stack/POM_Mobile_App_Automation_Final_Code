using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;

namespace POM_Mobile_App_Automate_Stage.All_Pages
{
    internal class GeneralUIUXtest
    {
        private AndroidDriver driver;
        private WebDriverWait wait;

        // Constructor
        public GeneralUIUXtest(AndroidDriver driver)
        {
            this.driver = driver ?? throw new ArgumentNullException(nameof(driver));
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        // Locator
        private By ViewPersonalDetailsButton = By.Id("com.yappakistan.app.stage:id/tvViewPersonalDetails");

        // Method to Click View Personal Details
        public void ClickViewPersonalDetails()
        {
            wait.Until(d => d.FindElement(ViewPersonalDetailsButton).Displayed);
            driver.FindElement(ViewPersonalDetailsButton).Click();
        }
        private By AddIcon = By.Id("com.yappakistan.app.stage:id/ivAdd");

        public void ClickAddIcon()
        {
            wait.Until(d => d.FindElement(AddIcon).Displayed);
            driver.FindElement(AddIcon).Click();
        }
        // Locator for "Use photo" button
private By UsePhotoButton =
    By.Id("com.yappakistan.app.stage:id/btnUsePhoto");

// Method to click "Use photo"
public void ClickUsePhoto()
{
    IWebElement element = wait.Until(d => d.FindElement(UsePhotoButton));

    if (element.Displayed && element.Enabled)
    {
        element.Click();
    }
    else
    {
        throw new Exception("Use Photo button is not clickable.");
    }
}




        private By OpenCameraOption = By.Id("com.yappakistan.app.stage:id/tvTitle");

        public void ClickOpenCamera()
        {
            wait.Until(d => d.FindElement(
                By.XPath("//android.widget.TextView[@resource-id='com.yappakistan.app.stage:id/tvTitle' and @text='Open camera']")
            ).Displayed);

            driver.FindElement(
                By.XPath("//android.widget.TextView[@resource-id='com.yappakistan.app.stage:id/tvTitle' and @text='Open camera']")
            ).Click();
        }
        //    private By CameraGradientBar = By.Id("com.google.android.GoogleCamera:id/gradient_bar");

        //    public void TapCameraScreen()
        //    {
        //        wait.Until(d => d.FindElement(CameraGradientBar).Displayed);

        //        var element = driver.FindElement(CameraGradientBar);
        //        var location = element.Location;
        //        var size = element.Size;

        //        int centerX = location.X + size.Width / 2;
        //        int centerY = location.Y + size.Height / 2;

        //        driver.ExecuteScript("mobile: clickGesture", new Dictionary<string, object>
        //{
        //    { "x", centerX },
        //    { "y", centerY }
        //});
        //    }

        // Locator for Profile Icon


        private By ChoosePhotoOption = By.XPath("//android.widget.TextView[@resource-id='com.yappakistan.app.stage:id/tvTitle' and @text='Choose photo']");

        public void ClickChoosePhoto()
        {
            wait.Until(d => d.FindElement(ChoosePhotoOption).Displayed);
            driver.FindElement(ChoosePhotoOption).Click();
        }

        private By FirstPhotoInGrid = By.XPath("//android.view.View[@content-desc='Media grid']/android.view.View/android.view.View[2]/android.view.View[2]/android.view.View");

        public void TapFirstPhoto()
        {
            wait.Until(d => d.FindElement(FirstPhotoInGrid).Displayed);

            var element = driver.FindElement(FirstPhotoInGrid);

            int centerX = element.Location.X + element.Size.Width / 2;
            int centerY = element.Location.Y + element.Size.Height / 2;

            // Tap using Actions API
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveByOffset(centerX, centerY).Click().Perform();
            actions.MoveByOffset(-centerX, -centerY).Perform(); // Reset pointer
        }

        private By SelectButton = By.XPath("//androidx.compose.ui.platform.ComposeView/android.view.View/android.view.View/android.view.View/android.view.View/android.view.View[6]/android.view.View/android.view.View[3]/android.widget.Button");

        public void TapSelectButton()
        {
            wait.Until(d => d.FindElement(SelectButton).Displayed);

            var element = driver.FindElement(SelectButton);

            int centerX = element.Location.X + element.Size.Width / 2;
            int centerY = element.Location.Y + element.Size.Height / 2;

            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveByOffset(centerX, centerY).Click().Perform();
            actions.MoveByOffset(-centerX, -centerY).Perform();
        }

        // Locator for the first photo in media grid
        private By FirstPhotoInGrids = By.XPath("//android.view.View[@content-desc='Media grid']/android.view.View/android.view.View[2]/android.view.View[2]/android.view.View");

        public void TapFirstPhotoInGrid()
        {
            // Wait until element is visible
            wait.Until(d => d.FindElement(FirstPhotoInGrids).Displayed);

            var element = driver.FindElement(FirstPhotoInGrids);

            // Calculate center coordinates
            int centerX = element.Location.X + element.Size.Width / 2;
            int centerY = element.Location.Y + element.Size.Height / 2;

            // Tap using Actions API
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveByOffset(centerX, centerY).Click().Perform();

            // Reset pointer offset to avoid affecting next actions
            actions.MoveByOffset(-centerX, -centerY).Perform();
        }


        // Locator for the camera gradient bar
        private By CameraGradientBar = By.Id("com.google.android.GoogleCamera:id/gradient_bar");

        public void TapCameraGradientBar()
        {
            // Wait until the element is displayed
            wait.Until(d => d.FindElement(CameraGradientBar).Displayed);

            var element = driver.FindElement(CameraGradientBar);

            // Calculate center coordinates
            int centerX = element.Location.X + element.Size.Width / 2;
            int centerY = element.Location.Y + element.Size.Height / 2;

            // Tap using modern Actions API
            var actions = new OpenQA.Selenium.Interactions.Actions(driver);
            actions.MoveByOffset(centerX, centerY).Click().Perform();

            // Reset pointer offset so future actions work correctly
            actions.MoveByOffset(-centerX, -centerY).Perform();
        }
        // Locator for Profile Icon
        private By ProfileIcon = By.Id("com.yappakistan.app.stage:id/ivProfile");

        // Method to Click Profile
        public void ClickProfile()
        {
            wait.Until(d => d.FindElement(ProfileIcon).Displayed);
            driver.FindElement(ProfileIcon).Click();
        }
    }
}