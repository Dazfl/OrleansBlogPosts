using Microsoft.AspNetCore.Mvc;
using OrleansBlogPosts.Api.Grains;

namespace OrleansBlogPosts.Api.Features.BlogPosts
{
    public static class GetBlogPostById
    {
        public static void Map(this IEndpointRouteBuilder builder)
        {
            builder.MapGet("/{id}", GetBlogPostByIdCommand)
                .WithOpenApi();
        }

        internal static async Task<IActionResult> GetBlogPostByIdCommand([FromServices] IClusterClient clusterClient, long id)
        {
            var blogPost = clusterClient.GetGrain<IBlogPostGrain>(id);

            return new OkResult();
        }
    }
}
