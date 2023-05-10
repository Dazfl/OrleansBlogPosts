using Orleans.Runtime;
using OrleansBlogPosts.Api.Data;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostGrain : IGrainWithIntegerKey
    {
        public Task CreateBlogPostAsync(BlogPost newBlogPost);

        public Task<BlogPost> GetBlogPost();
    }

    public class BlogPostGrain : Grain, IBlogPostGrain
    {
        private readonly IPersistentState<BlogPost> _state;

        public BlogPostGrain([PersistentState(stateName: "BlogPostsState", storageName: "BlogPostsStorage")] IPersistentState<BlogPost> state)
        {
            _state = state;
        }

        public async Task CreateBlogPostAsync(BlogPost newBlogPost)
        {
            _state.State = newBlogPost;
            await _state.WriteStateAsync();

            var blogPosts = GrainFactory.GetGrain<IBlogPostsIndexGrain>(0);
            await blogPosts.AddToIndex(newBlogPost);
        }

        public Task<BlogPost> GetBlogPost()
        {
            var blogPost = _state.State;
            return Task.FromResult(blogPost);
        }
    }
}
