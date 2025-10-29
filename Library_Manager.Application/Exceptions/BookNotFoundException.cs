namespace Library_Manager.Application.Exceptions
{
    public class BookNotFoundException:Exception
    {
        public BookNotFoundException(string message) : base(message) { }
    }
}
