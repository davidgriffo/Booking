using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    public interface IAccountGateway {
        bool Login(string username, string password);
        bool Register(User user, string password);
        bool Logout();
        User GetUserLoggedIn();
    }
}
