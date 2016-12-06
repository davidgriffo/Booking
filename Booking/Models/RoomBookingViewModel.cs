using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dll.Entities;

namespace Booking.Models {
    public class RoomBookingViewModel {
        public Room Room { get; set; }
        public List<Dll.Entities.Booking> Bookings { get; set; }
        public string Events { get; set; }
    }
}