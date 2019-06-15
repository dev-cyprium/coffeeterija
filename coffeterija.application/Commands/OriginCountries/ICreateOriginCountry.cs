using System;
using coffeterija.application.Requests;

namespace coffeterija.application.Commands.OriginCountries
{
    public interface ICreateOriginCountry : ICommand<NewOriginCountryDTO> {}
}
