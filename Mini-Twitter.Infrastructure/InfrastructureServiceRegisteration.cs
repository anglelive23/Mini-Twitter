namespace Mini_Twitter.Infrastructure
{
    public static class InfrastructureServiceRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region DbContext
            services.AddIdentityCore<ApplicationUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<TwitterContext>();
            services.AddDbContext<TwitterContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region JWT
            services.Configure<JWT>(configuration.GetSection(nameof(JWT)));
            services.AddScoped<IAuthService, AuthService>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.RequireHttpsMetadata = false;
                opt.SaveToken = false;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidAudience = configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"] ?? "OdkSeYWNl/ECZJaRsjzTqQ9rGb7jp0Ovp57idk1LeGs=")),
                    ClockSkew = TimeSpan.Zero
                };
            });
            #endregion

            #region Repositories
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITweetRepository, TweetRepository>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITimelineRepository, TimelineRepository>();
            #endregion

            return services;
        }
    }
}
