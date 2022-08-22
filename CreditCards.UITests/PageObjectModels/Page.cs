using OpenQA.Selenium;

namespace CreditCards.UITests.PageObjectModels
{
    public class Page
    {
        protected IWebDriver Driver;
        protected virtual string PageUrl { get; }
        protected virtual string PageTitle { get; }

        public void NavigateTo()
        {
            Driver.Navigate().GoToUrl(PageUrl);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            bool urlIsCorrect;

            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = Driver.Url.StartsWith(PageUrl);
            }
            else
            {
                urlIsCorrect = Driver.Url == PageUrl;
            }

            var pageHasLoaded = urlIsCorrect && Driver.Title == PageTitle;

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = {Driver.Url}, " +
                    $"\n PageSource: {Driver.PageSource}");
            }
        }
    }
}
