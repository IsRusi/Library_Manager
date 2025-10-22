namespace Library_Manager.Domain.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth{ get; set; }
    }
}
