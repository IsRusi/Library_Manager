namespace Library_Manager.Application.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int PublishedYear { get; set; }
        public int AuthorId { get; set; }

        public BookDTO()
        { }

        public BookDTO(int id, string title, int publishedYear, int authorId)
        {
            Id = id;
            Title = title;
            PublishedYear = publishedYear;
            AuthorId = authorId;
        }
    }
}