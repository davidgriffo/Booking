using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    public abstract class AbstractUserGateway : AbstractGateway<User, string> {
        protected abstract List<Booking> GetBookingsForRoom(HttpClient client);

        public List<Booking> GetBookingsForUser() {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                return GetBookingsForRoom(client);
            }
        }
    }
}
