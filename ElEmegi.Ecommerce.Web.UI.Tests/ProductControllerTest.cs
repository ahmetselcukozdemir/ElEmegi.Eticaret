using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElEmegi.Ecommerce.Web.UI.Controllers;
using System.Web.Mvc;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Tests
{
    [TestClass]
    public class ProductControllerTest
    {
        [TestMethod]
        public void TestDetailsViewData()
        {
            var controller = new ProductsController();
            var result = controller.Details(2) as ViewResult;
            var product = (Product)result.ViewData.Model;
            Assert.AreEqual("Laptop", product.Name);
        }
    }
}
