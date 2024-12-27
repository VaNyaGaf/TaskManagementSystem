using Intaker.TMS.Dal.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Intaker.TMS.Dal.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddRepositories(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<WorkTaskContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<IWorkTaskRepository, WorkTaskRepository>();
        return services;
    }

}
