using System;
using NUnit.Framework;

namespace selenium_course
{
    [TestFixture]
    public class AddRemoveProductsToBasketPO : TestBase
    {
        [Test]
        public void l11t19_AddRemoveProductsToBasket()
        {
            for (int i = 0; i < 3; i++)
            {
                app.OpenProductFromMainPage();
                app.WaitProductPageToLoad();

                int quantity = app.GetBasketCounter();

                app.AddProductToBusket();

                Assert.AreEqual(app.GetBasketCounter(), quantity + 1, "Quantity of products in the basket was changed incorrecty");
            }

            app.Checkout();

            for (int i = 0; i < 3; i++)
            {
                app.RemoveItemFromBasket();
            }

            Assert.That(app.IsBasketEmpty());
        }
    }
}
