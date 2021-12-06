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
            this.httpClientUser = httpClient;
        }

        public async Task<int> Create(User item)
        {
            item.Password = HashPassword
                .HashPasswordUser(item.Password, System.Text.Encoding.UTF8.GetBytes("-qwert"));
            item.Status = Status.CREATED;
            item.RoleId = 2;

            return await httpClientUser.Add(item);
        }

        public async Task<User> FindById(int id)
        {
            return await httpClientUser.GetById(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await httpClientUser.GetAll();
        }

        public async Task<IEnumerable<User>> GetAllUsersNotAddedToEmp()
        {
            return await httpClientUser.GetAllUsersNotAddedToEmp();
        }

        public async Task<User> GetByEmail(string email)
        {
            return await httpClientUser.GetByEmail(email);
        }

        public async Task<int> Remove(string email)
        {
            User user = await httpClientUser.GetByEmail(email);

            return user == null ? 404 : await httpClientUser.Remove(user.Id);
        }

        public async Task<int> Update(User item)
        {
            User getUserInfo = await httpClientUser.GetByEmail(item.Email);
            item.Password = HashPassword
                .HashPasswordUser(item.Password, System.Text.Encoding.UTF8.GetBytes("-qwert"));
            item.Id = getUserInfo.Id;
            item.Status = getUserInfo.Status;
            item.RoleId = getUserInfo.RoleId;

            return await httpClientUser.Update(item);
        }
    }
}
