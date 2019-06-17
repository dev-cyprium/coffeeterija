using System;
using System.Linq;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Exceptions;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Coffees
{
    public class DeleteCoffee : CofeterijaBase, IDeleteCoffee
    {
        public DeleteCoffee(CoffeeContext context) : base(context)
        {
        }

        public void Execute(int id)
        {
            var coffee = CoffeeContext
                .Coffees
                .Include(c => c.Images)
                .Include(c => c.Prices)
                .FirstOrDefault(cf => cf.Id == id);

            if(coffee == null)
            {
                throw new EntityNotFoundException();
            }

            coffee.Prices.Clear();
            coffee.Images.Clear();
            CoffeeContext.Coffees.Remove(coffee);
            CoffeeContext.SaveChanges();
        }
    }
}
