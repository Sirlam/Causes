using Causes.UI.Web.Data;
using Causes.UI.Web.Models;
using Causes.UI.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Causes.UI.Web.Controllers
{
    public class AccountController : Controller
    {
        AppDbContext db;
        static PasswordManager pwdManager;

        public AccountController()
        {
            db = new AppDbContext();
            pwdManager = new PasswordManager();
        }

        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login user, string returnUrl = "")
        {
            if (ModelState.IsValid)
            {
                var localUser = db.TB_USERS.Where(x => x.EMAIL.ToLower() == user.Email.ToLower()).FirstOrDefault();

                if (localUser != null)
                {
                    if (!pwdManager.IsPasswordMatch(user.Password, localUser.PASSWORD_SALT, localUser.PASSWORD_HASH))
                        localUser = null;
                    else
                    {
                        FormsAuthentication.SetAuthCookie(user.Email, user.RememberMe);
                        if (Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }
                        
                }
            }
            ViewBag.Error = "Invalid Username and Passsword";
            ModelState.Remove("Password");
            return View(user);
        }

        // GET: Register
        public ActionResult Register()
        {
            var model = new User();
            List<TB_ROLES> userRoles = db.TB_ROLES.OrderBy(x => x.ID).ToList();

            model.Roles = new SelectList(userRoles, "ID", "ROLE_NAME").ToList();
            return View(model);
        }

        // POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                string salt = string.Empty;
                string passwordHash = pwdManager.GeneratePasswordHash(user.Password, out salt);
                TB_USERS tB_USERS = new TB_USERS
                {
                    EMAIL = user.Email,
                    FIRST_NAME = user.FirstName,
                    LAST_NAME = user.LastName,
                    ROLE_ID = user.RoleId,
                    PASSWORD_HASH = passwordHash,
                    PASSWORD_SALT = salt,
                    CREATED_DATE = DateTime.Now,
                };

                db.TB_USERS.Add(tB_USERS);
                db.SaveChanges();
                //db.Entry(user).GetDatabaseValues();

                ModelState.Clear();
                TempData["RegisterMessage"] = "Account created. Please Login";
                return RedirectToAction("Login");
            }
            List<TB_ROLES> userRoles = db.TB_ROLES.OrderBy(x => x.ID).ToList();
            user.Roles = new SelectList(userRoles, "ID", "ROLE_NAME").ToList();
            return View(user);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}