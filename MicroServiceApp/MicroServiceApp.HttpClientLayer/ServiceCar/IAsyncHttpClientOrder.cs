namespace MicroServiceApp.HttpClientLayer.ServiceCar
{
    public interface IAsyncHttpClientOrder<T>:IAsynHttpClient<T>
    {
        IAsyncHttpClientOrder<T> SetJwt(string jwt = null);
    }
}
