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
    public class SelectedBookingController : Controller
    {
        private IGateway<Room, int> _rg = new DllFacade().GetRoomGateway();
        // GET: SelectedBooking
        public ActionResult Index(int id)
        {
            ;
            return View(_rg.Read(id));
        }

        public ActionResult BookRoom()
        {


            return View();
        }
    }
}