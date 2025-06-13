using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_ItemsByCateId()
        {
            var item = _dbConnect.Products.Where(x=>x.IsHome && x.IsActive).Take(12).ToList();
            return PartialView( item);
        }
        public ActionResult Partial_ProductSales() 
        {
            var item = _dbConnect.Products.Where(x=>x.IsSale).Take(10).ToList();
            return PartialView( item);
        }
    }
}