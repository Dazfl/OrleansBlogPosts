namespace OrleansBlogPosts.Api.Models
{
    public record BlogPost
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime? Published { get; set; }
        public ICollection<string> Tags { get; set; } = [];
    }
}
