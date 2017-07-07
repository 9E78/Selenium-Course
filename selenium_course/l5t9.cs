using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class CheckCountriesGeoZonesSorting
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        [Test]
        public void l5t9_CheckCountriesSorting()
        {
            driver.Url = "http://localhost:100/litecart/admin/?app=countries&doc=countries";
            driver.FindElement(By.ClassName("input-wrapper")).Click();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            IWebElement countriesForm = driver.FindElement(By.CssSelector("form[name=countries_form]"));
            
            // This part is awful because I can't understand wich collection to use in c#. Why not python?..

            List<string> nonEmptyCountries = new List<string>();
            IWebElement Country;
            string prevCountry = "";
            string curCountry = "";


            foreach (IWebElement row in countriesForm.FindElements(By.XPath("./table/tbody/tr[@class='row']")))
            {
                // Check alphabetical order
                Country = row.FindElement(By.XPath("./td[5]/a"));
                curCountry = Country.Text;
                Assert.That((string.Compare(prevCountry, curCountry) <= 0), string.Format("String '{0}' is greater than '{1}'. Alphabetical order is broken.", prevCountry, curCountry));
                prevCountry = curCountry;

                // Collect link if Zones greater than zero
                if (Convert.ToInt16((row.FindElement(By.XPath("./td[6]"))).Text) > 0)
                    nonEmptyCountries.Add(Country.GetAttribute("href"));
            }
                       
            foreach (string link in nonEmptyCountries)
            {
                driver.Url = link;
                prevCountry = "";

                // row[1] is a header, row[max] always has empty values
                for (int i = 2; i < driver.FindElements(By.CssSelector("table#table-zones tbody tr")).Count; i++)
                {
                    curCountry = driver.FindElement(By.XPath(string.Format("//table[@id='table-zones']/tbody/tr[{0}]/td[3]", i.ToString()))).Text;
                    Assert.That((string.Compare(prevCountry, curCountry) <= 0), string.Format("String '{0}' is greater than '{1}'. Alphabetical order is broken on page {2}.", prevCountry, curCountry, link));
                    prevCountry = curCountry;
                }
            }
            
        }

        [Test]
        public void l5t9_CheckGeoZonesSorting()
        {
            driver.Url = "http://localhost:100/litecart/admin/?app=geo_zones&doc=geo_zones";
            driver.FindElement(By.ClassName("input-wrapper")).Click();
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            IWebElement geoZonesForm = driver.FindElement(By.CssSelector("form[name=geo_zones_form]"));
            
            List<string> geoZones = new List<string>();

            foreach (IWebElement row in geoZonesForm.FindElements(By.XPath("./table/tbody/tr[@class='row']")))
            {
                // Collect link                
                geoZones.Add(row.FindElement(By.XPath("./td[3]/a")).GetAttribute("href"));
            }

            foreach (string link in geoZones)
            {
                driver.Url = link;

                string prevCountry = "";
                string curCountry = "";

                // row[1] is a header, row[max] always has empty values
                for (int i = 2; i < driver.FindElements(By.CssSelector("table#table-zones tbody tr")).Count; i++)
                {
                    curCountry = driver.FindElement(By.XPath(string.Format("//table[@id='table-zones']/tbody/tr[{0}]/td[3]/select/option[@selected='selected']", i.ToString()))).GetAttribute("textContent");
                    Assert.That((string.Compare(prevCountry, curCountry) <= 0), string.Format("String '{0}' is greater than '{1}'. Alphabetical order is broken on page {2}.", prevCountry, curCountry, link));
                    prevCountry = curCountry;
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
