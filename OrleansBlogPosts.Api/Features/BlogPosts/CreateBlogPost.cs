using Microsoft.AspNetCore.Mvc;
using OrleansBlogPosts.Api.Grains;

namespace OrleansBlogPosts.Api.Features.BlogPosts
{
    public static class CreateBlogPost
    {
        public static void Map(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/", CreateBlogPostCommand)
                .WithOpenApi()
                .ProducesValidationProblem();
        }

        internal static async Task<IActionResult> CreateBlogPostCommand([FromServices] IClusterClient clusterClient)
        {
            var blogPost = clusterClient.GetGrain<IBlogPostGrain>(0);
            await blogPost.CreateBlogPost("Test", "", DateTime.Now);

            return new StatusCodeResult(StatusCodes.Status201Created);
        }
    }
}
