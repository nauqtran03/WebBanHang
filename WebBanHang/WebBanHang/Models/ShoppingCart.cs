using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> item { get; set; }; 
        public ShoppingCart() 
        {
            this.item = new List<ShoppingCartItem>();
        }
        public void AddToCart(int? id, int? quantity)
        {

        }
    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public string TotalPrice { get; set; }
    }
}