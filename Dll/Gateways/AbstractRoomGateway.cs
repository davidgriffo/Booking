using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    public abstract class AbstractRoomGateway : AbstractGateway<Room, int> {
        protected abstract List<Booking> GetBookingsForRoom(HttpClient client, int roomId);

        public List<Booking> GetBookingsForRoom(int roomId) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                return GetBookingsForRoom(client, roomId);
            }
        }
    }
}
