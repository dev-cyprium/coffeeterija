using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.api.Filters;
using coffeterija.application.Commands.OriginCountries;
using coffeterija.application.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OriginCountriesController : Controller
    {
        private readonly ICreateOriginCountry createCommand;
        private readonly IDeleteOriginCountry deleteCommand;
        private readonly IGetOriginCountries getCommand;
        private readonly IShowOriginCountry showCommand;
        private readonly IUpdateOriginCountry updateCommand;

        public OriginCountriesController(
            ICreateOriginCountry createCommand,
            IDeleteOriginCountry deleteCommand,
            IGetOriginCountries getCommand,
            IShowOriginCountry showCommand,
            IUpdateOriginCountry updateCommand
            )
        {
            this.createCommand = createCommand;
            this.deleteCommand = deleteCommand;
            this.getCommand = getCommand;
            this.showCommand = showCommand;
            this.updateCommand = updateCommand;
        }

        // GET: api/values
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery] OriginCountryFilterDTO request)
        {
            return Ok(getCommand.Execute(request));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [LoggedIn]
        public IActionResult Get(int id)
        {
            return Ok(showCommand.Execute(id));
        }

        // POST api/values
        [HttpPost]
        [LoggedIn]
        public IActionResult Post([FromBody] NewOriginCountryDTO originCountry)
        {
            createCommand.Execute(originCountry);
            return Ok(); 
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [LoggedIn]
        public IActionResult Put(int id, [FromBody] UpdateOriginCountryDTO country)
        {
            country.Id = id;
            updateCommand.Execute(country);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [LoggedIn]
        public IActionResult Delete(int id)
        {
            deleteCommand.Execute(id);
            return Ok();
        }
    }
}
