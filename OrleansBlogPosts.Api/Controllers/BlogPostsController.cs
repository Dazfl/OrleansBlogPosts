using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrleansBlogPosts.Api.Features.BlogPosts.Commands;
using OrleansBlogPosts.Api.Features.BlogPosts.Queries;
using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IMediator _bus;

        public BlogPostsController(IMediator bus)
        {
            _bus = bus;
        }

        [HttpGet]
        public async Task<IQueryable<BlogPost>> GetBlogPosts(CancellationToken cancellationToken)
        {
            return await _bus.Send(new GetBlogPosts.Query(), cancellationToken);
        }

        [HttpGet("ById/{id}")]
        public async Task<BlogPost> GetBlogById(long id, CancellationToken cancellationToken)
        {
            return await _bus.Send(new GetBlogPostById.Query(id), cancellationToken);
        }

        [HttpGet("BySlug/{slug}")]
        public async Task<BlogPost> GetBlogBySlug(string slug, CancellationToken cancellationToken)
        {
            return await _bus.Send(new GetBlogPostBySlug.Query(slug), cancellationToken);
        }

        [HttpPost]
        public async Task<CreateBlogPost.CreateResponse> CreateBlogPost(BlogPost blogPost, CancellationToken cancellationToken)
        {
            return await _bus.Send(new CreateBlogPost.Command(blogPost), cancellationToken);
        }
    }
}
