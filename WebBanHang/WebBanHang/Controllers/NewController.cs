using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using PagedList;
namespace WebBanHang.Controllers
{
    public class NewController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: New
        public ActionResult Index(int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var item = _dbConnect.News.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);
            ViewBag.pageSize = pageSize;
            ViewBag.pageNumber = pageNumber;
            return View(item);
        }
        public ActionResult Detail(int id)
        {
            var item = _dbConnect.News.Find(id);
            return View(item);
        }
        public ActionResult Partial_News_Home()
        {
            var item = _dbConnect.News.Take(3).ToList();
            return PartialView(item);
        }
    }
}