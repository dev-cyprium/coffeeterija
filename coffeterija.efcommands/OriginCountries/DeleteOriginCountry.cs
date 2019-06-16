using System;
using System.Linq;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Exceptions;
using coffeterija.dataaccess;
using coffeterija.efcommands.Continents;

namespace coffeterija.efcommands.OriginCountries
{
    public class DeleteOriginCountry : CofeterijaBase, IDeleteOriginCountry
    {
        public DeleteOriginCountry(CoffeeContext context) : base(context) {}

        public void Execute(int id)
        {
            var country = CoffeeContext
                .OriginCountries
                .FirstOrDefault(c => c.Id == id);

            if (country == null)
            {
                throw new EntityNotFoundException();
            }

            CoffeeContext.OriginCountries.Remove(country);
            CoffeeContext.SaveChanges();
        }
    }
}
