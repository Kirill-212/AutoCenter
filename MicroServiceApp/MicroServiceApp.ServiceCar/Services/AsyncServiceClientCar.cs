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

        public async Task<int> Create(PostClientCarDto item, string jwt = null)
        {
            Car car = mapper.Map<Car>(item.postCarDto); 
            car.IsActive = true;
            if (item.postCarDto.SharePercentage != null)
            {
                ActionCar actionCar = await asyncHttpClientActionCar.SetJwt(jwt)
                    .GetBySharePercentage((int)item.postCarDto.SharePercentage);
                if (actionCar == null)
                {
                    actionCar = new();
                    actionCar.SharePercentage = (int)item.postCarDto.SharePercentage;
                    await asyncHttpClientActionCar.SetJwt(jwt).Add(actionCar);
                    actionCar = await asyncHttpClientActionCar.SetJwt(jwt)
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
                CarId = (await asyncHttpClientCar.SetJwt(jwt).GetByVin(item.postCarDto.VIN)).Id,
                UserId = (await asyncHttpClientUser.SetJwt(jwt).GetByEmail(item.Email)).Id,
                RegisterNumber = item.RegisterNumber
            };

            return await asyncHttpClientClientCar.SetJwt(jwt).Add(clientCar);
        }

        public async Task<IEnumerable<ClientCar>> GetAll( string jwt = null)
        {
            return await asyncHttpClientClientCar.SetJwt(jwt).GetAll();
        }

        public async Task<ClientCar> GetById(int id, string jwt = null)
        {
            return await asyncHttpClientClientCar.SetJwt(jwt).GetById(id);
        }

        public async Task<int> Remove(string registerNumber, string jwt = null)
        {
            if (string.IsNullOrEmpty(registerNumber)) return 404;
            ClientCar clientCar = await asyncHttpClientClientCar.SetJwt(jwt).GetByRegisterNumber(registerNumber);

            return clientCar == null ? 404 : await asyncHttpClientCar.SetJwt(jwt).Remove(clientCar.CarId);
        }

        public async Task<int> Update(PutClientCarDto item, string jwt = null)
        {
            ClientCar clientCar = await asyncHttpClientClientCar.SetJwt(jwt).GetByRegisterNumber(item.OldRegisterNumber);
            //User user = await asyncHttpClientUser.GetByEmail(item.Email);
            if (item.NewOwnerEmail != null)
            {
                User user_new_owner = await asyncHttpClientUser.SetJwt(jwt).GetByEmail(item.NewOwnerEmail);
                clientCar.UserId = user_new_owner.Id;
            }
            clientCar.RegisterNumber = item.NewRegisterNumber ?? item.OldRegisterNumber;

            return await asyncHttpClientClientCar.SetJwt(jwt).Update(clientCar);
        }
    }
}
