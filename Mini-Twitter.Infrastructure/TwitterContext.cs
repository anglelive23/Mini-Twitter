namespace Mini_Twitter.Infrastructure
{
    public class TwitterContext : IdentityDbContext<ApplicationUser>
    {
        public TwitterContext(DbContextOptions<TwitterContext> options) : base(options) { }
        public DbSet<Tweet> Tweets { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Retweet> Retweets { get; set; }
        //public DbSet<UserTweet> UserTweets { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TwitterContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles", "security");

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users", "security");

            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", "security");

            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", "security");

            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "security");

            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles", "security");

            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", "security");

            modelBuilder.Entity<RefreshToken>()
                .ToTable("RefreshTokens", "security");

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.Followers)
                .WithMany(u => u.Followees)
                .UsingEntity<UserFollowers>(
                    j => j.HasOne(uf => uf.Follower)
                          .WithMany()
                          .HasForeignKey(uf => uf.FollowerId),
                    j => j.HasOne(uf => uf.Followee)
                          .WithMany()
                          .HasForeignKey(uf => uf.FolloweeId),
                    j =>
                    {
                        j.ToTable("UserFollowers");
                        j.HasKey(uf => new { uf.FollowerId, uf.FolloweeId });
                    }
                );
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;

                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }

            foreach (var entry in ChangeTracker.Entries<ApplicationUser>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedDate = DateTime.UtcNow;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
