﻿namespace Mini_Twitter.API
{
    public static class APIServiceRegistration
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services, WebApplicationBuilder builder)
        {
            #region MediatR
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            #endregion

            #region Cache
            //builder.Services.AddOutputCache(options =>
            //{
            //    options.AddPolicy("Tweets", policy => policy.Tag("Tweets").Expire(TimeSpan.FromHours(1)));
            //    options.AddPolicy("Tweet", policy => policy.Tag("Tweets").SetVaryByQuery("key").Expire(TimeSpan.FromHours(1)));
            //});
            #endregion

            #region Serilog
            builder.Host.UseSerilog();
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message}{NewLine}{Exception}", theme: AnsiConsoleTheme.Code)
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            #endregion

            #region Cors
            builder.Services.AddCors();
            #endregion

            #region Carter [Minimal API's]
            services.AddCarter();
            #endregion

            #region Redis
            services.AddStackExchangeRedisCache(options =>
            {
                // Dynamic according to current work env.
                options.Configuration = builder.Environment.IsDevelopment()
                    ? "localhost:6379,connectTimeout=2000,syncTimeout=2000,abortConnect=false"
                    : builder.Configuration.GetConnectionString("Redis");
            });
            #endregion

            return services;
        }
    }
}
