namespace OrleansBlogPosts.Api.Models
{
    /// <summary>
    /// Blog Post object.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         GenerateFieldIds is set so that the object serializes/deserilizes correctly
    ///     </para>
    /// </remarks>
    [GenerateSerializer(GenerateFieldIds = GenerateFieldIds.PublicProperties)]
    public class BlogPost
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public DateTime? Published { get; set; }
        public ICollection<string> Tags { get; set; } = new HashSet<string>();

        // More properties can be added ...
    }
}
