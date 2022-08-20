using CreditCards.UITests.PageObjectModels;
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
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                var applicationPage = homePage.ClickApplyLowRateLink();

                applicationPage.EnsurePageLoaded();
            }

        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                var homePage = new HomePage(driver);
                homePage.NavigateTo();

                homePage.WaitForEasyApplicationCarouselPage();

                var applicationPage = homePage.ClickApplyEasyApplicationLink();

                applicationPage.EnsurePageLoaded();

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

        [Fact]
        public void BeSubmitedWhenValid()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(Apply_Url);

                driver.FindElement(By.Id("FirstName")).SendKeys("Sarah");
                driver.FindElement(By.Id("LastName")).SendKeys("Smith");
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                driver.FindElement(By.Id("Age")).SendKeys("18");
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                driver.FindElement(By.Id("Single")).Click();

                var businesSourceSelectElement = driver.FindElement(By.Id("BusinessSource"));
                var businesSource = new SelectElement(businesSourceSelectElement);
                
                Assert.Equal("I'd Rather Not Say", businesSource.SelectedOption.Text);

                foreach(var option in businesSource.Options)
                {
                    _output.WriteLine($"Value: {option.GetAttribute("value")}, Text: {option.Text}");
                }

                businesSource.SelectByValue("Email");
                businesSource.SelectByText("Internet Search");
                businesSource.SelectByIndex(4);

                driver.FindElement(By.Id("TermsAccepted")).Click();

                driver.FindElement(By.Id("Single")).Submit();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Sarah Smith", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("18", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            }
        }
    }
}
