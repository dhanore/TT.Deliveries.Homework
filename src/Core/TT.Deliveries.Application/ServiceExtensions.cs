using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TT.Deliveries.Application.Common;
using TT.Deliveries.Application.Features.DeliveryFeatures;
using TT.Deliveries.Application.Features.UserFeatures;

namespace TT.Deliveries.Application;
public static class ServiceExtensions
{
    public static void ConfigureApplication(this IServiceCollection services)
    {
        services.AddTransient<IUserServices, UserServices>();
        services.AddTransient<IDeliveryServices, DeliveryServices>();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(ValidationBehavior<,>));
    }
}
