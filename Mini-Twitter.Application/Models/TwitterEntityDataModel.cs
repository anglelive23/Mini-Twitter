namespace Mini_Twitter.Application.Models
{
    public class TwitterEntityDataModel
    {
        public IEdmModel GetEntityDataModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Tweet>("Tweets");
            builder.EntitySet<ApplicationUser>("ApplicationUsers");

            builder.EnableLowerCamelCase();
            return builder.GetEdmModel();
        }
    }
}
