using System;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands.Coffees
{
    public interface IShowCoffee : ICommand<int, CoffeeResponse>
    {}
}
