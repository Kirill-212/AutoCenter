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
        public async Task<int> Create(PostCarDto item)
        {
            Car car = mapper.Map<Car>(item);
            if (item.SharePercentage != null)
            {
                ActionCar actionCar = await asyncHttpClientActionCar
                    .GetBySharePercentage((int)item.SharePercentage);
                if (actionCar == null)
                {
                    actionCar = new() { SharePercentage = (int)item.SharePercentage };
                    await asyncHttpClientActionCar.Add(actionCar);
                    actionCar = await asyncHttpClientActionCar
                        .GetBySharePercentage((int)item.SharePercentage);
                    car.ActionCarId = actionCar.Id;
                }
                else
                {
                    car.ActionCarId = actionCar.Id;
                }
            }

            return await asyncHttpClientCar.Add(car);
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await asyncHttpClientCar.GetAll();
        }

        public async Task<Car> GetById(int id)
        {
            return await asyncHttpClientCar.GetById(id);
        }

        public async Task<Car> GetByVin(string vin)
        {
            return await asyncHttpClientCar.GetByVin(vin);
        }

        public async Task<Car> GetByVinNotAddedEmpValidAttr(string vin)
        {
            return await asyncHttpClientCar.GetByVinNotAddedEmpValidAttr(vin);
        }

        public async Task<IEnumerable<Car>> GetCarByEmail(string email)
        {
            return await asyncHttpClientCar.GetCarByEmail(email);
        }

        public async Task<IEnumerable<Car>> GetCarForUser()
        {
            return await asyncHttpClientCar.GetCarForUser();
        }

        public async Task<IEnumerable<Car>> GetWithoutClientCar()
        {
            return await asyncHttpClientCar.GetWithoutClientCar();
        }

        public async Task<int> Remove(string vin)
        {
            Car car = await asyncHttpClientCar.GetByVin(vin);

            return car == null ? 404 : await asyncHttpClientCar.Remove(car.Id);
        }

        public async Task<int> Update(PutCarDto item)
        {
            Car car = await asyncHttpClientCar.GetByVin(item.VIN);
            if (item.SharePercentage == null || item.SharePercentage == 0)
            {
                car.ActionCarId = null;
            }
            else if (car.ActionCar == null && item.SharePercentage > 0)
            {
                await asyncHttpClientActionCar
                       .Add(new() { SharePercentage = (int)item.SharePercentage });
                ActionCar actionCar = await asyncHttpClientActionCar
                    .GetBySharePercentage((int)item.SharePercentage);
                car.ActionCarId = actionCar.Id;
            }
            else if (car.ActionCar.SharePercentage != item.SharePercentage)
            {
                ActionCar actionCar = await asyncHttpClientActionCar
                    .GetBySharePercentage((int)item.SharePercentage);
                if (actionCar == null)
                {
                    await asyncHttpClientActionCar
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

            return await asyncHttpClientCar.Update(putCar);
        }
    }
}
