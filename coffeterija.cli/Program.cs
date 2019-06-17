using System;
using System.Collections.Generic;
using System.Linq;
using coffeterija.dataaccess;

namespace coffeterija.cli
{

    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new CoffeeContext();

            var prices = ctx.Coffees
                .Select(cof => cof.GetActivePrice());

            foreach(var price in prices)
            {
                Console.WriteLine(price);
            }
        }
    }
}
