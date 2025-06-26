using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class NewController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: New
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Partial_News_Home()
        {
            var item = _dbConnect.News.Take(3).ToList();
            return PartialView(item);
        }
    }
}