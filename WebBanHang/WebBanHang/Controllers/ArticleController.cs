using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class ArticleController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Article
        public ActionResult Index(string alias)
        {
            var item = _dbConnect.Posts.FirstOrDefault(c => c.Alias == alias);
            return View(item);
        }
    }
}