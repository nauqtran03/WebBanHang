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
        public ActionResult Index(int? id)
        {
            
            var item = _dbConnect.Products.ToList();
            if (id != null) 
            {
            item = item.Where(x => x.ProductCategoryId == id).ToList();
            }
            return View(item);
        }
        public ActionResult Detail(string alias, int? id)
        {
            var item = _dbConnect.Products.Find(id);
            if (item != null)
            {
                _dbConnect.Products.Attach(item);
                item.ViewCount = item.ViewCount + 1;
                _dbConnect.Entry(item).Property(x=>x.ViewCount).IsModified = true;
                _dbConnect.SaveChanges();
            }
            return View(item);
        }
        public ActionResult ProductCate(string alias,int id)
        {

            var item = _dbConnect.Products.ToList();
            if (id > 0)
            {
                item = item.Where(x => x.ProductCategoryId == id).ToList();
            }
            var cate = _dbConnect.ProductCategories.Find(id);
            if (cate != null) 
            {
                ViewBag.CateName = cate.Title;
            }
            ViewBag.CateId = id;
            return View(item);
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