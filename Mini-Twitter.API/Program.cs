using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<LogActivityFilter>();
}).AddOData(options =>
{
    options.AddRouteComponents("api/odata", new TwitterEntityDataModel().GetEntityDataModel()).Select().Filter().OrderBy().Expand().SetMaxTop(1000);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Mini-Twitter.API", Version = "v1" });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services
    .AddApplicationServices()
    .AddAPIServices(builder)
    .AddInfrastructureServices(builder.Configuration);

EnsureDatabaseCreationAndUpdateToLastMigration(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(cors => cors.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.MapControllers();
app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
app.UseOutputCache();
app.Run();


void EnsureDatabaseCreationAndUpdateToLastMigration(IServiceCollection services)
{
    using (var serviceScope = services.BuildServiceProvider().CreateScope())
    {
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<TwitterContext>();

        var appliedMigrations = dbContext.Database.GetAppliedMigrations().ToList();
        var allMigrations = dbContext.Database.GetMigrations().ToList();

        if (!appliedMigrations.SequenceEqual(allMigrations))
        {
            dbContext.Database.Migrate();
            Log.Information("Database updated to last migration.");
        }

        Log.Information("Database already up to date.");
    }
}
