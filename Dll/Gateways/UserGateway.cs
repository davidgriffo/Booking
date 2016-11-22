using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    class UserGateway : AbstractGateway<User, string> {
        protected override User Create(HttpClient client, User element) {
            throw new NotImplementedException();
        }

        protected override User Read(HttpClient client, string id) {
            throw new NotImplementedException();
        }

        protected override List<User> Read(HttpClient client) {
            throw new NotImplementedException();
        }

        protected override User Update(HttpClient client, User element) {
            throw new NotImplementedException();
        }

        protected override bool Delete(HttpClient client, string id) {
            throw new NotImplementedException();
        }
    }
}
