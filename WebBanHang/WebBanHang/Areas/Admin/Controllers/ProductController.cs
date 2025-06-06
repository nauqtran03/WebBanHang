using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/Product
        public ActionResult Index(string searchText,int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.searchText = searchText;
            var query = _dbConnect.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                string normallizedSearch = searchText.Replace(" ", "-");
                query = query.Where(x => x.Title.Contains(searchText) || x.Alias.Contains(normallizedSearch));
                ViewBag.searchText = searchText;
            }
            var item = query.OrderByDescending(x => x.Id).ToPagedList(pageNumber, pageSize);
            ViewBag.pageSize = pageSize;
            ViewBag.pageNumber = pageNumber;
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
    }
}