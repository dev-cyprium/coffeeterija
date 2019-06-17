using System;
using System.Collections.Generic;

namespace coffeterija
{
    public class Coffee : PrimaryKey
    {
        public string Name { get; set; }
        public ICollection<CoffeePrice> Prices { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
        public ICollection<CoffeeImage> Images { get; set; }
        public OriginCountry Country { get; set; }
    }
}
