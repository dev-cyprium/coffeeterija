using coffeterija.api.Filters;
using coffeterija.application;
using coffeterija.application.Commands;
using coffeterija.application.Commands.Continents;
using coffeterija.application.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsController : Controller
    {
        private readonly IDeleteContinent deleteContinentCommand;
        private readonly IShowContinent showContinentCommand;
        private readonly IGetContinents continentsService;
        private readonly IUpdateContinent updateContinentCommand;
        private readonly ICreateContinent createContinentCommand;

        public ContinentsController(
            IGetContinents continentsService,
            IUpdateContinent updateContinentCommand,
            ICreateContinent createContinentCommand,
            IDeleteContinent deleteContinentCommand,
            IShowContinent showContinentCommand)
        {
            this.continentsService = continentsService;
            this.updateContinentCommand = updateContinentCommand;
            this.createContinentCommand = createContinentCommand;
            this.deleteContinentCommand = deleteContinentCommand;
            this.showContinentCommand = showContinentCommand;
        }

        // GET: api/continents
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery] PagedRequest request)
        {
            return Ok(continentsService.Execute(request));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [LoggedIn]
        public IActionResult Get(int id)
        {
            return Ok(showContinentCommand.Execute(id));
        }

        // POST api/values
        [HttpPost]
        [LoggedIn]
        public IActionResult Post([FromBody] NewContinentDTO continent)
        {
            createContinentCommand.Execute(continent);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [LoggedIn]
        public IActionResult Put(int id, [FromBody] UpdateContinentDTO continent)
        {
            continent.Id = id;
            updateContinentCommand.Execute(continent);
            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        [LoggedIn]
        public IActionResult Delete(int id)
        {
            deleteContinentCommand.Execute(id);
            return Ok();
        }
    }
}
