using System;
using coffeterija.application;
using coffeterija.application.Requests;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Continents
{
    public class CreateContinent : CofeterijaBase, ICreateContinent
    {
        public CreateContinent(CoffeeContext context) : base(context)
        {}

        public void Execute(NewContinentDTO request)
        {
            Continent continent = Mapper.Map<Continent>(request);
            CoffeeContext.Continents.Add(continent);
            CoffeeContext.SaveChanges();
        }
    }
}
