using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Coffees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeesController : Controller
    {
        private readonly List<string> AllowedFileTypes;

        public CoffeesController(IConfiguration configuration)
        {
            AllowedFileTypes = configuration.GetSection("AllowedFileUploadTypes")
                .AsEnumerable()
                .Where(p => p.Value != null)
                .Select(p => p.Value)
                .ToList();
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
        public void Post([FromForm] NewCoffeeDTO coffee)
        {
            string ext = Path.GetExtension(coffee.Image.FileName);

            if(!AllowedFileTypes.Contains(ext))
            {
                throw new UnsuportedFileTypeException(ext);
            }

            int totalMill = (int)(new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
            var fileName = totalMill + "_" + coffee.Image.FileName;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
            coffee.Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
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
