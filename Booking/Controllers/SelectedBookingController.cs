using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dll;
using Dll.Entities;
using Dll.Gateways;

namespace Booking.Controllers {
    public class SelectedBookingController : Controller {
        private IGateway<Room, int> _rg = new DllFacade().GetRoomGateway();
        private IGateway<Dll.Entities.Booking, int> _bg = new DllFacade().GetBookingGateway();
        private IAccountGateway _ag = new DllFacade().GetAccountGateway();

        // GET: SelectedBooking
        public ActionResult Index(int id) {
            return View(_rg.Read(id));
        }

        public ActionResult BookRoom(string startDate, string endDate, int roomId) {
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