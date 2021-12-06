using AutoMapper;
using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{
    public class AsyncServiceEmployee : IAsyncServiceEmployee<Employee>
    {
        private readonly IAsyncHttpClientEmployee<Employee> httpClientEmployee;
        private readonly IAsyncHttpClientUser<User> httpClientUser;
        private readonly IMapper mapper;

        public AsyncServiceEmployee(
            IAsyncHttpClientEmployee<Employee> httpClient,
            IMapper mapper,
            IAsyncHttpClientUser<User> httpClientUser)
        {
            this.httpClientUser = httpClientUser;
            this.mapper = mapper;
            this.httpClientEmployee = httpClient;
        }

        public async Task<int> Create(PostEmployeeDto item)
        {
            User getUser = await httpClientUser.GetByEmail(item.Email);
            Employee empAdd = mapper.Map<Employee>(item);
            empAdd.IsActive = false;
            empAdd.StartWorkDate = DateTime.Now;
            empAdd.UserId = getUser.Id;
            getUser.Status = Status.ACTIVE;
            getUser.RoleId = item.RoleId;

            return await httpClientUser.Update(getUser) == 200 ?
                await httpClientEmployee.Add(empAdd) : 404;
        }

        public async Task<Employee> FindById(int id)
        {
            return await httpClientEmployee.GetById(id);
        }

        public async Task<Employee> FindByUserEmail(string email)
        {
            return await httpClientEmployee.GetByUserEmail(email);
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await httpClientEmployee.GetAll();
        }

        public async Task<int> Remove(string email)
        {
            Employee employee = await httpClientEmployee.GetByUserEmail(email);

            return employee == null ? 404 : await httpClientEmployee.Remove(employee.Id);
        }

        public async Task<int> Update(PutEmployeeDto item)
        {
            User getUser = await httpClientUser.GetByEmail(item.Email);
            Employee empPut = await httpClientEmployee.GetByUserId(getUser.Id);
            empPut.UserId = getUser.Id;
            empPut.Address = item.Address;

            if (getUser.RoleId != item.RoleId)
            {
                getUser.RoleId = item.RoleId;
                return await httpClientUser.Update(getUser) == 200 ?
                     await httpClientEmployee.Update(empPut) : 404;
            }

            return await httpClientEmployee.Update(empPut);
        }
    }
}
