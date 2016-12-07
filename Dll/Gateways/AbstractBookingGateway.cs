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

        protected abstract void RemoveInvite(HttpClient client, int bookingId);

        public void RemoveInvite(int bookingId) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                RemoveInvite(client, bookingId);
            }
        }
    }
}
