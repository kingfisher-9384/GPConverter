using GPConverter.Interfaces;
using GPConverter.Services;
using Microsoft.Extensions.Configuration;
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
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json");
        var config = configuration.Build();
        
        var serviceProvider = new ServiceCollection()
            .AddSingleton<IConfiguration>(config)
            .AddScoped<IConversionManager, ConversionManager>()
            .AddScoped<IImageFileConversions, ImageFileConversions>()
            .AddLogging(options =>
            {
                options.ClearProviders();
            })
            .AddSingleton<Application>()
            .BuildServiceProvider();
        
        return serviceProvider;
    }
}
