using ElEmegi.Ecommerce.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Xml;

namespace ElEmegi.Ecommerce.API.Controllers
{
    public class ProductController : ApiController
    {
        private DataContext db = new DataContext();
        public List<Category> Get()
        {
            var data = db.Categories.ToList();
            return data;
        }

        public Product Get(int id)
        {
            var data = db.Products.Where(x => x.ID == id).FirstOrDefault();
            return data;
        }
    }
}
