using System;
namespace coffeterija
{
    public class CoffeeImage : PrimaryKey
    {
        public int CoffeeId { get; set; }
        public string ImagePath { get; set; }
        public Coffee Coffee { get; set; }
    }
}
