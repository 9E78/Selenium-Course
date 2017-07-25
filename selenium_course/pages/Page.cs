using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    internal class Page
    {
        protected IWebDriver driver;
        protected WebDriverWait wait;

        public Page(IWebDriver driver)
        {
            this.driver = driver;
            this.wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        internal Int16 GetBasketCounter()
        {
            Int16 counter;
            Int16.TryParse(driver.FindElement(By.ClassName("quantity")).Text, out counter);
            return counter;
        }
    }
}
