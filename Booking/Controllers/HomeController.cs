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
using Microsoft.Ajax.Utilities;
using Search;

namespace Booking.Controllers {
    [RequireUser]
    public class HomeController : Controller {
        private IGateway<Room, int> _rg = new DllFacade().GetRoomGateway();
        private IGateway<Equipment, int> _eg = new DllFacade().GetEquipmentGateway();
        private IGateway<Dll.Entities.Booking, int> _bg = new DllFacade().GetBookingGateway();

        public ActionResult Index() {
            return View(_rg.Read());
        }

        public ActionResult Search(string startDate, string endDate, int? capacity) {
            var searcher = new SearchRooms();

            var allRooms = _rg.Read();
            if (capacity != null && capacity > 0) {
                allRooms = searcher.CheckCapacity(allRooms, capacity.Value);
            }
            if (!startDate.IsNullOrWhiteSpace() || !endDate.IsNullOrWhiteSpace()) {
                var startDateTime = Convert.ToDateTime(startDate);
                var endDateTime = Convert.ToDateTime(endDate);

                allRooms = searcher.CheckAvailibility(allRooms, _bg.Read(), startDateTime, endDateTime);
            }

            return View("Index", allRooms);
        }

    }
}