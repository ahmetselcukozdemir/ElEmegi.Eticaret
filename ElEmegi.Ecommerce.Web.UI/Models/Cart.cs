using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ElEmegi.Ecommerce.Core.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Cart
    {
        private List<CartLine> _cardLines = new List<CartLine>();
        public List<CartLine> CartLines
        {
            get
            {
                return _cardLines;
            }

        }

        public void AddProduct(Product product, int quantity)
        {
            var line = _cardLines.Where(i => i.Product.ID == product.ID).FirstOrDefault();
            if (line == null)
            {
                _cardLines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                //sepette aynı daha önce eklenen bir ürün varsa adetini bir arttır.
                line.Quantity += quantity;
            }
        }

        public void DeleteProduct(Product product)
        {
            _cardLines.RemoveAll(i => i.Product.ID == product.ID);
        }

        public double Total()
        {
            return _cardLines.Sum(i => i.Product.Price * i.Quantity);
        }
        public void Clear()
        {
            _cardLines.Clear();
        }
        public class CartLine
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}