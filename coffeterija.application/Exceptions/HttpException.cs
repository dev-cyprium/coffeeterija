using System;
namespace coffeterija.application.Exceptions
{
    public class HttpException : Exception
    {
        public int HttpStatus { get; private set; }
        public string HttpMessage { get; private set; }

        public HttpException(int httpStatus, string message = "Something went wrong")
            : base(message)
        {
            HttpStatus = httpStatus;
            HttpMessage = message;
        }
    }
}
