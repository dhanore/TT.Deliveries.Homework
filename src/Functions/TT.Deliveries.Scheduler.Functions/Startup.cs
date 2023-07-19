using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using TT.Deliveries.Application.Features.DeliveryFeatures;

[assembly: FunctionsStartup(typeof(FunctionApp2.Startup))]

namespace FunctionApp2
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton<IDeliveryServices, DeliveryServices>();
        }
    }
}
