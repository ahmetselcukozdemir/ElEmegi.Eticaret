using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ElEmegi.Ecommerce.Web.UI.Controllers;
using System.Web.Mvc;

namespace ElEmegi.Ecommerce.Web.UI.Tests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void Index()
        {
            HomeController controller = new HomeController();
            ViewResult result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }
    }
   
}
