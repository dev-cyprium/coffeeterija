using System;
using coffeterija.dataaccess;

namespace coffeterija.efcommands
{
    public class CofeterijaBase
    {
        protected CoffeeContext CoffeeContext { get; private set; }

        public CofeterijaBase(CoffeeContext context) => CoffeeContext = context;
    }
}
