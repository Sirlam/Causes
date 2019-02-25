using Causes.UI.Web.Data;
using Causes.UI.Web.Identity.Models;
using Causes.UI.Web.Models;
using Causes.UI.Web.Security;
using Causes.UI.Web.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Causes.UI.Web.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private CustomIdentity identity;
        private CustomMembershipProvider member;
        private AppDbContext db;

        public AccountController()
        {
            db = new AppDbContext();
            member = new CustomMembershipProvider();
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

        // GET: Account
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Login user, string returnUrl )
        {
            if (!ModelState.IsValid)
                return View(user);
            try
            {
                if (Membership.ValidateUser(user.Username, user.Password))
                {
                    FormsAuthentication.SetAuthCookie(user.Username, user.RememberMe);

                    if (this.Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") & !returnUrl.StartsWith("/\\"))
                        return this.Redirect(returnUrl);
                    else
                        return this.RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username and Passsword");
                }

            }catch(Exception e)
            {
                ModelState.AddModelError("", "An error occured. Please try again later");
            }
                
            return this.View(user);
        }

        // GET: Register
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Register()
        {
            var model = new User();
            List<TB_ROLES> userRoles = db.TB_ROLES.OrderBy(x => x.ID).ToList();

            model.Roles = new SelectList(userRoles, "ID", "ROLE_NAME").ToList();
            return View(model);
        }

        // POST: Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(User model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                if(member.CreateUser(model.UserId, model.Email, model.Password, model.RoleId, model.FirstName, model.LastName))
                {
                    TempData["RegisterMessage"] = "Account created. Please login";
                    return RedirectToAction("Login");
                }
            }

            List<TB_ROLES> userRoles = db.TB_ROLES.OrderBy(x => x.ID).ToList();
            model.Roles = new SelectList(userRoles, "ID", "ROLE_NAME").ToList();
            ModelState.AddModelError("", "Error. Please try again");
            return View(model);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            AuthenticationManager.SignOut();
            Session.RemoveAll();
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