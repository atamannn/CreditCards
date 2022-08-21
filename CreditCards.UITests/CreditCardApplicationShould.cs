using CreditCards.UITests.PageObjectModels;
using OpenQA.Selenium;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardApplicationShould : IClassFixture<ChromeDriveFixture>
    {
        const string Home_Url = "http://localhost:44108/";
        const string Apply_Url = "http://localhost:44108/Apply";
        const string Apply_Title = "Credit Card Application - Credit Cards";
        
        private readonly ChromeDriveFixture _chromeDriveFixture;

        public CreditCardApplicationShould(ChromeDriveFixture chromeDriveFixture)
        {
            _chromeDriveFixture = chromeDriveFixture;
            _chromeDriveFixture.Driver.Manage().Cookies.DeleteAllCookies();
            _chromeDriveFixture.Driver.Navigate().GoToUrl("about:blank");
        }

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
            var homePage = new HomePage(_chromeDriveFixture.Driver);
            homePage.NavigateTo();

            var applicationPage = homePage.ClickApplyLowRateLink();

            applicationPage.EnsurePageLoaded();

        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {

            var homePage = new HomePage(_chromeDriveFixture.Driver);
            homePage.NavigateTo();

            homePage.WaitForEasyApplicationCarouselPage(By.LinkText("Easy: Apply Now!"), TimeSpan.FromSeconds(11));

            var applicationPage = homePage.ClickApplyEasyApplicationLink();

            applicationPage.EnsurePageLoaded();


        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {

            var homePage = new HomePage(_chromeDriveFixture.Driver);
            homePage.NavigateTo();

            homePage.WaitForEasyApplicationCarouselPage(By.ClassName("customer-service-apply-now"), TimeSpan.FromSeconds(35));

            var applicationPage = homePage.ClickApplyNowlink();

            applicationPage.EnsurePageLoaded();

        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomCreating()
        {

            _chromeDriveFixture.Driver.Navigate().GoToUrl(Home_Url);

            var randomGreetingApplyLink = _chromeDriveFixture.Driver.FindElement(By.PartialLinkText("Apply Now!"));
            randomGreetingApplyLink.Click();

            Assert.Equal(Apply_Title, _chromeDriveFixture.Driver.Title);
            Assert.Equal(Apply_Url, _chromeDriveFixture.Driver.Url);

        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomCreating_Using_XPath()
        {

            _chromeDriveFixture.Driver.Navigate().GoToUrl(Home_Url);

            var randomGreetingApplyLink = _chromeDriveFixture.Driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
            randomGreetingApplyLink.Click();

            Assert.Equal(Apply_Title, _chromeDriveFixture.Driver.Title);
            Assert.Equal(Apply_Url, _chromeDriveFixture.Driver.Url);

        }

        [Fact]
        public void BeSubmitedWhenValid()
        {
            const string FIRST_NAME = "Sarah";
            const string LAST_NAME = "Smith";
            const string NUMBER = "123456-A";
            const string AGE = "18";
            const string INCOME = "50000";



            var applycationPage = new ApplicationPage(_chromeDriveFixture.Driver);
            applycationPage.NavigateTo();

            applycationPage.EnterFirstName(FIRST_NAME);
            applycationPage.EnterLastName(LAST_NAME);
            applycationPage.EnterFrequentFlyerNumber(NUMBER);
            applycationPage.EnterAge(AGE);
            applycationPage.EnterCrossAnnualIncome(INCOME);
            applycationPage.ChooseMaritalStatusSingle();
            applycationPage.ChoseBussinesSourceTV();
            applycationPage.AcceptTerms();

            var applicationCompletePage = applycationPage.SubmitApplication();

            applicationCompletePage.EnsurePageLoaded();

            Assert.Equal("ReferredToHuman", applicationCompletePage.Decision);
            Assert.NotEmpty(applicationCompletePage.ReferenceNumber);
            Assert.Equal($"{FIRST_NAME} {LAST_NAME}", applicationCompletePage.FullName);
            Assert.Equal(AGE, applicationCompletePage.Age);
            Assert.Equal(INCOME, applicationCompletePage.Income);
            Assert.Equal("Single", applicationCompletePage.RelationshipStatus);
            Assert.Equal("TV", applicationCompletePage.BusinessSource);

        }

        [Fact]
        public void BeSubmitetWhenValidationErrorsCorrected()
        {
            const string FIRST_NAME = "Sarah";
            const string INVALID_AGE = "17";
            const string VALID_AGE = "18";


            var applycationPage = new ApplicationPage(_chromeDriveFixture.Driver);
            applycationPage.NavigateTo();

            applycationPage.EnterFirstName(FIRST_NAME);
            // Don't entre last name
            applycationPage.EnterFrequentFlyerNumber("123456-A");
            applycationPage.EnterAge(INVALID_AGE);
            applycationPage.EnterCrossAnnualIncome("50000");
            applycationPage.ChooseMaritalStatusSingle();
            applycationPage.ChoseBussinesSourceTV();
            applycationPage.AcceptTerms();
            applycationPage.SubmitApplication();

            // Assert that validation failed
            Assert.Equal(2, applycationPage.VaidationErrorMessages.Count);
            Assert.Contains("Please provide a last name", applycationPage.VaidationErrorMessages);
            Assert.Contains("You must be at least 18 years old", applycationPage.VaidationErrorMessages);

            // Fix Errors
            applycationPage.EnterLastName("Smith");
            applycationPage.ClearAge();
            applycationPage.EnterAge(VALID_AGE);

            var applicationCopitePage = applycationPage.SubmitApplication();
            applicationCopitePage.EnsurePageLoaded();

        }
    }
}
