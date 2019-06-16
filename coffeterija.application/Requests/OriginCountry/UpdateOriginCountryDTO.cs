using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class UpdateOriginCountryDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Range(10.0, double.MaxValue)]
        public decimal? Area { get; set; }
    }
}
