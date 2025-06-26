using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Models;
using PagedList;
using System.Net;
using Microsoft.Owin.Security;
using WebBanHang.Models.EF;
namespace WebBanHang.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext _dbConnect = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        

        // GET: Admin/Account
        public ActionResult Index(int? page)
        {

            var item = _dbConnect.Users.ToList();
            var pageSize = 10;
            var pageNumber = page ?? 1;

            return View(item.ToPagedList(pageNumber,pageSize));
        }
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }
        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(_dbConnect.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Phone = model.Phone,
                    FullName = model.FullName
                };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // Gán Role
                    if (!string.IsNullOrEmpty(model.Role))
                    {
                        await UserManager.AddToRoleAsync(user.Id, model.Role);
                    }

                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }

            ViewBag.Role = new SelectList(_dbConnect.Roles.ToList(), "Name", "Name", model.Role);
            return View(model);
        }
        //[HttpGet]
        //public ActionResult Edit(string username)
        //{
        //    var item = _dbConnect.Users.FirstOrDefault(c => c.UserName == username);
        //    if (item == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    var model = new EditAccountViewModel
        //    {
        //        UserName = item.UserName,
        //        FullName = item.FullName,
        //        Phone = item.Phone,
        //        Email = item.Email,
        //        Role = _dbConnect.Roles.FirstOrDefault(r => r.Users.Any(u => u.UserId == item.Id))?.Name
        //    };

        //    ViewBag.Role = new SelectList(_dbConnect.Roles.ToList(), "Name", "Name", model.Role);
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(EditAccountViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var item = _dbConnect.Users.FirstOrDefault(c => c.UserName == model.UserName);
        //        if (item == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        item.FullName = model.FullName;
        //        item.Phone = model.Phone;
        //        item.Email = model.Email;
        //        // Xử lý Role nếu cần
        //        _dbConnect.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.Role = new SelectList(_dbConnect.Roles.ToList(), "Name", "Name", model.Role);
        //    return View(model);
        //}
        [HttpPost]
        public ActionResult Delete(string username)
        {
            var item = _dbConnect.Users.FirstOrDefault(x => x.UserName == username);
            if (item != null)
            {
                _dbConnect.Users.Remove(item);
                _dbConnect.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
    }
}