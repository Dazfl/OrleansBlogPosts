using OrleansBlogPosts.Api.Models;

namespace OrleansBlogPosts.Api.Grains
{
    public interface IBlogPostGrain : IGrainWithIntegerKey
    {
        Task CreateBlogPost(string title, string slug, DateTime published);

        Task AddTags(params List<string> tags);

    }

    public class BlogPostGrain : IBlogPostGrain
    {
        private BlogPost _blogPost;

        public Task CreateBlogPost(string title, string slug, DateTime published)
        {
            _blogPost = new()
            {
                Title = title,
                Slug = slug,
                Published = published
            };

            return Task.CompletedTask;
        }

        public Task AddTags(params List<string> tags)
        {
            if (_blogPost is null || _blogPost.Tags is null)
                return Task.CompletedTask;

            foreach (var tag in tags)
            {
                _blogPost.Tags.Add(tag);
            }

            return Task.CompletedTask;
        }
    }
}
