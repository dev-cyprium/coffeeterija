using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija
{
    public class OriginCountry : PrimaryKey
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Area { get; set; }
        public Continent Continent { get; set; }
    }
}
