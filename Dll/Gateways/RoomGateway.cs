using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    class RoomGateway : AbstractRoomGateway {
        private const string ApiRef = "api/Room";

        protected override Room Create(HttpClient client, Room element) {
            HttpResponseMessage response = client.PostAsJsonAsync(ApiRef, element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Room>().Result : null;
        }

        protected override Room Read(HttpClient client, int id) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Room>().Result : null;
        }

        protected override List<Room> Read(HttpClient client) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<Room>>().Result : null;
        }

        protected override Room Update(HttpClient client, Room element) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/{element.Id}", element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Room>().Result : null;
        }

        protected override bool Delete(HttpClient client, int id) {
            HttpResponseMessage response = client.DeleteAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        protected override List<Booking> GetBookingsForRoom(HttpClient client, int roomId) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}/GetBookingsForRoom?id={roomId}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<Booking>>().Result : null;
        }
    }
}
