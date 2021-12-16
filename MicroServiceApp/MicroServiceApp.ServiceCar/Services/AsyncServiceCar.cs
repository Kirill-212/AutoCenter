using AutoMapper;
using MicroServiceApp.HttpClientLayer.ServiceCar;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceCar : IAsyncServiceCar<Car>
    {
        private readonly IAsyncHttpClientCar<Car> asyncHttpClientCar;
        private readonly IAsyncHttpClientActionCar<ActionCar> asyncHttpClientActionCar;
        private readonly IMapper mapper;

        public AsyncServiceCar(
            IMapper mapper,
            IAsyncHttpClientCar<Car> asyncHttpClientCar,
            IAsyncHttpClientActionCar<ActionCar> asyncHttpClientActionCar
            )
        {
            this.mapper = mapper;
            this.asyncHttpClientActionCar = asyncHttpClientActionCar;
            this.asyncHttpClientCar = asyncHttpClientCar;
        }
        public async Task<int> Create(PostCarDto item, string jwt = null)
        {
            Car car = mapper.Map<Car>(item);
            if (item.SharePercentage != null)
            {
                ActionCar actionCar = await asyncHttpClientActionCar.SetJwt(jwt)
                    .GetBySharePercentage((int)item.SharePercentage);
                if (actionCar == null)
                {
                    actionCar = new() { SharePercentage = (int)item.SharePercentage };
                    await asyncHttpClientActionCar.SetJwt(jwt).Add(actionCar);
                    actionCar = await asyncHttpClientActionCar.SetJwt(jwt)
                        .GetBySharePercentage((int)item.SharePercentage);
                    car.ActionCarId = actionCar.Id;
                }
                else
                {
                    car.ActionCarId = actionCar.Id;
                }
            }
            car.IsActive = false;
            return await asyncHttpClientCar.SetJwt(jwt).Add(car);
        }

        public async Task<IEnumerable<Car>> GetAll( string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetAll();
        }

        public async Task<Car> GetById(int id, string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetById(id);
        }

        public async Task<Car> GetByVin(string vin, string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetByVin(vin);
        }

        public async Task<Car> GetByVinNotAddedEmpValidAttr(string vin, string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetByVinNotAddedEmpValidAttr(vin);
        }

        public async Task<IEnumerable<Car>> GetCarByEmail(string email, string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetCarByEmail(email);
        }

        public async Task<IEnumerable<Car>> GetCarForUser( string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetCarForUser();
        }

        public async Task<IEnumerable<Car>> GetWithoutClientCar( string jwt = null)
        {
            return await asyncHttpClientCar.SetJwt(jwt).GetWithoutClientCar();
        }

        public async Task<int> Remove(string vin, string jwt = null)
        {
            Car car = await asyncHttpClientCar.SetJwt(jwt).GetByVin(vin);

            return car == null ? 404 : await asyncHttpClientCar.SetJwt(jwt).Remove(car.Id);
        }

        public async Task<int> Update(PutCarDto item, string jwt = null)
        {
            Car car = await asyncHttpClientCar.SetJwt(jwt).GetByVin(item.VIN);
            if (item.SharePercentage == null || item.SharePercentage == 0)
            {
                car.ActionCarId = null;
            }
            else if (car.ActionCar == null && item.SharePercentage > 0)
            {
                await asyncHttpClientActionCar.SetJwt(jwt)
                       .Add(new() { SharePercentage = (int)item.SharePercentage });
                ActionCar actionCar = await asyncHttpClientActionCar.SetJwt(jwt)
                    .GetBySharePercentage((int)item.SharePercentage);
                car.ActionCarId = actionCar.Id;
            }
            else if (car.ActionCar.SharePercentage != item.SharePercentage)
            {
                ActionCar actionCar = await asyncHttpClientActionCar.SetJwt(jwt)
                    .GetBySharePercentage((int)item.SharePercentage);
                if (actionCar == null)
                {
                    await asyncHttpClientActionCar.SetJwt(jwt)
                        .Add(new() { SharePercentage = (int)item.SharePercentage });
                    car.ActionCarId = actionCar.Id;
                }
                else
                {
                    car.ActionCarId = actionCar.Id;
                }
            }
            Car putCar = mapper.Map<Car>(item);
            putCar.Id = car.Id;
            putCar.ActionCarId = car.ActionCarId;

            return await asyncHttpClientCar.SetJwt(jwt).Update(putCar);
        }

        public async Task<int> UpdateStatus(string vin, string jwt = null)
        {
            Car car=await asyncHttpClientCar.SetJwt(jwt).GetByVin(vin);
            if (car == null) return 404;
            car.IsActive = !car.IsActive;
            return await asyncHttpClientCar.SetJwt(jwt).Update(car);
        }
    }
}
