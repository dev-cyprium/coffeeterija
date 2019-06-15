using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class NewOriginCountryDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [Range(100.0, double.MaxValue)]
        public decimal Area { get; set; }
    }
}
