using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using WebBanHang.Models.EF;

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
            ViewBag.ProductCategory = new SelectList(_dbConnect.ProductCategories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model, List<string> Images, List<int> rDefault)
        {
            if (ModelState.IsValid) 
            {
                if (Images != null && Images.Count > 0) 
                {
                    for (int i = 0; i < Images.Count; i++) 
                    {
                        if( i + 1 == rDefault[0])
                        {
                            model.Image = Images[i];
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = true
                            });
                        }
                        else
                        {
                            model.ProductImages.Add(new ProductImage
                            {
                                ProductId = model.Id,
                                Image = Images[i],
                                IsDefault = false
                            });
                        }
                    }
                }
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                if (string.IsNullOrEmpty(model.SeoTitle))
                {
                    model.SeoTitle= model.Title;
                }
                model.Alias = WebBanHang.Models.Common.Filter.FilterChar(model.Title);
                _dbConnect.Products.Add(model);
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(_dbConnect.ProductCategories.ToList(), "Id", "Title");
            return View(model);
        }
        public ActionResult Edit (int id)
        {
            ViewBag.ProDuctCategory = new SelectList(_dbConnect.ProductCategories.ToList(), "Id", "Title");
            var item = _dbConnect.Products.Find(id);
            return View(item);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                model.Alias = WebBanHang.Models.Common.Filter.FilterChar(model.Title);
                _dbConnect.Products.Attach(model);
                _dbConnect.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id) 
        {
            var item = _dbConnect.Products.Find(id);
            if (item != null)
            {
                _dbConnect.Products.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new {success = false});
        }
        [HttpPost]
        public ActionResult IsActive(int id)
        {
            var item = _dbConnect.Products.Find(id);
            if (item != null)
            {
                item.IsActive = !item.IsActive;
                _dbConnect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return Json(new { success = true, isActive = item.IsActive});
            }
            return Json(new {success = false});
        }
        [HttpPost]
        public ActionResult IsHome(int id)
        {
            var item = _dbConnect.Products.Find(id);
            if (item != null) 
            {
                item.IsHome = !item.IsHome;
                _dbConnect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return Json(new { success = true, isHome = item.IsHome});
            }
            return Json(new {success = false});
        }
        [HttpPost]
        public ActionResult IsSale(int id)
        {
            var item = _dbConnect.Products.Find(id);
            if (item != null)
            {
                item.IsSale = !item.IsSale;
                _dbConnect.Entry(item).State = System.Data.Entity.EntityState.Modified;
                _dbConnect.SaveChanges();
                return Json(new { success = true, isSale = item.IsSale });
            }
            return Json(new {success = false});
        }
    }
}