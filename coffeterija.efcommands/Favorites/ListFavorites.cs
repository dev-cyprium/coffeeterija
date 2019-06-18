using System;
using System.Linq;
using coffeterija.application.Commands.Favorites;
using coffeterija.application.Pagination;
using coffeterija.application.Requests;
using coffeterija.application.Requests.Favorites;
using coffeterija.application.Responses;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Favorites
{
    public class ListFavorites : CofeterijaBase, IListFavorites
    {
        public ListFavorites(CoffeeContext context) : base(context) {}

        public PaginateResponse<CoffeeResponse> Execute(ListFavoritesDTO request)
        {
            var query = CoffeeContext.Coffees.AsQueryable();
            CoffeeContext
                .Attach(request.User)
                .Collection(user => user.Favorites)
                .Query()
                .Include(fav => fav.Coffee)
                .ThenInclude(cof => cof.Country)
                .ThenInclude(cou => cou.Continent)
                .Include(fav => fav.Coffee.Images)
                .Include(fav => fav.Coffee.Prices)
                .Load();
            var coffees = request.User.Favorites.Select(fav => fav.Coffee).AsQueryable();

            return coffees.Select(c => new CoffeeResponse()
            {
                Id = c.Id,
                Name = c.Name,
                ContinentName = c.Country.Continent.Name,
                CountryName = c.Country.Name,
                ImageURL = c.Images.First().ImagePath,
                Price = c.Prices
                    .OrderByDescending(p => p.CreatedAt)
                    .First()
                    .Price
            }).Paginate(request.PerPage, request.Page);
        }
    }
}
