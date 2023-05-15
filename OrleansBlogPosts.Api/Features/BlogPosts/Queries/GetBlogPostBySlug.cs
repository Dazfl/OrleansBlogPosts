using MediatR;
using OrleansBlogPosts.Api.Grains;
using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Features.BlogPosts.Queries
{
    /// <summary>
    /// Query to retrieve a blog post by slug
    /// </summary>
    public static class GetBlogPostBySlug
    {
        /// <summary>
        /// Query request
        /// </summary>
        public record Query(string Slug) : IRequest<BlogPost>;

        /// <summary>
        /// Query handler
        /// </summary>
        public sealed class QueryHandler : IRequestHandler<Query, BlogPost>
        {
            private readonly IGrainFactory _grainFactory;

            public QueryHandler(IGrainFactory grainFactory)
            {
                _grainFactory = grainFactory;
            }

            public async Task<BlogPost?> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogPostManager = _grainFactory.GetGrain<IBlogPostsManagerGrain>(0);
                return await blogPostManager.GetBlogPostBySlug(request.Slug);
            }
        }
    }
}
