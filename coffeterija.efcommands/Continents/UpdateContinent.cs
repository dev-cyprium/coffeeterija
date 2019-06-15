using System;
using System.Linq;
using coffeterija.application.Commands.Continents;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Continents
{
    public class UpdateContinent : CofeterijaBase, IUpdateContinent
    {
        public UpdateContinent(CoffeeContext context) : base(context)
        {
        }

        public void Execute(UpdateContinentDTO request)
        {
            Console.WriteLine(request.Id);
            var continent = CoffeeContext
                .Continents
                .FirstOrDefault(cnt => cnt.Id == request.Id);

            if(continent == null)
            {
                throw new EntityNotFoundException();
            }

            continent = Mapper.Map(request, continent);
            CoffeeContext.SaveChanges();
        }
    }
}
