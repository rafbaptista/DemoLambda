using Amazon.S3;
using DemoLambda.Application.Factories;
using DemoLambda.Application.Interfaces;
using DemoLambda.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DemoLambda
{
    public abstract class BaseFunction
    {
        protected readonly IHost host;
        private IConfiguration _configuration;
        protected BaseFunction()
        {
            try
            {
                LoggerFactory.CreateLogger();

                host = new HostBuilder()
                               .ConfigureHostConfiguration(configHost =>
                               {
                                   configHost.AddEnvironmentVariables();
                               })
                               .ConfigureAppConfiguration((hostingContext, config) =>
                               {
                                   config.SetBasePath(Directory.GetCurrentDirectory());
                                   _configuration = config.AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true).Build();
                               })
                               .ConfigureServices((hostContext, services) =>
                               {
                                   RegisterServices(services);
                               })
                               .ConfigureLogging(logging =>
                               {
                                   logging.Services.AddLogging();
                                   logging.AddSerilog();
                               })
                               .Build();

                //host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "A aplicação falhou em iniciar");
                Environment.Exit(0);
            }
        }

        private static IServiceCollection RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IFunctionService, FunctionService>();
            services.AddAWSService<IAmazonS3>();
            return services;
        }

        ~BaseFunction()
        {
            host?.Dispose();
        }

    }
}
