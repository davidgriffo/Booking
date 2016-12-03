using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    class DepartmentGateway : AbstractGateway<Department, int> {
        private const string ApiRef = "api/Department";

        protected override Department Create(HttpClient client, Department element) {
            HttpResponseMessage response = client.PostAsJsonAsync(ApiRef, element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Department>().Result : null;
        }

        protected override Department Read(HttpClient client, int id) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Department>().Result : null;
        }

        protected override List<Department> Read(HttpClient client) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<Department>>().Result : null;
        }

        protected override Department Update(HttpClient client, Department element) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/{element.Id}", element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Department>().Result : null;
        }

        protected override bool Delete(HttpClient client, int id) {
            HttpResponseMessage response = client.DeleteAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode;
        }
    }
}
