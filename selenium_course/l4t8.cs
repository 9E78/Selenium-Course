using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class CheckStickersOnMainPage
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void l4t8_CheckStickersOnMainPage()
        {
            driver.Url = "http://localhost:100/litecart/";

            foreach (IWebElement productItem in driver.FindElements(By.XPath("//li[@class='product column shadow hover-light']")))
            {
                productItem.FindElement(By.XPath("./a/div/div[contains(@class, 'sticker')]"));                
            }
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
