using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers {
    public class UsersController : Controller {
        private readonly IGateway<User, String> _userGateway = new DllFacade().GetUserGateway();
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();

        // GET: Users
        public ActionResult Index() {
            var model = new UsersViewModel {Users = _userGateway.Read()};

            return View(model);
        }

        public ActionResult Edit(string id) {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = _userGateway.Read(id);


            if (user == null) {
                return HttpNotFound();
            }

            return View(user);
        }

        public ActionResult Details() {
            return View();
        }

        public ActionResult Delete() {
            return View();
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
                User user = new User {
                    Email = model.Email,
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.Phone
                };


                if (model.UserStatus.Equals("1")) {
                    user.IsAdmin = true;
                } else if (model.UserStatus.Equals("2")) {
                    user.IsSuperAdmin = true;
                }


                bool response = _accountGateway.Register(user, model.Password);

                if (response) {
                    _accountGateway.Login(model.Email, model.Password);
                    return RedirectToAction("Index");
                } else
                    ModelState.AddModelError("", "Error registering new user");
            }

            return View(model);
        }

        // POST: /Account/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FirstName, LastName, Email, PhoneNumber")]User user, string userStatus) {
            if (ModelState.IsValid) {
                if (userStatus.Equals("1")) {
                    user.IsAdmin = true;
                    user.IsSuperAdmin = false;
                } else if (userStatus.Equals("2")) {
                    user.IsSuperAdmin = true;
                    user.IsAdmin = false;
                } else {
                    user.IsAdmin = false;
                    user.IsSuperAdmin = false;
                }


                var updatedUser = _userGateway.Update(user);

                if (updatedUser != null) {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Error updating new user");
            }

            return View(user);
        }
    }
}