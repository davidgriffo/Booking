using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    class BookingGateway : AbstractBookingGateway {
        private const string ApiRef = "api/Booking";

        protected override Booking Create(HttpClient client, Booking element) {
            HttpResponseMessage response = client.PostAsJsonAsync(ApiRef, element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Booking>().Result : null;
        }

        protected override Booking Read(HttpClient client, int id) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Booking>().Result : null;
        }

        protected override List<Booking> Read(HttpClient client) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<Booking>>().Result : null;
        }

        protected override Booking Update(HttpClient client, Booking element) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/{element.Id}", element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Booking>().Result : null;
        }

        protected override bool Delete(HttpClient client, int id) {
            HttpResponseMessage response = client.DeleteAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode;
        }

        protected override void InviteUsers(HttpClient client, Booking booking, List<User> users) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/InviteUsers?id={booking.Id}", users).Result;
        }

        protected override void RemoveInvite(HttpClient client, int bookingId) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/RemoveInvite?id={bookingId}", new {}).Result;
        }
    }
}
