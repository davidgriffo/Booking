using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Authorization;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers
{
    [RequireUser]
    public class ProfileController : Controller
    {
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();
        private readonly IGateway<User, string> _userGateway = new DllFacade().GetUserGateway();

        // GET: Profile
        public ActionResult Index()
        {     
            return View(_accountGateway.GetUserLoggedIn());
        }

        [HttpGet]
        public ActionResult Edit()
        {
            return View(_accountGateway.GetUserLoggedIn());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, FirstName, LastName, Email, PhoneNumber")]User user) {
            if (ModelState.IsValid)
            {
                var updatedUser = _userGateway.Update(user);

                if (updatedUser != null)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(user);
        }
    }
}