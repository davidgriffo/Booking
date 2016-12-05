using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entities
{
    public class Equipment
    {
        public int Id { get; set; }
        [DisplayName("Navn")]
        public String Name { get; set; }
        [DisplayName("Beskrivelse")]
        public String Description { get; set; }

    }
}
