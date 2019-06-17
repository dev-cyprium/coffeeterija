using System;
using coffeterija.application.Pagination;
using coffeterija.application.Requests.Coffees;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands.Coffees
{
    public interface IGetCoffee : ICommand<CoffeeFilterDTO, PaginateResponse<CoffeeResponse>> {}
}
