using System;
namespace coffeterija.application.Exceptions
{
    public class UniqueFieldException : HttpException
    {
        public UniqueFieldException(string field) : base(400, $"Field {field} already exists in database.")
        {}
    }
}
