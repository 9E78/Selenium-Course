using System;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace selenium_course
{
    [TestFixture]
    public class AddNewProduct
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
        public void l6t12_AddNewProduct()
        {
            driver.Url = "http://localhost:100/litecart/admin/?category_id=0&app=catalog&doc=edit_product";
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            driver.FindElement(By.Name("name[en]")).SendKeys("Cool Duck");
            driver.FindElement(By.Name("code")).SendKeys("rd006");

            driver.FindElement(By.CssSelector("input[data-name='Rubber Ducks']")).Click();

            driver.FindElement(By.CssSelector("input[name='product_groups[]'][value='1-3']")).Click();

            driver.FindElement(By.Name("quantity")).SendKeys("100");

            new SelectElement(driver.FindElement(By.Name("delivery_status_id"))).SelectByText("3-5 days");

            new SelectElement(driver.FindElement(By.Name("sold_out_status_id"))).SelectByText("Temporary sold out");
            

            string directory = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string imagePath = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "..\\..\\data\\cool_duck.jpg"));
            Assert.That(File.Exists(imagePath), string.Format("File for product image is not found, '{0}'", imagePath));            
            driver.FindElement(By.Name("new_images[]")).SendKeys(imagePath);

            driver.FindElement(By.Name("date_valid_from")).SendKeys("01/01/2015");
            driver.FindElement(By.Name("date_valid_to")).SendKeys("12/31/2017");

            driver.FindElement(By.CssSelector("a[href='#tab-information']")).Click();                        
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("manufacturer_id")));

            new SelectElement(driver.FindElement(By.Name("manufacturer_id"))).SelectByText("ACME Corp.");

            new SelectElement(driver.FindElement(By.Name("supplier_id"))).SelectByText("-- Select --");

            driver.FindElement(By.Name("keywords")).SendKeys("duck cool");

            driver.FindElement(By.Name("short_description[en]")).SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse sollicitudin ante massa, eget ornare");

            driver.FindElement(By.Name("description[en]")).Click();
            driver.FindElement(By.Name("description[en]")).SendKeys("Lorem ipsum dolor sit amet, consectetur adipiscing elit. Suspendisse sollicitudin ante massa, eget ornare libero porta congue. Cras scelerisque dui non consequat sollicitudin. Sed pretium tortor ac auctor molestie. Nulla facilisi. Maecenas pulvinar nibh vitae lectus vehicula semper. Donec et aliquet velit. Curabitur non ullamcorper mauris. In hac habitasse platea dictumst. Phasellus ut pretium justo, sit amet bibendum urna. Maecenas sit amet arcu pulvinar, facilisis quam at, viverra nisi. Morbi sit amet adipiscing ante. Integer imperdiet volutpat ante, sed venenatis urna volutpat a. Proin justo massa, convallis vitae consectetur sit amet, facilisis id libero.");

            driver.FindElement(By.Name("head_title[en]")).SendKeys("Cool Duck");

            driver.FindElement(By.Name("meta_description[en]")).SendKeys("cool duck");

            driver.FindElement(By.CssSelector("a[href='#tab-prices']")).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(By.Name("purchase_price")));

            driver.FindElement(By.Name("purchase_price")).Clear();
            driver.FindElement(By.Name("purchase_price")).SendKeys("123");

            new SelectElement(driver.FindElement(By.Name("purchase_price_currency_code"))).SelectByValue("EUR");

            driver.FindElement(By.Name("prices[USD]")).SendKeys("200");

            driver.FindElement(By.Name("prices[EUR]")).SendKeys("300");

            driver.FindElement(By.Name("save")).Click();
            
            driver.Url = "http://localhost:100/litecart/admin/?app=catalog&doc=catalog";

            driver.FindElement(By.XPath("//a[contains(text(), 'Cool Duck')]"));

        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}