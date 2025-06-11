using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();  
        // GET: Admin/ProductImage
        public ActionResult Index(int Id)
        {
            ViewBag.ProductId = Id;
            var item = _dbConnect.ProductImages.Where(x => x.ProductId == Id).ToList();
            return View(item);
        }
        [HttpPost]
        public ActionResult AddImage(int productId,string url)
        {
            _dbConnect.ProductImages.Add(new ProductImage
            {
                ProductId = productId,
                Image = url,
                IsDefault = false
            });
            _dbConnect.SaveChanges();
            return Json(new { success = true });
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbConnect.ProductImages.Find(id);
            _dbConnect.ProductImages.Remove(item);
            _dbConnect.SaveChanges();
            return Json(new {success = true});
        }
    }
}