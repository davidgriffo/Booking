using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Models {
    public class ProfileBookingsViewModel {
        public Dll.Entities.Booking Booking { get; set; }
        public List<Dll.Entities.Booking> Bookings { get; set; }
        public List<Dll.Entities.Booking> Invites { get; set; }
    }
}