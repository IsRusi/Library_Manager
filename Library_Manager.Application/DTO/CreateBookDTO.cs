namespace Library_Manager.Application.DTO
{
        public class CreateBookDTO
        {
            public string Title { get; set; } = string.Empty;
            public int PublishedYear { get; set; }
            public int AuthorId { get; set; }
        }
    }

