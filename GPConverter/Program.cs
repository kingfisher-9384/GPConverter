using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GPConverter;

class Program
{
    [STAThread]
    static void Main(string[] args)
    {
        var services = CreateServices();

        var app = services.GetRequiredService<Application>();
        app.Main(args).Wait();
    }

    private static ServiceProvider CreateServices()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(options =>
            {
                options.ClearProviders();
            })
            .AddSingleton<Application>(new Application())
            .BuildServiceProvider();
        
        return serviceProvider;
    }
}
