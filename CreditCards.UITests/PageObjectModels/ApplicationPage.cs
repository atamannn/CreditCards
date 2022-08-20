using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModels
{
    public class ApplicationPage
    {
        const string PAGE_URL = "http://localhost:44108/Apply";
        const string APPLICATION_PAGE_TITIE = "Credit Card Application - Credit Cards";

        private readonly IWebDriver _driver;

        public ApplicationPage(IWebDriver webDriver) 
        { 
            _driver = webDriver;
        }

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(PAGE_URL);
            EnsurePageLoaded();
        }

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
