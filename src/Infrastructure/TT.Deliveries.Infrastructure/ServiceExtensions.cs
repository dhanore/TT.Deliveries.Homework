using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using TT.Deliveries.Application.Repositories;
using TT.Deliveries.Domain.Common;
using TT.Deliveries.Persistence.Context;
using TT.Deliveries.Persistence.Repositories;

namespace TT.Deliveries.Persistence
{
    public static class ServiceExtensions
    {
        public static void ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<MongoDBSettings>()
           .Configure<IConfiguration>((settings, configuration) =>
           {
               configuration.GetSection(nameof(MongoDBSettings)).Bind(settings);
           });

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeliveryRepository, DeliveryRepository>();
            var mongoDBSettings = configuration.GetSection(nameof(MongoDBSettings)).Get<MongoDBSettings>();
            services.AddSingleton<IMongoClient>(x => new MongoClient(mongoDBSettings.ConnectionString));
            services.AddTransient(typeof(MongoDBContext<>));
        }
    }
}
