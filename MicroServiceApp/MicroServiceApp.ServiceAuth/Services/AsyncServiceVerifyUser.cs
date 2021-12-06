using MicroServiceApp.InfrastructureLayer.Dto;
using MicroServiceApp.InfrastructureLayer.HashPassword;
using MicroServiceApp.InfrastructureLayer.Models;

namespace MicroServiceApp.ServiceAuth.Services
{
    public class AsyncServiceVerifyUser : IAsyncServiceVerifyUser<string, AuthDto, User>
    {
        public string VerifyUser(AuthDto authDto, User user)
        {
            if (user == null) return "Check input Email";
            if (user.Status == Status.CREATED)
            {
                return "your account is not active";
            }
            if (!HashPassword.Verify(user.Password, authDto.Password))
            {
                return "Check input Password";
            }

            return null;
        }
    }
}
