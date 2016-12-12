using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dll.Entities {
    public class Department {
        public int Id { get; set; }
        [Display(Name = "Afdelings navn")]
        public string Name { get; set; }
    }
}
