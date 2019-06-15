using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija
{
    public class Continent : PrimaryKey
    {
        [Required]
        public string Name { get; set; }
    }
}
