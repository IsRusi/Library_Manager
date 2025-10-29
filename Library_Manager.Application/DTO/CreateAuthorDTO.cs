namespace Library_Manager.Application.DTO
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public CreateAuthorDTO() { }
        public CreateAuthorDTO(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }
    }
}