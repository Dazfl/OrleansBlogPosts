using OrleansBlogPosts.Api.Features.BlogPosts;

namespace OrleansBlogPosts.Api.Extensions
{
    public static class EndpointExtensions
    {
        public static void MapFeatureEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("")
                .WithOpenApi();

            endpoints.MapBlogPostsEndpoints();
        }


        public static void MapBlogPostsEndpoints(this IEndpointRouteBuilder builder)
        {
            var endpoints = builder.MapGroup("/BlogPosts")
                .WithTags("Blog Posts");

            CreateBlogPost.Map(endpoints);
        }
    }
}
