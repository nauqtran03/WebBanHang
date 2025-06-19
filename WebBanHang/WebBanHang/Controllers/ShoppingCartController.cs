using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Item.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        { 
            return View();
        }
        public ActionResult Partial_CheckOut()
        {
            return PartialView();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckOut(OrderViewModel req)
        {
            var code = new {success = false, Code = -1};
            if (ModelState.IsValid)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart != null && cart.Item.Any())
                {
                    Order order = new Order();
                    order.CustomerName = req.CustomerName;
                    order.Phone = req.Phone;
                    order.Address = req.Address;
                    cart.Item.ForEach(x => order.OrderDetails.Add(new OrderDetail
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                    }));
                    order.TotalAmount = cart.Item.Sum(x => (x.Price * x.Quantity));
                    order.TypeMethod = req.TypeMethod;
                    order.CreatedDate = DateTime.Now;
                    order.CreatedBy = req.Phone;
                    order.ModifiedDate = DateTime.Now;
                    Random rd = new Random();
                    order.Code = "QN" + rd.Next(0,9)+ rd.Next(0, 9)+rd.Next(0, 9)+rd.Next(0, 9)+ rd.Next(0, 9);
                    _dbConnect.Orders.Add(order);
                    _dbConnect.SaveChanges();
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }       
            }
            return Json(code);
        }
        public ActionResult Partial_Item_Checkout()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Item.Any()) 
            {
                return PartialView(cart.Item);
            }
            return PartialView();
            }
        public ActionResult Partial_Item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.Item.Any())
            {
                ViewBag.CheckCart = cart;
                return PartialView(cart.Item);
            }
            return PartialView();
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var code = new { success = false, msg = "", code = -1 ,count =0 };
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.Item.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null) 
                {
                    cart.RemoveCart(id);
                    code = new { success = true, msg ="", code = 1, count = cart.Item.Count };
                }
            }
            return Json(code);
        }
        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { success=true, count = cart.Item.Count }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { count = 0 },JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddToCart(int id, int quantity)
        {
            var code = new { success = false, msg = "", code = -1,count = 0};
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.Id == id);
            if (checkProduct!=null)
            {
               
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();  
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.Id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategorys.Title,
                    Alias = checkProduct.Alias,
                    Quantity = quantity
                };
                if (checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImages.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { success = true, msg = "Product added to cart successfully!", code = 1, count = cart.Item.Count};
            }
            return Json(code);
        }
        [HttpPost]
        public ActionResult Update(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id, quantity);
                return Json(new { success =true });
            }
            return Json(new {success= false});
        }
        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            { 
                cart.ClearCart();
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, msg = "There are no items in your cart" });
            }
        }
    }
}