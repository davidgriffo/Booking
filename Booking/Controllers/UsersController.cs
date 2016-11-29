using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers
{
    public class UsersController : Controller
    {
        private readonly IGateway<User, String> _userGateway = new DllFacade().GetUserGateway();
        // GET: Users
        public ActionResult Index()
        {
            var model = new UsersViewModel
            {
                Users = _userGateway.Read()
            };

            return View(model);
        }
    }
}