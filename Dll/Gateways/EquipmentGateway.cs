using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    class EquipmentGateway : AbstractGateway<Equipment, int> {
        private const string ApiRef = "api/Equipment";

        protected override Equipment Create(HttpClient client, Equipment element) {
            HttpResponseMessage response = client.PostAsJsonAsync(ApiRef, element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Equipment>().Result : null;
        }

        protected override Equipment Read(HttpClient client, int id) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Equipment>().Result : null;
        }

        protected override List<Equipment> Read(HttpClient client) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<Equipment>>().Result : null;
        }

        protected override Equipment Update(HttpClient client, Equipment element) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/{element.Id}", element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Equipment>().Result : null;
        }

        protected override bool Delete(HttpClient client, int id) {
            HttpResponseMessage response = client.DeleteAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
