using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entities
{
    public class Room
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public string Department { get; set; }
        public String Description { get; set; }
        public int Capacity { get; set; }
        public List<Equipment> Equipment { get; set; }
    }
}
