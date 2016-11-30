using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Booking.Controllers;
using Dll.Entities;

namespace Booking.Models {
    public class BookingViewModel {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Room Room { get; set; }
    }
}