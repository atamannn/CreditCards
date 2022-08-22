using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace CreditCards.UITests.PageObjectModels
{
    public class HomePage : Page
    {
        public HomePage(IWebDriver driver)
        {
            Driver = driver;
        }

        protected override string PageUrl => "http://localhost:44108/";

        protected override string PageTitle => "Home Page - Credit Cards";

        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();

                var productCells = Driver.FindElements(By.TagName("td"));

                for (int i = 0; i < productCells.Count - 1; i += 2)
                {
                    var name = productCells[i].Text;
                    var interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }

                return products.AsReadOnly();
            }
        }

        public string GenerationToken => Driver.FindElement(By.Id("GenerationToken")).Text;

        public bool IsCookieMessagePresent => Driver.FindElements(By.Id("CookiesBeingUsed")).Any();

        public void ClickContactFooterLink() => Driver.FindElement(By.Id("ContactFooter")).Click();

        public void ClickLiveChatFooterLink() => Driver.FindElement(By.Id("LiveChat")).Click();

        public void ClickLearnAboutUsLink() => Driver.FindElement(By.Id("LearnAboutUs")).Click();

        public ApplicationPage ClickApplyLowRateLink()
        {
            Driver.FindElement(By.Name("ApplyLowRate")).Click();
            return new ApplicationPage(Driver);
        }

        public void WaitForEasyApplicationCarouselPage(By by, TimeSpan timeOut)
        {
            Func<IWebDriver, IWebElement> findEnableAndVisible = (d) =>
            {
                var e = d.FindElement(by);

                if (e is null)
                {
                    throw new NotFoundException();
                }

                if (e.Enabled && e.Displayed)
                {
                    return e;
                }
                throw new NotFoundException();
            };

            var wait = new WebDriverWait(Driver, timeOut);

            var applyLink = wait.Until(findEnableAndVisible);

        }

        public ApplicationPage ClickApplyEasyApplicationLink()
        {
            var script = @"document.evaluate('//a[text()[contains(.,\'Easy: Apply Now!\')]]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.click();";
            Driver.FindElement(By.LinkText("Easy: Apply Now!")).Click();
            return new ApplicationPage(Driver);
        }

        public ApplicationPage ClickApplyNowlink()
        {
            Driver.FindElement(By.ClassName("customer-service-apply-now")).Click();
            return new ApplicationPage(Driver);
        }

    }
}
