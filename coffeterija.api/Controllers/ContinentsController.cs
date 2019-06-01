using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.application.Requests;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api.Controllers
{
    [Route("api/[controller]")]
    public class ContinentsController : Controller
    {
        // GET: api/continents
        [HttpGet]
        public IEnumerable<int> Get(PagedRequest request)
        {
            var test = new PagedRequest();

            return new int[] { request.Page, request.PerPage };
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
