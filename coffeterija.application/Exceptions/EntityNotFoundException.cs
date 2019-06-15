using System;
namespace coffeterija.application.Exceptions
{
    public class EntityNotFoundException : HttpException
    {
        public EntityNotFoundException()
            : base (404, "The entety is not found")
        {}
    }
}
