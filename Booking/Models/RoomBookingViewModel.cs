using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Dll.Entities;

namespace Booking.Models {
    public class RoomBookingViewModel {
        public Room Room { get; set; }
        public List<Dll.Entities.Booking> Bookings { get; set; }
    }
}