namespace Library_Manager.Application.Exceptions
{
    public class InvalidBookIdException : Exception
    {
        public InvalidBookIdException(string message) : base(message) { }
    }
}
