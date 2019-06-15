using System;
using coffeterija.application.Pagination;
using coffeterija.application.Requests;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands
{

    public interface IGetContinents
        : ICommand<PagedRequest, PaginateResponse<ContinentResponse>> {} 
}
