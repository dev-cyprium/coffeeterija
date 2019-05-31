using System;
using coffeterija.dataaccess;

namespace coffeterija.cli
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CoffeeContext();
            var continent = new Continent()
            {
                Name = "Test"
            };

            ctx.Add(continent);
        }
    }
}
