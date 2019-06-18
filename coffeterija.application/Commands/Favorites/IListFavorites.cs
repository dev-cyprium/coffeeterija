using System;
using coffeterija.application.Pagination;
using coffeterija.application.Requests;
using coffeterija.application.Requests.Favorites;
using coffeterija.application.Responses;

namespace coffeterija.application.Commands.Favorites
{
    public interface IListFavorites : ICommand<ListFavoritesDTO, PaginateResponse<CoffeeResponse>> {}
}
