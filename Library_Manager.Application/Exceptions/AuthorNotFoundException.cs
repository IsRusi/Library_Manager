namespace Library_Manager.Application.Exceptions
{
    public class AuthorNotFoundException : Exception
    {
        public AuthorNotFoundException(string message) : base(message) { }
    }
}
