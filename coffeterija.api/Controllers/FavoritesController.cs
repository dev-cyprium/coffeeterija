﻿using System;
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
        private readonly ILoginService loginService;

        public FavoritesController(
            IAddFavorite addCommand,
            ILoginService loginService
            )
        {
            this.addCommand = addCommand;
            this.loginService = loginService;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        [LoggedIn]
        public IActionResult Post([FromBody] FavoriteDTO request)
        {
            request.UserId = loginService.MaybeGetUser().Id;
            addCommand.Execute(request);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
