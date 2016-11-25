using System;
using System.Collections.Generic;
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
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsSuperAdmin { get; set; }

    }
}
