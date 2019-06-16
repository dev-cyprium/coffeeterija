using System;
using System.Linq;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Exceptions;
using coffeterija.application.Responses;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.OriginCountries
{
    public class ShowOriginCountry : CofeterijaBase, IShowOriginCountry
    {
        public ShowOriginCountry(CoffeeContext context) : base(context)
        {
        }

        public OriginCountryResponse Execute(int id)
        {
            var country = CoffeeContext.OriginCountries.FirstOrDefault(oc => oc.Id == id);

            if (country == null)
            {
                throw new EntityNotFoundException();
            }

            return new OriginCountryResponse()
            {
                Id = country.Id,
                Continent = country.Continent.Name,
                Name = country.Name
            };
        }
    }
}
