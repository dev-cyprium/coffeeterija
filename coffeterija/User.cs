using System;
using System.Collections.Generic;

namespace coffeterija
{
    public class User : PrimaryKey
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
    }
}
