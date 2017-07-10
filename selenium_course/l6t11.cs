using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class RegisterUser
    {
        private IWebDriver driver;        

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Test]
        public void l6t11_RegisterUser()
        {
            driver.Url = "http://localhost:100/litecart/en/create_account";
            string ID = Guid.NewGuid().ToString();

            driver.FindElement(By.CssSelector("input[name=tax_id]")).SendKeys("123456");
            driver.FindElement(By.CssSelector("input[name=company]")).SendKeys(string.Format("{0} Company", ID));
            driver.FindElement(By.CssSelector("input[name=firstname]")).SendKeys(string.Format("{0}Firstname", ID));
            driver.FindElement(By.CssSelector("input[name=lastname]")).SendKeys(string.Format("{0}Lastname", ID));

            driver.FindElement(By.CssSelector("input[name=address1]")).SendKeys(string.Format("{0} address1", ID));
            driver.FindElement(By.CssSelector("input[name=address2]")).SendKeys(string.Format("{0} address2", ID));
            driver.FindElement(By.CssSelector("input[name=postcode]")).SendKeys("12345");
            driver.FindElement(By.CssSelector("input[name=city]")).SendKeys(string.Format("{0} City", ID));

            StringAssert.AreNotEqualIgnoringCase(driver.FindElement(By.CssSelector("span.select2-selection__rendered")).Text, "United States");
            new SelectElement(driver.FindElement(By.CssSelector("select[name=country_code]"))).SelectByValue("US");
            StringAssert.AreEqualIgnoringCase(driver.FindElement(By.CssSelector("span.select2-selection__rendered")).Text, "United States");

            new SelectElement(driver.FindElement(By.CssSelector("select[name=zone_code]"))).SelectByValue("CA");

            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(string.Format("{0}@ya.ru", ID));
            driver.FindElement(By.CssSelector("input[name=phone]")).Clear();
            driver.FindElement(By.CssSelector("input[name=phone]")).SendKeys(string.Format("+79211234567", ID));
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(ID);
            driver.FindElement(By.CssSelector("input[name=confirmed_password]")).SendKeys(ID);

            driver.FindElement(By.CssSelector("button[name=create_account]")).Click();

            StringAssert.AreEqualIgnoringCase(driver.FindElement(By.CssSelector("div#box-account h3.title")).Text, "Account");

            driver.Url = "http://localhost:100/litecart/en/logout";

            driver.FindElement(By.CssSelector("input[name=email]")).SendKeys(string.Format("{0}@ya.ru", ID));
            driver.FindElement(By.CssSelector("input[name=password]")).SendKeys(ID);
            driver.FindElement(By.CssSelector("button[name=login]")).Click();
                        
            StringAssert.AreEqualIgnoringCase(driver.FindElement(By.CssSelector("div#box-account h3.title")).Text, "Account");

            driver.Url = "http://localhost:100/litecart/en/logout";
        }
        
        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
