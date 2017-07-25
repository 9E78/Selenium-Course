using System;
using OpenQA.Selenium;

namespace selenium_course
{
    internal class MainPage: Page
    {
        public MainPage(IWebDriver driver) : base(driver) { }
        internal MainPage Open()
        {
            driver.Url = "http://localhost:100/litecart/";
            return this;
        }

        internal bool IsAnyProductAvailible()
        {
            return (driver.FindElements(By.ClassName("product")).Count > 0);
        }

        internal void OpenAnyProduct()
        {
            driver.FindElement(By.ClassName("product")).Click();
            wait.Until(driver => driver.FindElement(By.Id("box-product")));
        }
    }
}
