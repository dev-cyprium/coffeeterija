using System;
using coffeterija.application.Requests.Coffees;

namespace coffeterija.application.Commands.Coffees
{
    public interface IUpdateCoffee : ICommand<UpdateCoffeeDTO> {}
}
