using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public class AsyncHttpClientForUserService<T> :
        AsyncHttpClientBasicForService<T>,
        IAsyncHttpClientUser<T>,
        IAsyncHttpClientEmployee<T>,
        IAsyncHttpClientRole<T>
    {
        public async Task<IEnumerable<T>> GetAllUsersNotAddedToEmp()
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetAllUsersNotAddedToEmp");

            return await response.Content.ReadAsAsync<IEnumerable<T>>();
        }

        public async Task<T> GetByEmail(string email)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByEmail" + "?email=" + email);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetByEmailValidAttr(string email)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByEmailValidAttr" + "?email=" + email);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetByUserEmail(string email)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByUserEmail" + "?email=" + email);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetByUserEmailValidAttr(string email)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByUserEmailValidAttr" + "?email=" + email);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetByUserId(int id)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByUserId" + "?userId=" + id);

            return await response.Content.ReadAsAsync<T>();
        }

        IAsyncHttpClientEmployee<T> IAsyncHttpClientEmployee<T>.SetJwt(string jwt)
        {
            if (jwt != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", jwt);
            }

            return this;
        }

        IAsyncHttpClientUser<T> IAsyncHttpClientUser<T>.SetJwt(string jwt)
        {
            if (jwt != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", jwt);
            }

            return this;
        }

        IAsyncHttpClientRole<T> IAsyncHttpClientRole<T>.SetJwt(string jwt)
        {
            if (jwt != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", jwt);
            }

            return this;
        }
    }
}
