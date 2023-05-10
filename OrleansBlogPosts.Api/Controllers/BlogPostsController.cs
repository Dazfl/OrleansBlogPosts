using Microsoft.AspNetCore.Mvc;
using OrleansBlogPosts.Api.Data;
using OrleansBlogPosts.Api.Grains;

namespace OrleansBlogPosts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IGrainFactory _grainFactory;

        public BlogPostsController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        [HttpGet]
        public async Task<HashSet<BlogPost>> GetBlogPosts(CancellationToken cancellationToken)
        {
            var blogPosts = _grainFactory.GetGrain<IBlogPostsIndexGrain>(0);
            return await blogPosts.GetBlogPosts();
        }

        [HttpPost]
        public Task AddBlogPost(BlogPost blogPost, CancellationToken cancellationToken)
        {
            var blogPostGrain = _grainFactory.GetGrain<IBlogPostGrain>(blogPost.Id);
            return blogPostGrain.CreateBlogPostAsync(blogPost);
        }
    }
}
