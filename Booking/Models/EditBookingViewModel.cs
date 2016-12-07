using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Booking.Models {
    public class EditBookingViewModel {
        public Dll.Entities.Booking Booking { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }

}