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
        private IGateway<Room, int> _rg = new DllFacade().GetRoomGateway();
        private IGateway<Dll.Entities.Booking, int> _bg = new DllFacade().GetBookingGateway();
        private IAccountGateway _ag = new DllFacade().GetAccountGateway();

        [HttpGet]
        // GET: SelectedBooking/BookRoom
        public ActionResult BookRoom(string startDate, string endDate, int? id) {
            if (id != null & id > 0) {

                var model = new BookRoomViewModel {Room = _rg.Read(id.Value)};
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
        public ActionResult BookRoomConfirm(string startDate, string endDate, int roomId) {
            DateTimeFormatInfo dk = new CultureInfo("da-DK", false).DateTimeFormat;
            var startDateConverted = Convert.ToDateTime(startDate, dk);
            var endDateConverted = Convert.ToDateTime(endDate, dk);

            var user = _ag.GetUserLoggedIn();
            var booking = new Dll.Entities.Booking() {
                Creator = user,
                Room = _rg.Read(roomId),
                FromDate = startDateConverted,
                ToDate = endDateConverted,
                Accepted = new List<User>(),
                Invited = new List<User>()
            };

            bool isAvailible = true;
            Dll.Entities.Booking createdBooking;

            foreach (var bookingToCheck in _bg.Read()) {
                if (bookingToCheck.Room.Id != roomId) {
                    continue;
                }

                //CHECK AVAILIBILITY HERE
                if (bookingToCheck.ToDate <= endDateConverted && bookingToCheck.ToDate >= startDateConverted) {
                    isAvailible = false;
                    break;
                } else if (bookingToCheck.FromDate <= endDateConverted && bookingToCheck.FromDate >= startDateConverted) {
                    isAvailible = false;
                    break;
                } else if (bookingToCheck.FromDate <= startDateConverted && bookingToCheck.ToDate >= endDateConverted) {
                    isAvailible = false;
                    break;
                }
            }
            if (isAvailible) {
                createdBooking = _bg.Create(booking);
            } else {
                createdBooking = null;
            }


            return View(createdBooking);
        }
    }
}