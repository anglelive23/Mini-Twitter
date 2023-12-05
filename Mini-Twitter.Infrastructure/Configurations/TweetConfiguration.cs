namespace Mini_Twitter.Infrastructure.Configurations
{
    public class TweetConfiguration : IEntityTypeConfiguration<Tweet>
    {
        public void Configure(EntityTypeBuilder<Tweet> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.Context)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(x => x.LikesCount)
                .HasDefaultValue(0);

            builder.Property(x => x.RetweetCount)
                .HasDefaultValue(0);

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
