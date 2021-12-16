using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.HttpClientLayer.ServiceCar;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.ServiceCar.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceOrder : IAsyncServiceOrder<Order>
    {

        private readonly IAsyncHttpClientOrder<Order> asyncHttpClientOrder;
        private readonly IAsyncHttpClientClientCar<ClientCar> asyncHttpClientClientCar;
        private readonly IAsyncHttpClientUser<User> asyncHttpClientUser;
        private readonly IAsyncHttpClientCar<Car> asyncHttpClientCar;
        private readonly IAsyncRepositoryCarEquipment<CarEquipment> asyncRepositoryCarEquipment;

        public AsyncServiceOrder(IAsyncHttpClientOrder<Order> asyncHttpClientOrder,
            IAsyncHttpClientClientCar<ClientCar> asyncHttpClientClientCar,
            IAsyncHttpClientUser<User> asyncHttpClientUser,
            IAsyncHttpClientCar<Car> asyncHttpClientCar,
            IAsyncRepositoryCarEquipment<CarEquipment> asyncRepositoryCarEquipment)
        {
            this.asyncRepositoryCarEquipment = asyncRepositoryCarEquipment;
            this.asyncHttpClientUser = asyncHttpClientUser;
            this.asyncHttpClientClientCar = asyncHttpClientClientCar;
            this.asyncHttpClientOrder = asyncHttpClientOrder;
            this.asyncHttpClientCar = asyncHttpClientCar;
        }

        public async Task<int> Create(PostOrderDto item, string jwt = null)
        {
            Car car = await asyncHttpClientCar.SetJwt(jwt).GetByVin(item.VIN);
            CarEquipment carEquipment = await asyncRepositoryCarEquipment
                                                    .GetByName(car.NameCarEquipment);
            decimal totalCost = car.Cost;
            foreach(var i in carEquipment.Equipment)
            {
                totalCost += i.Value.Cost;
            }
            totalCost = car.ActionCar != null ?
                (decimal)(Convert.ToDouble(totalCost) * (100-(double)car.ActionCar.SharePercentage) / 100.0) : totalCost;
            Order order = new() { 
                CarId = car.Id,
                DateOfBuyCar = DateTime.Now,
                TotalCost = totalCost 
            };
            ClientCar clientCar = new() {
                CarId = car.Id,
                UserId = (await asyncHttpClientUser.SetJwt(jwt).GetByEmail(item.Email)).Id,
                RegisterNumber = item.RegisterNumber
            };

            return await asyncHttpClientOrder.SetJwt(jwt).Add(order) == 200 ?
                await asyncHttpClientClientCar.SetJwt(jwt).Add(clientCar) : 404;
        }

        public async Task<IEnumerable<Order>> GetAll( string jwt = null)
        {
            return await asyncHttpClientOrder.SetJwt(jwt).GetAll();
        }

        public async Task<Order> GetById(int id, string jwt = null)
        {
            return await asyncHttpClientOrder.SetJwt(jwt).GetById(id);
        }
    }
}
