using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public Room Room { get; set; }
        public User Creator { get; set; }
        public List<User> Invited { get; set; }
        public List<User> Accepted { get; set; }
    }
}
