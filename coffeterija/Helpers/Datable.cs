using System;
namespace coffeterija
{
    public class Datable : SoftDeletable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
