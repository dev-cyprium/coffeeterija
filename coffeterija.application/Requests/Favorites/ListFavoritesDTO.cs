using System;
namespace coffeterija.application.Requests.Favorites
{
    public class ListFavoritesDTO : PagedRequest
    {
        public User User { get; set; }
    }
}
