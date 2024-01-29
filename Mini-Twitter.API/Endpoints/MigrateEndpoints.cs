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
                    return Results.Ok(new { Message = "Database created and updated to last migration." });
                }

                // database exists, check latest version and apply all updates
                var appliedMigrations = context.Database.GetAppliedMigrations();
                var allMigrations = context.Database.GetMigrations();
                if (!appliedMigrations.SequenceEqual(allMigrations))
                {
                    await context.Database.MigrateAsync();
                    return Results.Ok(new { Message = "Database updated to latest migration." });
                }

                // database exists, and up to date
                return Results.Ok(new { Message = "Database already up to date." });
            })
            .WithTags("Database Migration");
        }
    }
}
