using System;
using System.Linq;
using coffeterija.application.Commands.Continents;
using coffeterija.application.Exceptions;
using coffeterija.application.Responses;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Continents
{
    public class ShowContinent : CofeterijaBase, IShowContinent
    {
        public ShowContinent(CoffeeContext context) : base(context)
        {
        }

        public ContinentResponse Execute(int id)
        {
            var continent = CoffeeContext.Continents.FirstOrDefault(c => c.Id == id);

            if(continent == null)
            {
                throw new EntityNotFoundException();
            }

            return new ContinentResponse()
            {
                Id = continent.Id,
                Name = continent.Name
            };
        }
    }
}
