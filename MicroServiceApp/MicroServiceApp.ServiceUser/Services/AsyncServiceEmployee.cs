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

        public async Task<int> Create(PostEmployeeDto item, string jwt = null)
        {
            User getUser = await httpClientUser.SetJwt(jwt).GetByEmail(item.Email);
            Employee empAdd = mapper.Map<Employee>(item);
            empAdd.IsActive = false;
            empAdd.StartWorkDate = DateTime.Now;
            empAdd.UserId = getUser.Id;
            getUser.Status = Status.ACTIVE;
            getUser.RoleId = item.RoleId;

            return await httpClientUser.SetJwt(jwt).Update(getUser) == 200 ?
                await httpClientEmployee.SetJwt(jwt).Add(empAdd) : 404;
        }

        public async Task<Employee> FindById(int id, string jwt = null)
        {
            return await httpClientEmployee.SetJwt(jwt).GetById(id);
        }

        public async Task<Employee> FindByUserEmail(string email, string jwt = null)
        {
            return await httpClientEmployee.SetJwt(jwt).GetByUserEmail(email);
        }

        public async Task<IEnumerable<Employee>> GetAll(string jwt = null)
        {
            return await httpClientEmployee.SetJwt(jwt).GetAll();
        }

        public async Task<int> Remove(string email, string jwt = null)
        {
            Employee employee = await httpClientEmployee.SetJwt(jwt).GetByUserEmail(email);
            User getUser = await httpClientUser.SetJwt(jwt).GetByEmail(email);
            getUser.RoleId = 2;

            return employee == null ? 404 : await httpClientUser.SetJwt(jwt).Update(getUser) == 200 ? await httpClientEmployee.SetJwt(jwt).Remove(employee.Id):404;
        }

        public async Task<int> Update(PutEmployeeDto item, string jwt = null)
        {
            User getUser = await httpClientUser.SetJwt(jwt).GetByEmail(item.Email);
            Employee empPut = await httpClientEmployee.SetJwt(jwt).GetByUserId(getUser.Id);
            empPut.UserId = getUser.Id;
            empPut.Address = item.Address;

            if (getUser.RoleId != item.RoleId)
            {   if(item.RoleId==2 &&(getUser.RoleId==1|| getUser.RoleId == 3))
                {
                    getUser.RoleId = item.RoleId;
                    return await httpClientUser.SetJwt(jwt).Update(getUser) == 200 ?await Remove(getUser.Email) : 404;
                }
                getUser.RoleId = item.RoleId;
                return await httpClientUser.SetJwt(jwt).Update(getUser) == 200 ?
                     await httpClientEmployee.SetJwt(jwt).Update(empPut) : 404;
            }

            return await httpClientEmployee.SetJwt(jwt).Update(empPut);
        }
    }
}
