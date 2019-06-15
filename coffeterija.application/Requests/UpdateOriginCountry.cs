using System;
using System.ComponentModel.DataAnnotations;

namespace coffeterija.application.Requests
{
    public class UpdateOriginCountry
    {
        public string Name { get; set; }

        [Range(100.0, double.MaxValue)]
        public decimal Area { get; set; }
    }
}
