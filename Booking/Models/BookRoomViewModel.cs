using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dll.Entities;

namespace Booking.Models {
    public class BookRoomViewModel {
        public Room Room { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}