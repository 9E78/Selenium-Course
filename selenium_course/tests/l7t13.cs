using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class AddRemoveProductsToBasket
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void l7t13_AddRemoveProductsToBasket()
        {
            for (int i = 0; i < 3; i++)
            {
                driver.Url = "http://localhost:100/litecart/";
                driver.FindElement(By.ClassName("product")).Click();
                if (driver.FindElements(By.Name("options[Size]")).Count > 0)
                    new SelectElement(driver.FindElement(By.Name("options[Size]"))).SelectByIndex(1);

                wait.Until(ExpectedConditions.ElementIsVisible(By.Name("add_cart_product")));

                driver.FindElement(By.Name("add_cart_product")).Click();                
                wait.Until(driver => driver.FindElement(By.ClassName("quantity")).Text.Equals((i+1).ToString()));
            }

            driver.Url = "http://localhost:100/litecart/en/checkout";

            for (int i = 0; i < 3; i++)
            {
                if (driver.FindElements(By.ClassName("dataTable")).Count > 0)
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
                }
                else
                    break;
            }
            string emptyBasketText = driver.FindElement(By.CssSelector("div#checkout-cart-wrapper p em")).Text;
            StringAssert.AreEqualIgnoringCase(emptyBasketText, "There are no items in your cart.", "Text for empty card is different");
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}