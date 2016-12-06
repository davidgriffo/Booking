using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Booking.Models;
using Dll;
using Dll.Entities;
using Dll.Gateways;
using Microsoft.Ajax.Utilities;

namespace Booking.Controllers {
    public class SelectedBookingController : Controller {
        private readonly IGateway<Room, int> _roomGateway = new DllFacade().GetRoomGateway();
        private readonly IGateway<Dll.Entities.Booking, int> _bookingGateway = new DllFacade().GetBookingGateway();
        private readonly IAccountGateway _accountGateway = new DllFacade().GetAccountGateway();

        [HttpGet]
        // GET: SelectedBooking/BookRoom
        public ActionResult BookRoom(string startDate, string endDate, int? id) {
            if (id != null & id > 0) {
                var model = new BookRoomViewModel {Room = _roomGateway.Read(id.Value)};
                if (!startDate.IsNullOrWhiteSpace()) {
                    model.StartDate = startDate;
                }
                if (!endDate.IsNullOrWhiteSpace()) {
                    model.EndDate = endDate;
                }

                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        // POST: SelectedBooking/BookRoom
        public ActionResult BookRoom(string startDate, string endDate, int roomId) {
            DateTimeFormatInfo dk = new CultureInfo("da-DK", false).DateTimeFormat;
            var startDateConverted = Convert.ToDateTime(startDate, dk);
            var endDateConverted = Convert.ToDateTime(endDate, dk);

            //var user = _accountGateway.GetUserLoggedIn();
            var booking = new Dll.Entities.Booking() {
                //Creator = user,
                Room = new Room {Id = roomId},
                FromDate = startDateConverted,
                ToDate = endDateConverted,
                //Accepted = new List<User>(),
                //Invited = new List<User>()
            };

            var bookingCreated = _bookingGateway.Create(booking);
            if (bookingCreated != null) {
                return RedirectToAction("Bookings", "Profile");
            }

            return View(new BookRoomViewModel {Room = _roomGateway.Read(roomId), StartDate = startDate, EndDate = endDate, ErrorMessage = "Rummet er allerede booket i det valgte tidsrum"});
        }
    }
}