using Orleans.Runtime;
using OrleansBlogPosts.Api.Data;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostsIndexGrain : IGrainWithIntegerKey
    {
        public Task<HashSet<BlogPost>> GetBlogPosts();

        public Task AddToIndex(BlogPost blogPost);
    }

    public class BlogPostsIndexGrain : Grain, IBlogPostsIndexGrain
    {
        private readonly IPersistentState<HashSet<long>> _blogPostIds;
        private readonly HashSet<BlogPost> _blogPosts = new();

        public BlogPostsIndexGrain([PersistentState(stateName: "BlogPostsIndexState", storageName: "BlogPostsStorage")] IPersistentState<HashSet<long>> state)
        {
            _blogPostIds = state;
        }

        public override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            if (_blogPostIds is not { State.Count: > 0 })
                return;

            await Parallel.ForEachAsync(
                _blogPostIds.State,
                async (id, cancellationToken) =>
                {
                    var blogPostGrain = GrainFactory.GetGrain<IBlogPostGrain>(id);
                    _blogPosts.Add(await blogPostGrain.GetBlogPost());
                });
        }

        public Task AddToIndex(BlogPost blogPost)
        {
            _blogPostIds.State ??= new();
            _blogPostIds.State.Add(blogPost.Id);

            _blogPosts.Add(blogPost);

            return _blogPostIds.WriteStateAsync();
        }

        public Task<HashSet<BlogPost>> GetBlogPosts()
        {
            return Task.FromResult(_blogPosts);
        }
    }
}
