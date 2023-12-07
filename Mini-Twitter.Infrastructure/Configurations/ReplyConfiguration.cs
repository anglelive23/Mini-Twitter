namespace Mini_Twitter.Infrastructure.Configurations
{
    public class ReplyConfiguration : IEntityTypeConfiguration<Reply>
    {
        public void Configure(EntityTypeBuilder<Reply> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Context)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(x => x.UserId)
                .IsRequired();

            //builder.Property(x => x.TweetId)
            //    .IsRequired();

            builder.Property(x => x.IsDeleted)
                .HasDefaultValue(false);
        }
    }
}
