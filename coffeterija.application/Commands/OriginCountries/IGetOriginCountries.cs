using System;
using coffeterija.application.Pagination;
using coffeterija.application.Requests;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands.OriginCountries
{
    public interface IGetOriginCountries
        : ICommand<OriginCountryFilterDTO, PaginateResponse<OriginCountryResponse>> {}
}
