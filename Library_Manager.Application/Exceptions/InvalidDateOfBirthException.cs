namespace Library_Manager.Application.Exceptions
{
    public class InvalidDateOfBirthException : Exception
    {
        public InvalidDateOfBirthException(string message) : base(message) { }
    }
}
