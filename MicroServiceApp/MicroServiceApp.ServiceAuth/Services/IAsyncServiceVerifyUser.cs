namespace MicroServiceApp.ServiceAuth.Services
{
    public interface IAsyncServiceVerifyUser<T, V, C>
    {
        string VerifyUser(V authDto, C user);
    }
}
