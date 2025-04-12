using DragonBall.Aplication.Interfaces;
using DragonBall.Aplication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DragonBall.Aplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IDragonBallService, DragonBallService>();
            return services;
        }
    }
}
