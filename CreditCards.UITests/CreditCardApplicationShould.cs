using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Xunit.Abstractions;

namespace CreditCards.UITests
{
    public class CreditCardApplicationShould
    {
        const string Home_Url = "http://localhost:44108/";
        const string Apply_Url = "http://localhost:44108/Apply";
        const string Apply_Title = "Credit Card Application - Credit Cards";
        
        private readonly ITestOutputHelper _output;

        public CreditCardApplicationShould(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void BeInitiatedFromHomePage()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
                var applyLink = driver.FindElement(By.Name("ApplyLowRate"));
                applyLink.Click();

                Assert.Equal(Apply_Title, driver.Title);
                Assert.Equal(Apply_Url, driver.Url);
            }

        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);
               
                var carouselNext = driver.FindElement(By.CssSelector("[data-slide=\"next\"]"));
                carouselNext.Click();

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));

                var applyLink = wait.Until(d => d.FindElement(By.LinkText("Easy: Apply Now!")));

                applyLink.Click();

                Assert.Equal(Apply_Title, driver.Title);
                Assert.Equal(Apply_Url, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
            Func<IWebDriver, IWebElement> findEnableAndVisible = (d) =>
            {
                var e = d.FindElement(By.ClassName("customer-service-apply-now"));

                if (e is null)
                {
                    throw new NotFoundException();
                }

                if(e.Enabled && e.Displayed)
                {
                    return e;
                }
                throw new NotFoundException();
            };


            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(35));
                var applyLink = wait.Until(findEnableAndVisible);

                _output.WriteLine($"{DateTime.Now.ToLongTimeString()} Found element Displayed =  {applyLink.Displayed}, Enabled = {applyLink.Enabled}");

                applyLink.Click();

                Assert.Equal(Apply_Title, driver.Title);
                Assert.Equal(Apply_Url, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomCreating()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);

                var randomGreetingApplyLink = driver.FindElement(By.PartialLinkText("Apply Now!"));
                randomGreetingApplyLink.Click();

                Assert.Equal(Apply_Title, driver.Title);
                Assert.Equal(Apply_Url, driver.Url);
            }
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomCreating_Using_XPath()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Home_Url);

                var randomGreetingApplyLink = driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomGreetingApplyLink.Click();

                Assert.Equal(Apply_Title, driver.Title);
                Assert.Equal(Apply_Url, driver.Url);
            }
        }
    }
}
