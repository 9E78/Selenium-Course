using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    internal class ProductPage: Page
    {
        public ProductPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal ProductPage Open(string url)
        {
            driver.Url = url;
            return this;
        }

        internal ProductPage WaitToLoad()
        {
            wait.Until(driver => driver.FindElement(By.Id("box-product")));
            return this;
        }

        internal ProductPage FillProductOptions()
        {
            if (driver.FindElements(By.Name("options[Size]")).Count > 0)
                new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByIndex(1);
            return this;
        }

        internal ProductPage AddToCart()
        {
            IWebElement quantity = driver.FindElement(By.ClassName("quantity"));
            int c = this.GetBasketCounter();
            driver.FindElement(By.Name("add_cart_product")).Click();
            //wait.Until(ExpectedConditions.StalenessOf(quantity));
            wait.Until(driver => {
                if (this.GetBasketCounter() > c)
                    return driver;
                return null;
            });

            return this;
        }

    }
}
