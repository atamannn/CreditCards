using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace CreditCards.UITests.Helpers
{
    public static class Alert
    {
        public static IAlert? WaitGetAlert(this IWebDriver driver, int waitTimeInSeconds = 5)
        {
            IAlert? alert = null;

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTimeInSeconds));

            alert = wait.Until(d =>
            {
                try
                {
                    return driver.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            });
            
            return alert;
        }
    }
}
