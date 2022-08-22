using ApprovalTests;
using ApprovalTests.Reporters;
using CreditCards.UITests.Helpers;
using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        const string About_Url = "http://localhost:44108/Home/About";

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                var initialToken = homePage.GenerationToken;

                driver.Navigate().GoToUrl(About_Url);
                driver.Navigate().Back();

                homePage.EnsurePageLoaded();

                var reloadedToken = homePage.GenerationToken;

                Assert.NotEqual(initialToken, reloadedToken);
            }
        }

        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);

                homePage.NavigateTo();

                Assert.Equal("Easy Credit Card", homePage.Products[0].name);
                Assert.Equal("20% APR", homePage.Products[0].interestRate);

                Assert.Equal("Silver Credit Card", homePage.Products[1].name);
                Assert.Equal("18% APR", homePage.Products[1].interestRate);

                Assert.Equal("Gold Credit Card", homePage.Products[2].name);
                Assert.Equal("17% APR", homePage.Products[2].interestRate);
            }
        }
        [Fact]
        public void OpenContactFooterLinkInNewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.ClickContactFooterLink();
                
                var allTabs = driver.WindowHandles;
                var homePageTab = allTabs[0];
                var contactTab = allTabs[1];

                driver.SwitchTo().Window(contactTab);

                Assert.EndsWith("/Home/Contact", driver.Url);
            }
        }

        [Fact]
        public void AlertIfLiveChatClosed()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.ClickLiveChatFooterLink();

                var alert = driver.WaitGetAlert(5);

                Assert.Equal("Live chat is currently closed.", alert.Text);

                alert.Accept();

            }
        }

        [Fact]
        public void NavigateToAboutUsWhenOnClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.ClickLearnAboutUsLink();

                var alert = driver.WaitGetAlert();
                alert.Accept();

                Assert.EndsWith("/Home/About", driver.Url);
            }
        }

        [Fact]
        public void NotNavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.ClickLearnAboutUsLink();

                var alertBox = driver.WaitGetAlert(5);

                alertBox.Dismiss();

                homePage.EnsurePageLoaded();

            }
        }

        [Fact]
        public void NotDisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));
                driver.Navigate().Refresh();

                Assert.False(homePage.IsCookieMessagePresent);

               
                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();

                Assert.True(homePage.IsCookieMessagePresent);
            }
        }

        [Fact]
        [UseReporter(typeof(BeyondCompareReporter))]
        public void RenderAboutPage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(About_Url);

                var screenShotDirver = (ITakesScreenshot)driver;

                var screenShort = screenShotDirver.GetScreenshot();
                
                screenShort.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);

                var file = new FileInfo("aboutpage.bmp");

                Approvals.Verify(file);
            }
        }


    }
}