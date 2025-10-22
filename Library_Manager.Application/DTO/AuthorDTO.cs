namespace Library_Manager.Application.DTO
{
    public class AuthorDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public AuthorDTO() { }
        public AuthorDTO(int id, string name, DateOnly dateOfBirth)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}
