using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dll.Entities;

namespace Booking.Models
{
    public class UsersViewModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; }
    }
}