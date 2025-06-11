using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();  
        // GET: Admin/ProductImage
        public ActionResult Index(int productId)
        {
            var item = _dbConnect.ProductImages.Where(x => x.ProductId == productId).ToList();
            return View(item);
        }
    }
}