﻿using OpenQA.Selenium;
using System.Collections.ObjectModel;

namespace CreditCards.UITests.PageObjectModels
{
    public class HomePage
    {
        const string HOME_URL = "http://localhost:44108/";
        const string HOME_TITLE = "Home Page - Credit Cards";

        private readonly IWebDriver _driver;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
        }

        public ReadOnlyCollection<(string name, string interestRate)> Products
        {
            get
            {
                var products = new List<(string name, string interestRate)>();

                var productCells = _driver.FindElements(By.TagName("td"));

                for (int i = 0; i < productCells.Count - 1; i += 2)
                {
                    var name = productCells[i].Text;
                    var interestRate = productCells[i + 1].Text;
                    products.Add((name, interestRate));
                }

                return products.AsReadOnly();
            }
        }

        public string GenerationToken => _driver.FindElement(By.Id("GenerationToken")).Text;

        public bool IsCookieMessagePresent => _driver.FindElements(By.Id("CookiesBeingUsed")).Any();

        public void ClickContactFooterLink() => _driver.FindElement(By.Id("ContactFooter")).Click();

        public void ClickLiveChatFooterLink() => _driver.FindElement(By.Id("LiveChat")).Click();

        public void ClickLearnAboutUsLink() => _driver.FindElement(By.Id("LearnAboutUs")).Click();

        public void NavigateTo()
        {
            _driver.Navigate().GoToUrl(HOME_URL);
            EnsurePageLoaded();
        }

        public void EnsurePageLoaded(bool onlyCheckUrlStartsWithExpectedText = true)
        {
            bool urlIsCorrect;

            if (onlyCheckUrlStartsWithExpectedText)
            {
                urlIsCorrect = _driver.Url.StartsWith(HOME_URL);
            }
            else
            {
                urlIsCorrect = _driver.Url == HOME_URL;
            }

            var pageHasLoaded = urlIsCorrect && _driver.Title == HOME_TITLE;

            if (!pageHasLoaded)
            {
                throw new Exception($"Failed to load page. Page URL = {_driver.Url}, " +
                    $"\n PageSource: {_driver.PageSource}");
            }
        }

        
    }
}