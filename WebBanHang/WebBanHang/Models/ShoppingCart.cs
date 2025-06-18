using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebBanHang.Models
{
    public class ShoppingCart
    {
        public List<ShoppingCartItem> Item { get; set; }
        public ShoppingCart() 
        {
            this.Item = new List<ShoppingCartItem>();
        }
        public void AddToCart(ShoppingCartItem item, int quantity)
        {
            var checkExit = Item.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (checkExit != null) 
            {
                checkExit.Quantity += quantity;
                checkExit.TotalPrice = checkExit.Price * checkExit.Quantity;
            }
            else
            {
                Item.Add(item);
            }
        }
        public void RemoveCart(int id)
        {
            var checkExit = Item.SingleOrDefault(x => x.ProductId == id);
            if (checkExit != null)
            {
                Item.Remove(checkExit);
            }
        }
        public void UpdateQuantity (int id, int quantity)
        {
            var checkExit = Item.SingleOrDefault(x=>x.ProductId == id);
            if (checkExit != null)
            {
                checkExit.Quantity = quantity;
                checkExit.TotalPrice = checkExit.Price *checkExit.Quantity;
            }
        }
        public decimal GetTotalPrice()
        {
            return Item.Sum(x => x.TotalPrice);
        }
        public decimal GetToTalQuantity()
        {
            return Item.Sum(x=>x.Quantity);
        }
        public void ClearCart()
        {
            Item.Clear();
        }
    }
    public class ShoppingCartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Alias { get; set; }
        public string CategoryName { get; set; }
        public string ProductImg {  get; set; }
        public int Quantity {  get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }
}