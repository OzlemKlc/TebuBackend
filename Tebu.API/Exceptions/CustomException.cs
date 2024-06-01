namespace Tebu.API.Exceptions
{
    public class CustomException : Exception
    {
        public int HttpStatusCode { get; set; }

        public CustomException(string message, int httpStatusCode) : base(message) 
        { 
            HttpStatusCode = httpStatusCode;
        }

    }
}
