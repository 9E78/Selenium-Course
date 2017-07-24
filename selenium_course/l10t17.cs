using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class CheckBrowserLogs
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            //wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [Test]
        public void l10t14_CheckBrowserLogs()
        {
            driver.Url = "http://localhost:100/litecart/admin/?app=catalog&doc=catalog&category_id=1";
            driver.FindElement(By.ClassName("input-wrapper")).Click();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            ICollection<string> productLinks = new List <string>();

            foreach (IWebElement item in driver.FindElements(By.XPath("//table[@class='dataTable']/tbody/tr[@class='row']/td[3]/a")))
            {
                productLinks.Add(item.GetAttribute("href"));
            }

            foreach (string link in productLinks)
            {
                driver.Url = link;
                Console.WriteLine("_____________________________________________________");
                Console.WriteLine(link);
                foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
                {
                    Console.WriteLine(l);
                }                
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