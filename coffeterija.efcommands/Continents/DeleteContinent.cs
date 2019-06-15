using System;
using System.Linq;
using coffeterija.application.Commands.Continents;
using coffeterija.application.Exceptions;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Continents
{
    public class DeleteContinent : CofeterijaBase, IDeleteContinent
    {
        public DeleteContinent(CoffeeContext context) : base(context)
        {}

        public void Execute(int id)
        {
            var continent = CoffeeContext
                .Continents
                .FirstOrDefault(c => c.Id == id);

            if(continent == null)
            {
                throw new EntityNotFoundException();
            }

            CoffeeContext.Continents.Remove(continent);
            CoffeeContext.SaveChanges();
        }
    }
}
