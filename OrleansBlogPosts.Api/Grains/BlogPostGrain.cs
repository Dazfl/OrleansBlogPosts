using Orleans.Runtime;
using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostGrain : IGrainWithIntegerKey
    {
        public Task CreateAsync(BlogPost newBlogPost);

        public Task<BlogPost> GetAsync();
    }

    /// <summary>
    /// Blog post gain
    /// </summary>
    public class BlogPostGrain : Grain, IBlogPostGrain
    {
        private readonly IPersistentState<BlogPost> _state;

        public BlogPostGrain([PersistentState(stateName: "BlogPostsState", storageName: "BlogPostsStorage")] IPersistentState<BlogPost> state)
        {
            _state = state;
        }

        /// <summary>
        /// Create a new blog post
        /// </summary>
        public async Task CreateAsync(BlogPost newBlogPost)
        {
            // Save the blog post to state
            _state.State = newBlogPost;
            await _state.WriteStateAsync();
        }

        /// <summary>
        /// Retrieve the blog post
        /// </summary>
        public Task<BlogPost> GetAsync()
        {
            var blogPost = _state.State;
            return Task.FromResult(blogPost);
        }
    }
}
