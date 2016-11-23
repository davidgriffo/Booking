using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers {
    public class AccountController : Controller {
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();

        // GET: /Account/Login
        public ActionResult Login() {
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model) {
            if (ModelState.IsValid) {
                bool response = _accountGateway.Login(model.Email, model.Password);
                if (response) {
                    return RedirectToAction("Index", "Home");
                } else
                    ModelState.AddModelError("", "Invalid login attempt!");
            }

            return View(model);
        }

        //
        // GET: /Account/Register
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model) {
            if (ModelState.IsValid) {
                User user = new User {Email = model.Email, UserName = model.Email, FirstName = model.FirstName, LastName = model.LastName};
                bool response = _accountGateway.Register(user, model.Password);

                if (response) {
                    _accountGateway.Login(model.Email, model.Password);
                    return RedirectToAction("Index", "Home");
                } else
                    ModelState.AddModelError("", "Error registering new user");
            }

            return View(model);
        }

        public ActionResult Logout() {
            if (ModelState.IsValid) {
                bool response = _accountGateway.Logout();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}