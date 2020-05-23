using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using ElEmegi.Ecommerce.Model.Entity;

namespace ElEmegi.Ecommerce.Web.UI.Models
{
    public class Cart
    {
        private List<CartLine> _cardLines = new List<CartLine>();
        private int cart_id = 0;
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
                cart_id++;
                _cardLines.Add(new CartLine() { Product = product, Quantity = quantity ,ID=cart_id});
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

        public void UpdateCart(Product product,int quantity)
        {
            var data =_cardLines.Find(x => x.Product.ID == product.ID);
            if (data !=null)
            {
                data.Quantity = quantity;
            }
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
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int ID { get; set; } = 1;
            public Product Product { get; set; }
            public int Quantity { get; set; }
        }
    }
}