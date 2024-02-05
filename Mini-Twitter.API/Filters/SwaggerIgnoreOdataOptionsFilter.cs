using Swashbuckle.AspNetCore.SwaggerGen;

namespace Mini_Twitter.API.Filters
{
    public class SwaggerIgnoreOdataOptionsFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var optionsParameteres = context.ApiDescription
                .ParameterDescriptions
                .FirstOrDefault(p => p.Type == typeof(ODataQueryOptions<TweetDto>));

            if (optionsParameteres is not null)
                operation.Parameters
                    .Remove(operation.Parameters.FirstOrDefault(p => p.Name == optionsParameteres.Name));
        }
    }
}
