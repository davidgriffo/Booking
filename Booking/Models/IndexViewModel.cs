using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dll.Entities;

namespace Booking.Models {
    public class IndexViewModel {
        public List<Room> Rooms { get; set; }
        public List<Equipment> Equipment { get; set; }
    }
}