using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture(typeof(ChromeDriver))]
    [TestFixture(typeof(FirefoxDriver))]
    [TestFixture(typeof(InternetExplorerDriver))]
    public class CheckProductPageStyles<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        private IWebDriver driver;

        [SetUp]
        public void start()
        {
            this.driver = new TWebDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }        

        [Test]
        public void l5t10_CheckProductPageStyles()
        {
            driver.Url = "http://localhost:100/litecart";
            IWebElement campainsProduct = driver.FindElement(By.CssSelector("div#box-campaigns div ul li"));
            IWebElement regularPrice = campainsProduct.FindElement(By.XPath("./a[@class='link']/div[@class='price-wrapper']/s[@class='regular-price']"));
            IWebElement campaignPrice = campainsProduct.FindElement(By.XPath("./a[@class='link']/div[@class='price-wrapper']/strong[@class='campaign-price']"));

            // save properties for further comparison on product page
            string name = campainsProduct.FindElement(By.XPath("./a[@class='link']/div[@class='name']")).Text;
            string sRegularPrice = regularPrice.Text;
            string sCampaignPrice = campaignPrice.Text;

            //check styles
            string[] rgba = regularPrice.GetCssValue("color").Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(Convert.ToInt16(rgba[0]).Equals(Convert.ToInt16(rgba[1])) & Convert.ToInt16(rgba[1]).Equals(Convert.ToInt16(rgba[2])), "Regular Price is not grey");
            
            Assert.That(regularPrice.GetCssValue("text-decoration").Contains("line-through"), "Regular Price is not stroke");

            rgba = campaignPrice.GetCssValue("color").Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

            Assert.That(Convert.ToInt16(rgba[1]).Equals(0) & Convert.ToInt16(rgba[2]).Equals(0) & (Convert.ToInt16(rgba[0]) > 0), "Campaign Price is not red");

            Assert.That(campaignPrice.GetCssValue("font-weight").Equals("bold") || campaignPrice.GetCssValue("font-weight").Equals("900"), "Campaign Price is not bold");

            Assert.That(Convert.ToDecimal(regularPrice.GetCssValue("font-size").Replace("px", "")) < Convert.ToDecimal(campaignPrice.GetCssValue("font-size").Replace("px", "")), "Campaign Price is not bigger that Regular Price");

            driver.Url = campainsProduct.FindElement(By.XPath("./a[@class='link']")).GetAttribute("href");

            regularPrice = driver.FindElement(By.CssSelector("s.regular-price"));
            campaignPrice = driver.FindElement(By.CssSelector("strong.campaign-price"));


            StringAssert.AreEqualIgnoringCase(name, driver.FindElement(By.CssSelector("h1.title")).Text, "Product name on product page is not equal to the name from main page");
            StringAssert.AreEqualIgnoringCase(sRegularPrice, regularPrice.Text, string.Format("Regular price on product page '{0}' is not equal to the regular price on main page '{1}'", regularPrice.Text, sRegularPrice));
            StringAssert.AreEqualIgnoringCase(sCampaignPrice, campaignPrice.Text, string.Format("Campaign price on product page '{0}' is not equal to the campaign price on main page '{1}'", campaignPrice.Text, sCampaignPrice));

            //check styles on product page
            rgba = regularPrice.GetCssValue("color").Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(Convert.ToInt16(rgba[0]).Equals(Convert.ToInt16(rgba[1])) & Convert.ToInt16(rgba[1]).Equals(Convert.ToInt16(rgba[2])), "Regular Price is not grey");

            Assert.That(regularPrice.GetCssValue("text-decoration").Contains("line-through"), "Regular Price is not stroke");

            rgba = campaignPrice.GetCssValue("color").Replace("rgba(", "").Replace("rgb(", "").Replace(")", "").Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
            Assert.That(Convert.ToInt16(rgba[1]).Equals(0) & Convert.ToInt16(rgba[2]).Equals(0) & (Convert.ToInt16(rgba[0]) > 0), "Campaign Price is not red");

            Assert.That(campaignPrice.GetCssValue("font-weight").Equals("bold") || campaignPrice.GetCssValue("font-weight").Equals("700"), "Campaign Price is not bold");

            Assert.That(Convert.ToDecimal(regularPrice.GetCssValue("font-size").Replace("px", "")) < Convert.ToDecimal(campaignPrice.GetCssValue("font-size").Replace("px", "")), "Campaign Price is not bigger that Regular Price");
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }

        
    }
}
