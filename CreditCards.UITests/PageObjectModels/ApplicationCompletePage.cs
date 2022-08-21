using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModels
{
    public class ApplicationCompletePage 
    {
        private readonly string PAGE_URL = "http://localhost:44108/Apply";
        private readonly string APPLICATION_PAGE_TITIE = "Application Complete - Credit Cards";

        private readonly IWebDriver _driver;

        public ApplicationCompletePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string Decision => _driver.FindElement(By.Id("Decision")).Text;

        public string ReferenceNumber => _driver.FindElement(By.Id("ReferenceNumber")).Text;

        public string FullName => _driver.FindElement(By.Id("FullName")).Text;

        public string Age => _driver.FindElement(By.Id("Age")).Text;

        public string Income => _driver.FindElement(By.Id("Income")).Text;

        public string RelationshipStatus => _driver.FindElement(By.Id("RelationshipStatus")).Text;

        public string BusinessSource => _driver.FindElement(By.Id("BusinessSource")).Text;

        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            bool urlIsCorrect;

            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = _driver.Url.StartsWith(PAGE_URL);
            }
            else
            {
                urlIsCorrect = _driver.Url == PAGE_URL;
            }

            var pageHasLoaded = urlIsCorrect && _driver.Title == APPLICATION_PAGE_TITIE;

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = {_driver.Url}, " +
                    $"\n PageSource: {_driver.PageSource}");
            }
        }
    }
}
