using HR_LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR_LeaveManagement.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<HrDbContext>(options => {
            options.UseSqlServer(config.GetConnectionString("HrDatabaseString"));
        });
        return services;
    }
}
