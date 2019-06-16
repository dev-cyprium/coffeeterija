using System;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Requests;
using coffeterija.dataaccess;
using coffeterija.efcommands.Continents;

namespace coffeterija.efcommands.OriginCountries
{
    public class CreateOriginCountry : CofeterijaBase, ICreateOriginCountry
    {
        public CreateOriginCountry(CoffeeContext context) : base(context)
        {}

        public void Execute(NewOriginCountryDTO request)
        {
            OriginCountry country = Mapper.Map<OriginCountry>(request);
            CoffeeContext.OriginCountries.Add(country);
            CoffeeContext.SaveChanges();
        }
    }
}
