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
    public class BlogPostGrain([PersistentState(stateName: "BlogPostsState", storageName: "grain-storage")] IPersistentState<BlogPost> state) : Grain, IBlogPostGrain
    {
        private readonly IPersistentState<BlogPost> _state = state;

        /// <summary>
        /// Create a new blog post
        /// </summary>
        public async Task CreateAsync(BlogPost newBlogPost)
        {
            var grainId = this.GetGrainId();
            newBlogPost.Id = grainId.GetIntegerKey();

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
