using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class OpenAllMenuItems
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            // wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        [Test]
        public void l4t7_OpenAllMenuItems()
        {
            driver.Url = "http://localhost:100/litecart/admin/login.php";
            driver.FindElement(By.ClassName("input-wrapper")).Click();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            
            IWebElement menuItem;
            
            for (int i = 1; i <= driver.FindElements(By.CssSelector("ul#box-apps-menu li#app-")).Count; i++)
            {
                driver.FindElement(By.XPath(String.Format("//ul[@id='box-apps-menu']/li[{0}]", i.ToString()))).Click();

                int countItems = driver.FindElements(By.CssSelector("ul#box-apps-menu li#app- ul.docs li")).Count;

                if (countItems == 0)
                    driver.FindElement(By.CssSelector("h1"));
                else
                {
                    for (int j = 1; j <= countItems; j++)
                    {
                        driver.FindElement(By.XPath(String.Format("//ul[@id='box-apps-menu']/li[{0}]/ul[@class='docs']/li[{1}]", i.ToString(), j.ToString()))).Click();
                        driver.FindElement(By.CssSelector("h1"));
                    }
                }
                
            }
        }

        [Test]
        public void l4t7_OpenAllMenuItems_relative()
        {
            driver.Url = "http://localhost:100/litecart/admin/login.php";
            driver.FindElement(By.ClassName("input-wrapper")).Click();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            IWebElement menu = driver.FindElement(By.CssSelector("ul#box-apps-menu"));
            foreach (IWebElement menuItem in menu.FindElements(By.CssSelector("li#app-")))
            {
                menuItem.Click();
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
