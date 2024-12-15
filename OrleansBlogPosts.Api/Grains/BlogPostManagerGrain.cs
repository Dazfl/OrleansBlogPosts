namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostManagerGrain : IGrainWithStringKey
    {
        Task AddBlogPost(int blogPostId);
        Task RemoveBlogPost(int blogPostId);
        Task<List<int>> GetBlogPosts();
    }

    public class BlogPostManagerGrain : IBlogPostManagerGrain
    {
        private List<int> _blogPosts = [];

        public Task AddBlogPost(int blogPostId)
        {
            var blogPostCount = _blogPosts.Count;
            _blogPosts.Add(blogPostId);
            return Task.CompletedTask;
        }

        public Task<List<int>> GetBlogPosts()
        {
            return Task.FromResult(_blogPosts);
        }

        public Task RemoveBlogPost(int blogPostId)
        {
            _blogPosts.Remove(blogPostId);
            return Task.CompletedTask;
        }
    }
}
