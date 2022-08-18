using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace CreditCards.UITests
{
    public class JavaScriptExamples
    {
        [Fact]
        public void ClickOverlayLink()
        {
            using (IWebDriver driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("http://localhost:44108/JSOverlay.html");

                var script = "return document.getElementById('HiddenLink').innerHTML";

                var js = (IJavaScriptExecutor)driver;

                var linkText = js.ExecuteScript(script);   

                Assert.Equal("Go to Pluralsight", linkText);
            }
        }
    }
}
