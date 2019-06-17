using System;
using System.Collections.Generic;
using System.Linq;

namespace coffeterija
{
    public class Coffee : PrimaryKey
    {
        public string Name { get; set; }
        public ICollection<CoffeePrice> Prices { get; set; }
        public ICollection<Favorites> Favorites { get; set; }
        public ICollection<CoffeeImage> Images { get; set; }
        public OriginCountry Country { get; set; }

        public CoffeePrice GetActivePrice()
        {
            return Prices.OrderByDescending(p => p.CreatedAt).First();
        }
    }
}
