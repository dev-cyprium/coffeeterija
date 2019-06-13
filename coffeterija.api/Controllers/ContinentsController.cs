using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.api.Filters;
using coffeterija.application.Commands;
using coffeterija.application.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ContinentsController : Controller
    {
        private IGetContinents ContinentsService { get; set; }

        public ContinentsController(IGetContinents continentsService)
        {
            ContinentsService = continentsService;
        }

        // GET: api/continents
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery] PagedRequest request)
        {
            return Ok(ContinentsService.Execute(request));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
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
