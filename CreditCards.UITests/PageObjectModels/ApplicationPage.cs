using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace CreditCards.UITests.PageObjectModels
{
    public class ApplicationPage : Page
    {
        public ApplicationPage(IWebDriver webDriver) 
        {
            Driver = webDriver;
        }

        protected override string PageUrl => "http://localhost:44108/Apply";

        protected override string PageTitle => "Credit Card Application - Credit Cards";

        public void EnterFirstName(string firstName) => Driver.FindElement(By.Id("FirstName")).SendKeys(firstName);

        public void EnterLastName(string lastName) => Driver.FindElement(By.Id("LastName")).SendKeys(lastName);

        public void EnterFrequentFlyerNumber(string frequentFlyerNumner) => Driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys(frequentFlyerNumner);

        public void EnterAge(string age) => Driver.FindElement(By.Id("Age")).SendKeys(age);

        public void EnterCrossAnnualIncome(string crossAnnualIncome) => Driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys(crossAnnualIncome);

        public void ChooseMaritalStatusSingle() => Driver.FindElement(By.Id("Single")).Click();

        public void ChoseBussinesSourceTV()
        {
            var businesSourceSelectElement = Driver.FindElement(By.Id("BusinessSource"));
            var businesSource = new SelectElement(businesSourceSelectElement);
            businesSource.SelectByValue("TV");
        }

        public void AcceptTerms() => Driver.FindElement(By.Id("TermsAccepted")).Click();

        public ApplicationCompletePage SubmitApplication() 
        {
            Driver.FindElement(By.Id("SubmitApplication")).Click();

            return new ApplicationCompletePage(Driver);
        }

        public void ClearAge()
        {
            Driver.FindElement(By.Id("Age")).Clear();
        }

        public ReadOnlyCollection<string> VaidationErrorMessages
        {
            get
            {
                return Driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"))
                    .Select(x => x.Text)
                    .ToList()
                    .AsReadOnly();

            }
        }

    }
}
