﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;
using PagedList;

namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Employee")]
    public class NewController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/News
        public ActionResult Index(string searchText,int? page)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            ViewBag.searchText = searchText;
            var query = _dbConnect.News.AsQueryable();
            if (!string.IsNullOrEmpty(searchText))
            {
                string normallizedSearch = searchText.Replace(" ","-");
                query = query.Where(x => x.Title.Contains(searchText)|| x.Alias.Contains(normallizedSearch));
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(New model)
        {
            if (ModelState.IsValid)
            {
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang.Models.Common.Filter.FilterChar(model.Title);
                model.CategoryId = 6;
                _dbConnect.News.Add(model);
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public ActionResult Edit(int? id)
        {
            var item = _dbConnect.News.FirstOrDefault(c => c.Id == id);

            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(New model)
        {
            if (ModelState.IsValid)
            {              
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang.Models.Common.Filter.FilterChar(model.Title);
                _dbConnect.News.Attach(model);
                _dbConnect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id) 
        {
            var item = _dbConnect.News.Find(id);
            if (item != null) 
            {
                _dbConnect.News.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });

        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbConnect.News.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbConnect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return Json(new { success = true , isActive =item.IsActive});
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
                            var obj = _dbConnect.News.Find(id);
                            if (obj != null)
                            {
                                _dbConnect.News.Remove(obj);
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