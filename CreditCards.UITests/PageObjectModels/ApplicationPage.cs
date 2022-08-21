using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace CreditCards.UITests.PageObjectModels
{
    public class ApplicationPage : Page
    {
        private readonly IWebDriver _driver;

        public ApplicationPage(IWebDriver webDriver) 
        { 
            _driver = webDriver;
        }

        protected override string PageUrl => "http://localhost:44108/Apply";

        protected override string PageTitle => "Credit Card Application - Credit Cards";

        public void EnterFirstName(string firstName) => _driver.FindElement(By.Id("FirstName")).SendKeys(firstName);

        public void EnterLastName(string lastName) => _driver.FindElement(By.Id("LastName")).SendKeys(lastName);

        public void EnterFrequentFlyerNumber(string frequentFlyerNumner) => _driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys(frequentFlyerNumner);

        public void EnterAge(string age) => _driver.FindElement(By.Id("Age")).SendKeys(age);

        public void EnterCrossAnnualIncome(string crossAnnualIncome) => _driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys(crossAnnualIncome);

        public void ChooseMaritalStatusSingle() => _driver.FindElement(By.Id("Single")).Click();

        public void ChoseBussinesSourceTV()
        {
            var businesSourceSelectElement = _driver.FindElement(By.Id("BusinessSource"));
            var businesSource = new SelectElement(businesSourceSelectElement);
            businesSource.SelectByValue("TV");
        }

        public void AcceptTerms() => _driver.FindElement(By.Id("TermsAccepted")).Click();

        public ApplicationCompletePage SubmitApplication() 
        {
            _driver.FindElement(By.Id("SubmitApplication")).Click();

            return new ApplicationCompletePage(_driver);
        }

        public void ClearAge()
        {
            _driver.FindElement(By.Id("Age")).Clear();
        }

        public ReadOnlyCollection<string> VaidationErrorMessages
        {
            get
            {
                return _driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"))
                    .Select(x => x.Text)
                    .ToList()
                    .AsReadOnly();

            }
        }

    }
}
