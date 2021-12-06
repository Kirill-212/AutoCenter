using MicroServiceApp.HttpClientLayer.ServiceCar;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MicroServiceApp.HttpClientLayer
{
    public class AsyncHttpClientForCarService<T> :
        AsyncHttpClientBasicForService<T>,
        IAsyncHttpClientCar<T>,
        IAsyncHttpClientCarEquipment<T>,
        IAsyncHttpClientActionCar<T>,
        IAsyncHttpClientOrder<T>,
        IAsyncHttpClientClientCar<T>
    {
        public async Task<T> GetByVin(string vin)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByVin?vin=" + vin);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetByName(string name)
        {
            var response = await httpClient
                .GetAsync(URI_CAR_SERVICE + typeof(T).Name + "/GetByName?name=" + name);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<T> GetBySharePercentage(int sharePercentage)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetBySharePercentage?sharePercentage=" + sharePercentage);

            return await response.Content.ReadAsAsync<T>();
        }

        public async Task<int> UpdateRange(List<T> items)
        {
            var response = await httpClient.PutAsync(
                 URI_REPOSITORY_SERVICE + typeof(T).Name + "/UpdateRange",
                 new StringContent(JsonConvert.SerializeObject(items),
                 Encoding.UTF8,
                 "application/json"
                 ));

            return (int)response.StatusCode;
        }

        public async Task<int> DeleteAll(List<T> items)
        {
            var response = await httpClient.PutAsync(
                 URI_REPOSITORY_SERVICE + typeof(T).Name + "/DeleteRange",
                 new StringContent(JsonConvert.SerializeObject(items),
                 Encoding.UTF8,
                 "application/json"
                 ));

            return (int)response.StatusCode;
        }

        public async Task<T> GetByRegisterNumber(string registerNumber)
        {
            var response = await httpClient
                .GetAsync(URI_REPOSITORY_SERVICE + typeof(T).Name + "/GetByRegisterNumber?registerNumber=" + registerNumber);

            return await response.Content.ReadAsAsync<T>();
        }
    }
}
