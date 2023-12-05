using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace Mini_Twitter.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region MediatR
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            #endregion

            return services;
        }
    }
}
