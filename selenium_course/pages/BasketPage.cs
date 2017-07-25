using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Support.PageObjects;

namespace selenium_course
{
    internal class BasketPage: Page
    {
        public BasketPage(IWebDriver driver) : base(driver)
        {
            PageFactory.InitElements(driver, this);
        }

        internal BasketPage Open()
        {
            driver.Url = "http://localhost:100/litecart/en/checkout";
            wait.Until(ExpectedConditions.ElementExists(By.XPath("//div[contains(@id, 'checkout')]")));
            return this;
        }

        internal bool IsEmpty()
        {
            return (driver.FindElements(By.ClassName("dataTable")).Count > 0);
        }

        internal BasketPage RemoveItemFromBasket()
        {
            IWebElement productList = driver.FindElement(By.ClassName("dataTable"));
            for (int count = 0; ; count++)
            {
                if (count > 5)
                    throw new NoSuchElementException();
                try
                {
                    driver.FindElement(By.Name("remove_cart_item")).Click();
                    break;
                }
                catch (NoSuchElementException) { }
            }
            wait.Until(ExpectedConditions.StalenessOf(productList));
            return this;
        }
    }
}
