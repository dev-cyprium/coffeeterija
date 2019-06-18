using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.api.Filters;
using coffeterija.api.Services;
using coffeterija.application.Commands.Favorites;
using coffeterija.application.Requests.Favorites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        private readonly IAddFavorite addCommand;
        private readonly IListFavorites listCommand;
        private readonly IRemoveFavorite removeCommand;
        private readonly ILoginService loginService;

        public FavoritesController(
            IAddFavorite addCommand,
            IListFavorites listCommand,
            IRemoveFavorite removeCommand,
            ILoginService loginService
            )
        {
            this.addCommand = addCommand;
            this.listCommand = listCommand;
            this.removeCommand = removeCommand;
            this.loginService = loginService;
        }

        // GET: api/values
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery] ListFavoritesDTO request)
        {
            request.User = loginService.MaybeGetUser();
            return Ok(listCommand.Execute(request));
        }

        // POST api/favorites
        [HttpPost]
        [LoggedIn]
        public IActionResult Post([FromBody] FavoriteDTO request)
        {
            request.UserId = loginService.MaybeGetUser().Id;
            addCommand.Execute(request);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [LoggedIn]
        public IActionResult Delete([FromBody] FavoriteDTO request)
        {
            request.UserId = loginService.MaybeGetUser().Id;
            removeCommand.Execute(request);
            return Ok();
        }
    }
}
