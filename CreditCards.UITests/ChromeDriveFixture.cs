using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards.UITests
{
    public class ChromeDriveFixture : IDisposable
    {
        public IWebDriver Driver { get; set; }

        public ChromeDriveFixture()
        {
            Driver = new ChromeDriver();
        }

        public void Dispose()
        {
            Driver.Dispose();
        }
    }
}
