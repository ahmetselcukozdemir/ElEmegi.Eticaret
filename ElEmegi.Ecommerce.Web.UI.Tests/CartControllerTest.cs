using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElEmegi.Ecommerce.Web.UI.Controllers;
using System.Web.Mvc;

namespace ElEmegi.Ecommerce.Web.UI.Tests
{
    [TestClass]
    public class CartControllerTest
    {
        [TestMethod]
        public void Index()
        {
            var controller = new CartController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
}
