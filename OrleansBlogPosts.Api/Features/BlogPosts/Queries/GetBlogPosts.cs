using MediatR;
using OrleansBlogPosts.Api.Grains;
using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Features.BlogPosts.Queries
{
    /// <summary>
    /// Query to retrieve a list of Blog Posts
    /// </summary>
    public static class GetBlogPosts
    {
        /// <summary>
        /// Query request (empty)
        /// </summary>
        public record Query() : IRequest<IQueryable<BlogPost>>;

        /// <summary>
        /// Query handler
        /// </summary>
        public sealed class QueryHandler : IRequestHandler<Query, IQueryable<BlogPost>>
        {
            private readonly IGrainFactory _grainFactory;

            public QueryHandler(IGrainFactory grainFactory)
            {
                _grainFactory = grainFactory;
            }

            public async Task<IQueryable<BlogPost>> Handle(Query request, CancellationToken cancellationToken)
            {
                var blogPostManager = _grainFactory.GetGrain<IBlogPostsManagerGrain>(0);
                return (await blogPostManager.GetBlogPosts()).AsQueryable();
            }
        }
    }
}
