using Orleans.Runtime;
using OrleansBlogPosts.Api.Data;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostGrain : IGrainWithIntegerKey
    {
        public Task CreateBlogPostAsync(BlogPost newBlogPost);

        public Task<BlogPost> GetBlogPost();
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
        public async Task CreateBlogPostAsync(BlogPost newBlogPost)
        {
            // Save the blog post to state
            _state.State = newBlogPost;
            await _state.WriteStateAsync();

            // Add the blog post to the index (index grain id is default 0)
            var blogPosts = GrainFactory.GetGrain<IBlogPostsIndexGrain>(0);
            await blogPosts.AddToIndex(newBlogPost);
        }

        /// <summary>
        /// Retrieve the blog post
        /// </summary>
        public Task<BlogPost> GetBlogPost()
        {
            var blogPost = _state.State;
            return Task.FromResult(blogPost);
        }
    }
}
