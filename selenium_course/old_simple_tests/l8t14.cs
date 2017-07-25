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
    public class CountryLinksOpenInNewTabs
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
        }

        [Test]
        public void l8t143_CountryLinksOpenInNewTabs()
        {
            driver.Url = "http://localhost:100/litecart/admin/?app=countries&doc=countries";
            wait.Until(driver => driver.FindElement(By.Name("username")));
            //wait.Until(driver => driver.FindElement(By.ClassName("logotype")));
            
            driver.FindElement(By.ClassName("input-wrapper")).Click();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElements(By.CssSelector("a[title=Edit]"))[0].Click();

            foreach (IWebElement linkIcon in driver.FindElements(By.ClassName("fa-external-link")))
            {
                string mainWindowId = driver.CurrentWindowHandle;
                ICollection<string> oldWindows = driver.WindowHandles;
                
                linkIcon.Click();

                string newWindowId = wait.Until<string>((driver) =>
                {
                    IList<string> newWindows = driver.WindowHandles.Except(oldWindows).ToList();
                    
                    if (newWindows.Count > 0)
                        return newWindows.ElementAt(0);
                    return null;
                });
                
                driver.SwitchTo().Window(newWindowId);
                driver.Close();
                driver.SwitchTo().Window(mainWindowId);
                
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