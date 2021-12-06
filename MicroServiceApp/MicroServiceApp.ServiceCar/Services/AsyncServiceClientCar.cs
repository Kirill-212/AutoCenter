using AutoMapper;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.HttpClientLayer.ServiceCar;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceClientCar : IAsyncServiceClientCar<ClientCar>
    {
        private readonly IAsyncHttpClientClientCar<ClientCar> asyncHttpClientClientCar;
        private readonly IAsyncHttpClientUser<User> asyncHttpClientUser;
        private readonly IAsyncHttpClientCar<Car> asyncHttpClientCar;
        private readonly IAsyncHttpClientActionCar<ActionCar> asyncHttpClientActionCar;
        private readonly IMapper mapper;

        public AsyncServiceClientCar(
            IAsyncHttpClientActionCar<ActionCar> asyncHttpClientActionCar,
            IMapper mapper,
            IAsyncHttpClientClientCar<ClientCar> asyncHttpClientClientCar,
            IAsyncHttpClientUser<User> asyncHttpClientUser,
            IAsyncHttpClientCar<Car> asyncHttpClientCar
            )
        {
            this.asyncHttpClientActionCar = asyncHttpClientActionCar;
            this.asyncHttpClientUser = asyncHttpClientUser;
            this.asyncHttpClientClientCar = asyncHttpClientClientCar;
            this.mapper = mapper;
            this.asyncHttpClientCar = asyncHttpClientCar;
        }

        public async Task<int> Create(PostClientCarDto item)
        {
            Car car = mapper.Map<Car>(item.postCarDto);
            if (item.postCarDto.SharePercentage != null)
            {
                ActionCar actionCar = await asyncHttpClientActionCar
                    .GetBySharePercentage((int)item.postCarDto.SharePercentage);
                if (actionCar == null)
                {
                    actionCar = new();
                    actionCar.SharePercentage = (int)item.postCarDto.SharePercentage;
                    await asyncHttpClientActionCar.Add(actionCar);
                    actionCar = await asyncHttpClientActionCar
                        .GetBySharePercentage((int)item.postCarDto.SharePercentage);
                    car.ActionCarId = actionCar.Id;
                }
                else
                {
                    car.ActionCarId = actionCar.Id;
                }
            }
            await asyncHttpClientCar.Add(car);
            ClientCar clientCar = new()
            {
                CarId = (await asyncHttpClientCar.GetByVin(item.postCarDto.VIN)).Id,
                UserId = (await asyncHttpClientUser.GetByEmail(item.Email)).Id,
                RegisterNumber = item.RegisterNumber
            };

            return await asyncHttpClientClientCar.Add(clientCar);
        }

        public async Task<IEnumerable<ClientCar>> GetAll()
        {
            return await asyncHttpClientClientCar.GetAll();
        }

        public async Task<ClientCar> GetById(int id)
        {
            return await asyncHttpClientClientCar.GetById(id);
        }

        public async Task<int> Remove(string registerNumber)
        {
            if (string.IsNullOrEmpty(registerNumber)) return 404;
            ClientCar clientCar = await asyncHttpClientClientCar.GetByRegisterNumber(registerNumber);

            return clientCar == null ? 404 : await asyncHttpClientCar.Remove(clientCar.CarId);
        }

        public async Task<int> Update(PutClientCarDto item)
        {
            ClientCar clientCar = await asyncHttpClientClientCar.GetByRegisterNumber(item.OldRegisterNumber);
            //User user = await asyncHttpClientUser.GetByEmail(item.Email);
            if (item.NewOwnerEmail != null)
            {
                User user_new_owner = await asyncHttpClientUser.GetByEmail(item.NewOwnerEmail);
                clientCar.UserId = user_new_owner.Id;
            }
            clientCar.RegisterNumber = item.NewRegisterNumber ?? item.OldRegisterNumber;

            return await asyncHttpClientClientCar.Update(clientCar);
        }
    }
}
