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
        public sealed class CommandHandler : IRequestHandler<Command, CreateResponse>
        {
            private readonly IGrainFactory _grainFactory;

            public CommandHandler(IGrainFactory grainFactory)
            {
                _grainFactory = grainFactory;
            }

            public async Task<CreateResponse> Handle(Command request, CancellationToken cancellationToken)
            {
                var blogPostManager = _grainFactory.GetGrain<IBlogPostsManagerGrain>(0);
                await blogPostManager.CreateBlogPost(request.BlogPost);

                return new CreateResponse(true, request.BlogPost.Id);
            }
        }

        /// <summary>
        /// Command response
        /// </summary>
        public record CreateResponse(bool IsSuccessful, long BlogPostId);
    }
}
