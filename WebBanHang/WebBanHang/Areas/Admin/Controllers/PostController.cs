using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class PostController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/Post
        public ActionResult Index()
        {
            var items = _dbConnect.Posts.OrderByDescending(x => x.Id).ToList();
            return View(items);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Post model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang.Models.Common.Filter.FilterChar(model.Title);
                model.CategoryId = 2;
                _dbConnect.Posts.Add(model);
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            var item = _dbConnect.Posts.FirstOrDefault(c => c.Id == id);

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang.Models.Common.Filter.FilterChar(model.Title);
                _dbConnect.Posts.Attach(model);
                _dbConnect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = _dbConnect.Posts.Find(id);
            if (item != null)
            {
                _dbConnect.Posts.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbConnect.Posts.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbConnect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive });
            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult DeleteAll(string ids)
        {
            if (string.IsNullOrEmpty(ids))
            {
                return Json(new { success = false, message = "No IDs provided" });
            }

            try
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (int.TryParse(item, out int id))
                        {
                            var obj = _dbConnect.Posts.Find(id);
                            if (obj != null)
                            {
                                _dbConnect.Posts.Remove(obj);
                            }
                        }
                    }
                    _dbConnect.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false, message = "No valid IDs" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}