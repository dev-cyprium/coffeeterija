﻿using System;
using coffeterija.application.Requests.Favorites;

namespace coffeterija.application.Commands.Favorites
{
    public interface IAddFavorite : ICommand<FavoriteDTO> {}
}
