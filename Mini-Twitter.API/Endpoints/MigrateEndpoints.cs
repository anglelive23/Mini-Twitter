namespace Mini_Twitter.Endpoints
{
    public class MigrateEndpoints : CarterModule
    {
        public MigrateEndpoints() : base("/api/migrate") { }

        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/", async (TwitterContext context) =>
            {
                // database not exist
                if (!await context.Database.CanConnectAsync())
                {
                    await context.Database.MigrateAsync();
                    return Results.Ok(new
                    {
                        StatusCode = (int)StatusCodes.Status201Created,
                        Message = "Database created and updated to last migration."
                    });
                }
                // it exist, check latest version and apply all updates
                var currentMigration = context.Database.GetAppliedMigrations();
                var allMigrations = context.Database.GetMigrations();
                if (!currentMigration.SequenceEqual(allMigrations))
                {
                    await context.Database.MigrateAsync();
                    return Results.Ok(new
                    {
                        StatusCode = (int)HttpStatusCode.OK,
                        Message = "Database updated to latest migration."
                    });
                }
                return Results.Ok(new
                {
                    StatusCode = (int)StatusCodes.Status200OK,
                    Message = "Database already up to date."
                });
            })
            .WithTags("Database Migration");
        }
    }
}
