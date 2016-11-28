using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entities
{
    public class Room
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public String Name { get; set; }
        [DisplayName("Afdeling")]
        public string Department { get; set; }
        [DisplayName("Beskrivelse")]
        public String Description { get; set; }
        [DisplayName("Antal personer")]
        public int Capacity { get; set; }
        [DisplayName("Udstyr")]
        public List<Equipment> Equipment { get; set; }
    }
}
