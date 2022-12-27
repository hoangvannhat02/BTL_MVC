using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BTL_MVC
{
    public class MyCart
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImage { get; set; }
        public decimal? Price { get; set; }
        public byte? Quantily { get; set; }
        public decimal Acount { get; set; }
        public MyCart()
        {

        }
        public MyCart(int ProductId, string ProductName,string ProductImage,decimal? Price,byte? Quantily,decimal Acount)
        {
            this.ProductId = ProductId;
            this.ProductName = ProductName;
            this.Price = Price;
            this.Quantily = Quantily;
            this.Acount = Convert.ToDecimal(Price * Quantily);
        }
    }
}