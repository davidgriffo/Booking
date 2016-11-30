using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Dll.Entities
{
    public class User : IdentityUser
    {
        [DisplayName("Fornavn")]
        public String FirstName { get; set; }
        [DisplayName("Efternavn")]
        public String LastName { get; set; }
        [DisplayName("Administrator")]
        public bool IsAdmin { get; set; }
        [DisplayName("Super administrator")]
        public bool IsSuperAdmin { get; set; }
        [DisplayName("Telefon")]
        public override string PhoneNumber { get; set; }
    }
}
