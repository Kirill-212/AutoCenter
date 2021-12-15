using MicroServiceApp.HttpClientLayer;
using MicroServiceApp.InfrastructureLayer.Models;
using MicroServiceApp.InfrastructureLayer.HashPassword;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MicroServiceApp.ServiceUser.Services
{

    public class AsyncServiceUser : IAsyncServiceUser<User>
    {
        private readonly IAsyncHttpClientUser<User> httpClientUser;

        public AsyncServiceUser(
            IAsyncHttpClientUser<User> httpClient
            )
        {
            httpClientUser = httpClient;
        }

        public async Task<int> Create(User item, string jwt = null)
        {
            item.Password = HashPassword
                .HashPasswordUser(item.Password, System.Text.Encoding.UTF8.GetBytes("-qwert"));
            item.Status = Status.CREATED;
            item.RoleId = 2;

            return await httpClientUser.SetJwt(jwt).Add(item);
        }

        public async Task<User> FindById(int id, string jwt = null)
        {
            
            return await httpClientUser.SetJwt(jwt).GetById(id);
        }

        public async Task<IEnumerable<User>> GetAll(string jwt = null)
        {
            return await httpClientUser.SetJwt(jwt).GetAll();
        }

        public async Task<IEnumerable<User>> GetAllUsersNotAddedToEmp(string jwt = null)
        {
            return await httpClientUser.SetJwt(jwt).GetAllUsersNotAddedToEmp();
        }

        public async Task<User> GetByEmail(string email, string jwt = null)
        {
            return await httpClientUser.SetJwt(jwt).GetByEmail(email);
        }

        public async Task<int> Remove(string email, string jwt = null)
        {
            User user = await httpClientUser.SetJwt(jwt).GetByEmail(email);

            return user == null ? 404 : await httpClientUser.SetJwt(jwt).Remove(user.Id);
        }

        public async Task<int> Update(User item, string jwt = null)
        {
            User getUserInfo = await httpClientUser.SetJwt(jwt).GetByEmail(item.Email);
            item.Password = HashPassword
                .HashPasswordUser(item.Password, System.Text.Encoding.UTF8.GetBytes("-qwert"));
            item.Id = getUserInfo.Id;
            item.Status = getUserInfo.Status;
            item.RoleId = getUserInfo.RoleId;

            return await httpClientUser.SetJwt(jwt).Update(item);
        }

        public async Task<int> UpdateStatusByEmail(string email, string jwt = null)
        {
            User user = await httpClientUser.SetJwt(jwt).GetByEmail(email);
            if (user != null)
            {
                if (user.Role.RoleName != "USER") return 404;
                user.Status = user.Status == Status.ACTIVE ? Status.CREATED : Status.ACTIVE;
                return await httpClientUser.SetJwt(jwt).Update(user);
            }
            return 404;
        }
    }
}
