using ApprovalTests;
using ApprovalTests.Reporters;
using CreditCards.UITests.Helpers;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        const string Home_Url = "http://localhost:44108/";
        const string About_Url = "http://localhost:44108/Home/About";
        const string Home_Title = "Home Page - Credit Cards";

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);

                driver.Manage().Window.Maximize();
                Thread.Sleep(2000);

                driver.Manage().Window.Maximize();
                Thread.Sleep(2000);

                driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
                Thread.Sleep(2000);

                driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
                Thread.Sleep(2000);

                driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
                Thread.Sleep(2000);

                driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
                Thread.Sleep(2000);

                driver.Manage().Window.FullScreen();

                Assert.Equal(Home_Title, driver.Title);
                Assert.Equal(Home_Url, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);

                driver.Navigate().Refresh();

                Assert.Equal(Home_Title, driver.Title);
                Assert.Equal(Home_Url, driver.Url);
            }
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
                var initialToken = driver.FindElement(By.Id("GenerationToken")).Text;

                driver.Navigate().GoToUrl(About_Url);
                driver.Navigate().Back();

                var reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;

                Assert.Equal(Home_Title, driver.Title);
                Assert.Equal(Home_Url, driver.Url);
                Assert.NotEqual(initialToken, reloadedToken);
            }
        }
        [Fact]
        public void DisplayProductsAndRates()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
                var tableCells = driver.FindElements(By.TagName("td"));

                Assert.Equal("Easy Credit Card", tableCells[0].Text);
                Assert.Equal("20% APR", tableCells[1].Text);

                Assert.Equal("Silver Credit Card", tableCells[2].Text);
                Assert.Equal("18% APR", tableCells[3].Text);

                Assert.Equal("Gold Credit Card", tableCells[4].Text);
                Assert.Equal("17% APR", tableCells[5].Text);
            }
        }
        [Fact]
        public void OpenContactFooterLinkInNewTab()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
                driver.FindElement(By.Id("ContactFooter")).Click();

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
                driver.Navigate().GoToUrl(Home_Url);
                driver.FindElement(By.Id("LiveChat")).Click();

                var alert = driver.WaitGetAlert(5);

                Assert.Equal("Live chat is currently closed.", alert.Text);

                alert.Accept();

            }
        }

        [Fact]
        public void NotNavigateToAboutUsWhenCancelClicked()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
                driver.FindElement(By.Id("LearnAboutUs")).Click();

                var alertBox = driver.WaitGetAlert(5);

                alertBox.Dismiss();

                Assert.Equal(Home_Title, driver.Title);

            }
        }
        [Fact]
        public void NotDisplayCookieUseMessage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
                driver.Manage().Cookies.AddCookie(new Cookie("acceptedCookies", "true"));

                driver.Navigate().Refresh();

                var message = driver.FindElements(By.Id("CookiesBeingUsed"));
                Assert.Empty(message);

                var cookies = driver.Manage().Cookies.GetCookieNamed("acceptedCookies");
                Assert.Equal("true", cookies.Value);

                driver.Manage().Cookies.DeleteCookieNamed("acceptedCookies");
                driver.Navigate().Refresh();
                Assert.NotNull(driver.FindElement(By.Id("CookiesBeingUsed")));
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