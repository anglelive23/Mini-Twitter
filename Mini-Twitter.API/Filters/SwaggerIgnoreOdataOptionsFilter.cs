using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mini_Twitter.API.Filters
{
    public class SwaggerIgnoreOdataOptionsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameteresList = context.ApiDescription
                .ParameterDescriptions
                .Where(p => p.Type == typeof(ODataQueryOptions<TweetDto>)
                        || p.Type == typeof(ODataQueryOptions<ApplicationUserDto>)
                        || p.Type == typeof(ODataQueryOptions<RetweetDto>));

            foreach (var parameter in parameteresList)
            {
                if (parameteresList is not null)
                    operation.Parameters
                        .Remove(operation.Parameters.FirstOrDefault(p => p.Name == parameter.Name));
            }
        }
    }
}
