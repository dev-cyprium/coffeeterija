using System;
namespace coffeterija
{
    public class Favorites : Datable
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int CoffeeId { get; set; }
        public Coffee Coffee { get; set; }
    }
}
