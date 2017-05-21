using WindowsServicePortal;
public class TestStartup : Startup
{
    public void ConfigureTestServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.Replace<IServiceControllerFactory, IMockedService>();
    }
}