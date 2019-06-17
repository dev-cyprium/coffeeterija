using System;
namespace coffeterija.application.Exceptions
{
    public class UnsuportedFileTypeException : HttpException
    {
        public UnsuportedFileTypeException(string type)
            : base(400, $"The {type} extension is not supported.") {}
    }
}
