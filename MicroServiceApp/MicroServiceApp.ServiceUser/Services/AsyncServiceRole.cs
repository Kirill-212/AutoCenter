using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public class AsyncServiceRole : IAsyncServiceRole<Role>
    {
        private readonly IAsyncHttpClientRole<Role> httpClient;

        public AsyncServiceRole(
            IAsyncHttpClientRole<Role> httpClient
            )
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await httpClient.GetAll();
        }
    }
}
