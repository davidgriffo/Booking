using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    public abstract class AbstractBookingGateway : AbstractGateway<Booking, int> {

        protected abstract void InviteUsers(HttpClient client, Booking booking, List<User> users);

        public void InviteUsers(Booking booking, List<User> users) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                InviteUsers(client, booking, users);
            }
        }
    }
}
