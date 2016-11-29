using System;
using System.Collections.Generic;
using System.Globalization;
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
            var viewModel = new IndexViewModel {Rooms = _rg.Read(), Equipment = _eg.Read()};
            return View(viewModel);
        }

        public ActionResult Search(string startDate, string endDate, int? capacity, List<int> selectedEquipment) {
            var searcher = new SearchRooms();

            var allRooms = _rg.Read();
            if (capacity != null && capacity > 0) {
                allRooms = searcher.CheckCapacity(allRooms, capacity.Value);
            }
            if (!startDate.IsNullOrWhiteSpace() && !endDate.IsNullOrWhiteSpace()) {
                DateTimeFormatInfo dk = new CultureInfo("da-DK", false).DateTimeFormat;
                var startDateTime = Convert.ToDateTime(startDate, dk);
                var endDateTime = Convert.ToDateTime(endDate, dk);

                allRooms = searcher.CheckAvailibility(allRooms, _bg.Read(), startDateTime, endDateTime);
            }
            if (selectedEquipment != null && selectedEquipment.Count > 0) {
                var equipment = new List<Equipment>();
                selectedEquipment.ForEach(x => equipment.Add(new Equipment { Id = x }));

                allRooms = searcher.CheckEquipment(allRooms, equipment);
            }



            var viewModel = new IndexViewModel { Rooms = allRooms, Equipment = _eg.Read() };
            return View("Index", viewModel);
        }

    }
}