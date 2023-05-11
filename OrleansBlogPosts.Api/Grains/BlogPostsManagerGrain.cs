using Orleans.Runtime;
using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostsManagerGrain : IGrainWithIntegerKey
    {
        public Task<HashSet<BlogPost>> GetBlogPosts();

        public Task CreateBlogPost(BlogPost blogPost);
    }

    /// <summary>
    /// Blog Post index grain
    /// </summary>
    public class BlogPostsManagerGrain : Grain, IBlogPostsManagerGrain
    {
        private readonly IPersistentState<HashSet<long>> _blogPostIds;
        private readonly HashSet<BlogPost> _blogPosts = new();

        public BlogPostsManagerGrain([PersistentState(stateName: "BlogPostsIndexState", storageName: "BlogPostsStorage")] IPersistentState<HashSet<long>> state)
        {
            _blogPostIds = state;
        }

        /// <summary>
        /// When grain is activated, populated the list of Blog Posts
        /// </summary>
        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            // Don't do anything if we don't have any ids stored
            if (_blogPostIds is not { State.Count: > 0 })
                return;

            // Populate a list of blog posts by looping through each stored id and retrieving the blog post grain
            await Parallel.ForEachAsync(
                _blogPostIds.State,
                async (id, cancellationToken) =>
                {
                    var blogPostGrain = GrainFactory.GetGrain<IBlogPostGrain>(id);
                    _blogPosts.Add(await blogPostGrain.GetAsync());
                });
        }

        /// <summary>
        /// Add a blog post id to the index.
        /// </summary>
        public async Task CreateBlogPost(BlogPost blogPost)
        {
            // Add the blog post id
            _blogPostIds.State ??= new();
            _blogPostIds.State.Add(blogPost.Id);

            // Add the blog post to the list
            _blogPosts.Add(blogPost);

            // Save state
            await _blogPostIds.WriteStateAsync();

            // Create a single Blog Post grain
            var blogPostGrain = GrainFactory.GetGrain<IBlogPostGrain>(blogPost.Id);
            await blogPostGrain.CreateAsync(blogPost);
        }

        /// <summary>
        /// Return a list of Blog Posts that can be used to populate a grid that can be filtered, sorted and paged
        /// </summary>
        public Task<HashSet<BlogPost>> GetBlogPosts()
        {
            return Task.FromResult(_blogPosts);
        }
    }
}
