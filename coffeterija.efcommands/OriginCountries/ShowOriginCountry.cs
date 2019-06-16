using System;
using System.Linq;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Exceptions;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.OriginCountries
{
    public class ShowOriginCountry : CofeterijaBase, IShowOriginCountry
    {
        public ShowOriginCountry(CoffeeContext context) : base(context)
        {
        }

        public OriginCountryResponse Execute(int id)
        {
            var country = CoffeeContext
                .OriginCountries
                .Include(oc => oc.Continent)
                .FirstOrDefault(oc => oc.Id == id);

            if (country == null)
            {
                throw new EntityNotFoundException();
            }

            return new OriginCountryResponse()
            {
                Id = country.Id,
                Continent = country.Continent.Name,
                Name = country.Name,
                Area = country.Area
            };
        }
    }
}
