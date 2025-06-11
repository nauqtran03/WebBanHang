using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class MenuController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MenuTop()
        {
            var item = _dbConnect.Categories.OrderBy(x => x.Position).ToList();
            return PartialView("_MenuTop", item);
        }
        public ActionResult MenuProductCategory() 
        {
            var item = _dbConnect.ProductCategories.ToList();
            return PartialView("_MenuProductCategory",item);
        }
    }
}