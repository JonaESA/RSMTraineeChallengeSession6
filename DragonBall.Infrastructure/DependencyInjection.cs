using DragonBall.Domain.Interfaces;
using DragonBall.Infrastructure.Persistence;
using DragonBall.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DragonBall.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
                                                               IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    opt => opt.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName));
            });

            services.AddTransient<IRepository, Repository>();

            return services;
        }
    }
}
