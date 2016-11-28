using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();

        // GET: Profile
        public ActionResult Index()
        {
            
            return View(_accountGateway.GetUserLoggedIn());
        }
    }
}