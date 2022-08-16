using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
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
        public void LoadApplicationPage()
        {
            using(IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);

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
    }
}