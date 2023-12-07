namespace Mini_Twitter.Infrastructure.Configurations
{
    public class RetweetConfiguration : IEntityTypeConfiguration<Retweet>
    {
        public void Configure(EntityTypeBuilder<Retweet> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.UserId)
                .IsRequired();

            builder.Property(r => r.TweetId)
                .IsRequired();

            builder.Property(r => r.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
