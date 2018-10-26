namespace DotNetFest2018.Demo1.Services
{
    public interface IConnectionStringService
    {
        string Get();
    }

    public class ConnectionStringService : IConnectionStringService
    {
        public string Get() => "Data Source=production.db";
    }
}