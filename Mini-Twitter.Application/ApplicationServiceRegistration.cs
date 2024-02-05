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

            #region AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            #endregion

            #region HttpContextAccessor
            services.AddHttpContextAccessor();
            #endregion

            return services;
        }
    }
}
