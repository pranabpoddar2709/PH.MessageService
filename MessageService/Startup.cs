using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PH.MessageService.ServiceImp;
using PHDataAccessLayer.DataAccess;
using PHDataAccessLayer.DataInterfaces;
using PHDataAccessLayer.Models;

[assembly: FunctionsStartup(typeof(MessageService.Startup))]
namespace MessageService
{
    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            builder.Services.AddSingleton(configuration);
            var configuration1 = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json", true)
                .Build();
            var conn = configuration.GetConnectionString("MyDbConnStr");
            builder.Services.AddScoped<INotificationServiceFactory, NotificationServiceFactory>();

            builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        }
    }
}
