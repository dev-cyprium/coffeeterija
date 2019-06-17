using System;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Requests.Coffees;
using coffeterija.dataaccess;

namespace coffeterija.efcommands.Coffees
{
    public class CreateCoffee : CofeterijaBase, ICreateCoffee
    {
        public CreateCoffee(CoffeeContext context) : base(context)
        {
        }

        public void Execute(NewCoffeeDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
