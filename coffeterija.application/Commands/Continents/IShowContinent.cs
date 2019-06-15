using System;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands.Continents
{
    public interface IShowContinent : ICommand<int, ContinentResponse> {}
}
