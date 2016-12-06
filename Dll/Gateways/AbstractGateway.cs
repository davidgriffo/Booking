using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Dll.Gateways {
    public abstract class AbstractGateway<T, Y> : IGateway<T, Y> {

        protected abstract T Create(HttpClient client, T element);

        public T Create(T element) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                var added = Create(client, element);
                return added;
            }
        }

        protected abstract T Read(HttpClient client, Y id);

        public T Read(Y id) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                var read = Read(client, id);
                return read;
            }
        }

        protected abstract List<T> Read(HttpClient client);

        public List<T> Read() {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                var read = Read(client);
                return read;
            }
        }

        protected abstract T Update(HttpClient client, T element);

        public T Update(T element) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                var updated = Update(client, element);
                return updated;
            }
        }

        protected abstract bool Delete(HttpClient client, Y id);

        public bool Delete(Y id) {
            using (var client = new HttpClient()) {
                SetupClient(client);
                AddAuthorizationHeader(client);

                var deleted = Delete(client, id);
                return deleted;
            }
        }

        #region helpers

        internal void SetupClient(HttpClient client) {
            string baseAddress = WebConfigurationManager.AppSettings["RestApiURL"];
            client.BaseAddress = new Uri(baseAddress);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal void AddAuthorizationHeader(HttpClient client) {
            if (HttpContext.Current.Session["token"] != null) {
                string token = HttpContext.Current.Session["token"].ToString();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            }
        }

        #endregion
    }
}