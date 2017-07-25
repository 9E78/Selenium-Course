using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace selenium_course
{
    public class Application
    {
        private IWebDriver driver;

        private MainPage mainPage;
        private BasketPage basketPage;
        private ProductPage productPage;

        public Application()
        {
            driver = new ChromeDriver();
            mainPage = new MainPage(driver);
            basketPage = new BasketPage(driver);
            productPage = new ProductPage(driver);
        }

        public void Quit()
        {
            driver.Quit();
        }

        internal void OpenProductFromMainPage()
        {
            mainPage.Open();
            if (mainPage.IsAnyProductAvailible())
                mainPage.OpenAnyProduct();         
        }

        internal void WaitProductPageToLoad()
        {
            productPage.WaitToLoad();
        }

        internal int GetBasketCounter()
        {
            return productPage.GetBasketCounter();
        }

        internal void AddProductToBusket()
        {
            productPage
                .FillProductOptions()
                .AddToCart();
        }

        internal void Checkout()
        {
            basketPage.Open();
        }

        internal void RemoveItemFromBasket()
        {
            if (!basketPage.IsEmpty())
                basketPage.RemoveItemFromBasket();
        }

        internal bool IsBasketEmpty()
        {
            basketPage.Open();
            return basketPage.IsEmpty();
        }

    }
}