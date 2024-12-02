using MediatR;
using OrleansBlogPosts.Api.Grains;
using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Features.BlogPosts.Commands
{
    /// <summary>
    /// Command to create a new blog post
    /// </summary>
    public static class CreateBlogPost
    {
        /// <summary>
        /// Command request
        /// </summary>
        public record Command(BlogPost BlogPost) : IRequest<CreateResponse>;

        /// <summary>
        /// Command handler
        /// </summary>
        public sealed class CommandHandler(IGrainFactory grainFactory, IClusterClient clusterClient) : IRequestHandler<Command, CreateResponse>
        {
            public async Task<CreateResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var blogPostManager = clusterClient.GetGrain<IBlogPostGrain>(0);
                //var blogPostManager = grainFactory.GetGrain<IBlogPostsManagerGrain>(0);
                //await blogPostManager.CreateBlogPost(request.BlogPost);
                await blogPostManager.CreateAsync(request.BlogPost);

                return new CreateResponse(true, request.BlogPost.Id);
            }
        }

        /// <summary>
        /// Command response
        /// </summary>
        public record CreateResponse(bool IsSuccessful, long BlogPostId);
    }
}
