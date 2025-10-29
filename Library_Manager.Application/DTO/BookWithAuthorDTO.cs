namespace Library_Manager.Application.DTO
{
    public class BookWithAuthorDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public string AuthorName { get; set; } = string.Empty;

    }
}
