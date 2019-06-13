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

            var continents = ctx.Continents.AsQueryable();
        }
    }
}
