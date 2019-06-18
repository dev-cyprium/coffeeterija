using System;
using System.Linq;
using coffeterija.application.Commands.Favorites;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Favorites;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Favorites
{
    public class AddFavorite : CofeterijaBase, IAddFavorite
    {
        public AddFavorite(CoffeeContext context) : base(context)
        {
        }

        public void Execute(FavoriteDTO request)
        {
            var coffee = CoffeeContext
                .Coffees
                .Include(c => c.Favorites)
                .FirstOrDefault(c => c.Id == request.CoffeeId);

            if(coffee == null)
            {
                throw new EntityNotFoundException();
            }

            var fav = new coffeterija.Favorites()
            {
                 Coffee = coffee,
                 UserId = request.UserId
            };

            coffee.Favorites.Add(fav);
            CoffeeContext.SaveChanges();
        }
    }
}
