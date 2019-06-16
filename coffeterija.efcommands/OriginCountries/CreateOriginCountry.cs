using System;
using System.Linq;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Exceptions;
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
            Continent continent = CoffeeContext.Continents.FirstOrDefault(c => c.Id == request.ContinentId);
            country.Continent = continent ?? throw new EntityNotFoundException();
            CoffeeContext.OriginCountries.Add(country);
            CoffeeContext.SaveChanges();
        }
    }
}
