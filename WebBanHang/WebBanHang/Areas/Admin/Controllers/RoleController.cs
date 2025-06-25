using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var item = _dbConnect.Roles.ToList();
            return View(item);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model) 
        {
            if (ModelState.IsValid)
            {
                var role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbConnect));
                role.Create(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}

  