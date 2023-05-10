using Orleans.Runtime;
using OrleansBlogPosts.Api.Data;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostsIndexGrain : IGrainWithIntegerKey
    {
        public Task<HashSet<BlogPost>> GetBlogPosts();

        public Task AddToIndex(BlogPost blogPost);
    }

    /// <summary>
    /// Blog Post index grain
    /// </summary>
    public class BlogPostsIndexGrain : Grain, IBlogPostsIndexGrain
    {
        private readonly IPersistentState<HashSet<long>> _blogPostIds;
        private readonly HashSet<BlogPost> _blogPosts = new();

        public BlogPostsIndexGrain([PersistentState(stateName: "BlogPostsIndexState", storageName: "BlogPostsStorage")] IPersistentState<HashSet<long>> state)
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
                    _blogPosts.Add(await blogPostGrain.GetBlogPost());
                });
        }

        /// <summary>
        /// Add a blog post id to the index.
        /// </summary>
        public Task AddToIndex(BlogPost blogPost)
        {
            // Add the blog post id
            _blogPostIds.State ??= new();
            _blogPostIds.State.Add(blogPost.Id);

            // Add the blog post to the list
            _blogPosts.Add(blogPost);

            // Save
            return _blogPostIds.WriteStateAsync();
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
