using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class NewController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index()
        {
            var item = _dbConnect.News.OrderByDescending(x=>x.Id).ToList();
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
    }
}