using MicroServiceApp.HttpClientLayer.ServiceCar;
using MicroServiceApp.InfrastructureLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceActionCar :
        IAsyncServiceActionCar<ActionCar>
    {
        private readonly IAsyncHttpClientCar<Car> asyncHttpClientCar;
        private readonly IAsyncHttpClientActionCar<ActionCar> asyncHttpClientActionCar;

        public AsyncServiceActionCar(
            IAsyncHttpClientCar<Car> asyncHttpClientCar,
            IAsyncHttpClientActionCar<ActionCar> asyncHttpClientActionCar
            )
        {
            this.asyncHttpClientActionCar = asyncHttpClientActionCar;
            this.asyncHttpClientCar = asyncHttpClientCar;
        }
        public async Task<int> DeleteAll(string jwt = null)
        {
            await asyncHttpClientCar.SetJwt(jwt).UpdateRange((await asyncHttpClientCar.SetJwt(jwt).GetAll())
                .Where(i => i.ActionCarId != null)
                .Select(i => { i.ActionCarId = null; return i; })
                .ToList());
            List<ActionCar> actionCars = (await asyncHttpClientActionCar.SetJwt(jwt).GetAll()).ToList();
            return await asyncHttpClientActionCar.SetJwt(jwt).DeleteAll(actionCars);
        }
    }
}
