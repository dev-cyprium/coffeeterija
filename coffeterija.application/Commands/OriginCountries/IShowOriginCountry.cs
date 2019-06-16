using System;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands.OriginCountries
{
    public interface IShowOriginCountry : ICommand<int, OriginCountryResponse> {}
}
