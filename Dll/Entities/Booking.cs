using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        [DisplayName("Start")]
        public DateTime FromDate { get; set; }
        [DisplayName("Slut")]
        public DateTime ToDate { get; set; }
        public Room Room { get; set; }
        public User Creator { get; set; }
        public List<User> Invited { get; set; }
        public List<User> Accepted { get; set; }
    }
}
