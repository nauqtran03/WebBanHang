using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class StatisticalController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/Statistical
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetStatistical(string fromDate, string toDate)
            {
            var query = from o in _dbConnect.Orders
                        join od in _dbConnect.OrderDetails
                        on o.Id equals od.OrderId
                        join p in _dbConnect.Products
                        on od.ProductId equals p.Id
                        select new
                        {
                            CreateDate = o.CreatedDate,
                            Quantity = od.Quantity,
                            Price = od.Price,
                            OriginalPrice = p.OriginalPrice
                        };
            if (!string.IsNullOrEmpty(fromDate))
            {
                DateTime startDate = DateTime.ParseExact(fromDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate >= startDate);
            }
            if (!string.IsNullOrEmpty(toDate))
            {
                DateTime endDate = DateTime.ParseExact(toDate, "dd/MM/yyyy", null);
                query = query.Where(x => x.CreateDate <= endDate);
            }
            var result = query.GroupBy(x => DbFunctions.TruncateTime(x.CreateDate)).Select(x => new
            {
                Date = x.Key.Value,
                TotalBuy = x.Sum(y=>y.Quantity * y.OriginalPrice),
                TotalSell = x.Sum(y=>y.Quantity * y.Price)
            }).Select(x => new
            {
                Date = x.Date,
                Revenue = x.TotalSell,
                Profit = x.TotalSell - x.TotalBuy
            });
            return Json(new {Data = result}, JsonRequestBehavior.AllowGet);
        }
    }
}