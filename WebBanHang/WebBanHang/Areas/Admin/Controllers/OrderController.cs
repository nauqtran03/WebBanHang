using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/Order
        public ActionResult Index(int? page)
        {
            var item = _dbConnect.Orders.OrderByDescending(x => x.CreatedDate).ToList();
            if (page == null)
            {
                page = 1;
            }
            var pageNumber = page ?? 1;
            var pageSize = 10;
            ViewBag.PageSize = pageSize;
            ViewBag.PageNumber = pageNumber;
            return View(item.ToPagedList(pageNumber,pageSize));
        }

        public ActionResult View(int? id) 
        {
            var item = _dbConnect.Orders.Find(id);
            return View(item);
        }
        public ActionResult Partial_Product(int id) 
        {
            var item = _dbConnect.OrderDetails.Where(x => x.OrderId == id).ToList();
            return PartialView(item);
        }
        public ActionResult UpdateStatus(int id,int status)
        {
            var item = _dbConnect.Orders.Find(id);
            if (item!=null)
            {
                _dbConnect.Orders.Attach(item);
                item.TypeMethod = status;
                _dbConnect.Entry(item).Property(x=>x.TypeMethod).IsModified = true;
                _dbConnect.SaveChanges();
                return Json(new {message="Success",success =true});
            }
            return Json(new { message = "Unsuccess", success = false });
        }
    }
}