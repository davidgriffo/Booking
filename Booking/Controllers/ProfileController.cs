using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Authorization;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers {
    [RequireUser]
    public class ProfileController : Controller {
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();

        // GET: Profile
        public ActionResult Index() {
            return View(_accountGateway.GetUserLoggedIn());
        }

        public ActionResult ChangePassword() {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model) {
            if (ModelState.IsValid) {
                var succes = _accountGateway.ChangePassword(model.OldPassword, model.NewPassword);

                if (succes) {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Error updating user");
            }

            return View(model);
        }

    }
}