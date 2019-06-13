using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace coffeterija
{
    public class User : PrimaryKey
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
    }
}
