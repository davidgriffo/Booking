using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dll.Entities;

namespace Booking.Models
{
    public class UsersBookingViewModel
    {
        public Dll.Entities.Booking Booking { get; set; }
        public List<User> Users { get; set; }
    }
}