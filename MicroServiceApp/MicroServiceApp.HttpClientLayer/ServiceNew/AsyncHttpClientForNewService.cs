using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public class AsyncHttpClientForNewService<T> : 
        AsyncHttpClientBasicForService<T>,
        IAsyncHttpClientNew<T>,
        IAsyncHttpClientUserForNew<T>,
        IAsyncHttlClientImg<T>
    {
        public async Task<T> GetByTitle(string title)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByTitle" + "?title=" + title);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetUserByEmail(string email)
        {
            var response = await httpClient
                .GetAsync(URI_USER_SERVICE + typeof(T).Name + "/GetByUserId" + "?email=" + email);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<int> AddRange(List<T> items)
        {
            var response = await httpClient.PostAsync(
                URI_REPOSITORY_SERVICE + typeof(T).Name + "/PostRange",
                new StringContent(JsonConvert.SerializeObject(items),
                Encoding.UTF8,
                "application/json"
                ));

            return (int)response.StatusCode;
        }

        public async Task<int> RemoveRange(List<T> items)
        {
            var response = await httpClient.PutAsync(
                URI_REPOSITORY_SERVICE + typeof(T).Name + "/DeleteRange",
                new StringContent(JsonConvert.SerializeObject(items),
                Encoding.UTF8,
                "application/json"
                ));

            return (int)response.StatusCode;
        }
    }
}
