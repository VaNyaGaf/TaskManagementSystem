using Intaker.TMS.Bll.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Intaker.TMS.Bll.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddBllServices(this IServiceCollection services)
    {
        services.AddTransient<IWorkTaskService, WorkTaskService>();
        services.AddSingleton<IServiceBusHandler, ServiceBusHandler>();
        return services;
    }
}