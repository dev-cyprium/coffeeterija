using System;
using System.Linq;
using coffeterija.application.Commands.Favorites;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Favorites;
using coffeterija.dataaccess;
using Microsoft.EntityFrameworkCore;

namespace coffeterija.efcommands.Favorites
{
    public class RemoveFavorite : CofeterijaBase, IRemoveFavorite
    {
        public RemoveFavorite(CoffeeContext context) : base(context) {}

        public void Execute(FavoriteDTO request)
        {
            var user = CoffeeContext
                .Users
                .Include(u => u.Favorites)
                .First(u => u.Id == request.UserId);

            var favorite = user
                .Favorites
                .FirstOrDefault(fav => fav.CoffeeId == request.CoffeeId);

            if(favorite == null)
            {
                throw new EntityNotFoundException();
            }

            user.Favorites.Remove(favorite);
            CoffeeContext.SaveChanges();
        }
    }
}
