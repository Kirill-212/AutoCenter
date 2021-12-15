using MicroServiceApp.HttpClientLayer.ServiceCar;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceCar.Services
{
    public class AsyncServiceTestDrive : IAsyncServiceTestDrive<TestDrive>
    {
        private readonly IAsyncHttpClientTestDrive<TestDrive> asyncHttpClientTestDrive;
        private readonly IAsyncHttpClientCar<Car> asyncHttpClientCar;

        public AsyncServiceTestDrive(
            IAsyncHttpClientTestDrive<TestDrive> asyncHttpClientTestDrive,
            IAsyncHttpClientCar<Car> asyncHttpClientCar
            )
        {
            this.asyncHttpClientCar = asyncHttpClientCar;
            this.asyncHttpClientTestDrive = asyncHttpClientTestDrive;
        }

        public async Task<int> Create(PostTestDriveDto item)
        {
            IEnumerable<TestDrive> testDrives = await asyncHttpClientTestDrive.GetByVin(item.Vin);
            int hour;
            int inputHour = int.Parse(item.Time.Split(':')[0]);
            foreach (TestDrive i in testDrives)
            {
                if (i.Date == item.Date)
                {
                    hour = int.Parse(i.Time.Split(':')[0]);
                    if (inputHour == hour)
                    {
                        return 400;
                    }
                }
            }
            Car car = await asyncHttpClientCar.GetByVin(item.Vin);
            if (car == null) return 400;
            TestDrive testDrive = new()
            {
                CarId = car.Id,
                IsActive = true,
                Date = item.Date,
                Time = item.Time
            };

            return await asyncHttpClientTestDrive.Add(testDrive);
        }

        public async Task<IEnumerable<TestDrive>> GetAll()
        {
            return await asyncHttpClientTestDrive.GetAll();
        }

        public async Task<int> Put(PutTestDriveDto item)
        {
            Car car = await asyncHttpClientCar.GetByVin(item.Vin);
            if (car == null) return 400;
            TestDrive testDrive = await asyncHttpClientTestDrive.GetByAllData(new()
            {
                Car = car,
                Date = item.Date,
                Time = item.Time
            });
            if (testDrive == null) return 404;
            testDrive.IsActive = testDrive.IsActive != true;

            return await asyncHttpClientTestDrive.Update(testDrive);
        }
    }
}
