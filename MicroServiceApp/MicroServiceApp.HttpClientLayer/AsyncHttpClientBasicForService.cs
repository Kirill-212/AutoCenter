using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MicroServiceApp.HttpClientLayer
{
    public class AsyncHttpClientBasicForService<T> : IAsynHttpClient<T>
    {
        private protected HttpClient httpClient;
        private protected readonly string URI_REPOSITORY_SERVICE =
            @"http://localhost:37766/ServiceRepository/api/";
        private protected readonly string URI_CAR_SERVICE =
            @"http://localhost:37766/ServiceCar/api/";
        private protected readonly string URI_USER_SERVICE =
            @"http://localhost:37766/ServiceUser/api/";

        public AsyncHttpClientBasicForService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public AsyncHttpClientBasicForService()
        {
            httpClient = new HttpClient();
        }

        public async Task<int> Add(T entity)
        {
            var response = await httpClient.PostAsync(
                URI_REPOSITORY_SERVICE + typeof(T).Name,
                new StringContent(JsonConvert.SerializeObject(entity),
                Encoding.UTF8,
                "application/json"
                ));

            return (int)response.StatusCode;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            var response = await httpClient.GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name);

            return await response.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> GetById(int id)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + '/' + id);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<int> Remove(int id)
        {
            var response = await httpClient
                .DeleteAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + '/' + id);

            return (int)response.StatusCode;
        }

        public async Task<int> Update(T entity)
        {
            var response = await httpClient.PutAsync(
                URI_REPOSITORY_SERVICE + typeof(T).Name,
                new StringContent(JsonConvert.SerializeObject(entity),
                Encoding.UTF8,
                "application/json"
                ));

            return (int)response.StatusCode;
        }
    }
}
