using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using Dll.Entities;
using Newtonsoft.Json.Linq;

namespace Dll.Gateways {
    class AccountGateway : IAccountGateway {
        public bool Login(string username, string password) {
            using (var client = new HttpClient()) {
                SetupClient(client);

                //setup login data
                var formContent =
                    new FormUrlEncodedContent(new[] {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password)
                    });

                //Request token
                var response = client.PostAsync("/token", formContent).Result;

                if (response.IsSuccessStatusCode) {
                    var responseJson = response.Content.ReadAsStringAsync().Result;
                    var jObject = JObject.Parse(responseJson);
                    string token = jObject.GetValue("access_token").ToString();
                    HttpContext.Current.Session["token"] = token;
                }

                return response.IsSuccessStatusCode;
            }
        }

        public bool Register(User user, string password) {
            using (var client = new HttpClient()) {
                SetupClient(client);

                HttpResponseMessage response = client.PostAsJsonAsync($"api/account/register", user).Result;
                return response.IsSuccessStatusCode;
            }
        }

        public bool Logout() {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                HttpContext.Current.Session["token"] = null;

                return true;
            }
        }

        public User GetUserLoggedIn() {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                HttpResponseMessage response = client.GetAsync($"api/account/getuserloggedin").Result;
                return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<User>().Result : null;
            }
        }

        public bool ChangePassword(string oldPassword, string newPassword) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                HttpResponseMessage response = client.PostAsJsonAsync($"api/account/ChangePassword", new Tuple<string, string>(oldPassword, newPassword)).Result;
                return response.IsSuccessStatusCode;
            }
        }

        #region helpers

        private void SetupClient(HttpClient client) {
            string baseAddress = WebConfigurationManager.AppSettings["RestApiURL"];
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private void AddAuthorizationHeader(HttpClient client) {
            if (HttpContext.Current.Session["token"] != null) {
                string token = HttpContext.Current.Session["token"].ToString();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        #endregion

    }
}
