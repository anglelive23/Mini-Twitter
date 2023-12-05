namespace Mini_Twitter.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(25);

            builder.Property(u => u.Bio)
                .HasMaxLength(100)
                .IsRequired(false);

            builder.Property(u => u.FollowersCount)
                .HasDefaultValue(0);

            builder.Property(u => u.FollowingCount)
               .HasDefaultValue(0);
        }
    }
}
