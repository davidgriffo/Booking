using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dll.Entities;

namespace Dll.Gateways {
    class UserGateway : AbstractGateway<User, string> {

        private const string ApiRef = "api/User";
        protected override User Create(HttpClient client, User element) {
            throw new NotImplementedException();
        }

        protected override User Read(HttpClient client, string id) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}/{id}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<User>().Result : null;
        }

        protected override List<User> Read(HttpClient client) {
            HttpResponseMessage response = client.GetAsync($"{ApiRef}").Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<User>>().Result : null;
        }

        protected override User Update(HttpClient client, User element) {
            HttpResponseMessage response = client.PutAsJsonAsync($"{ApiRef}/{element.Id}", element).Result;
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<User>().Result : null;
        }

        protected override bool Delete(HttpClient client, string id) {
            throw new NotImplementedException();
        }
    }
}
