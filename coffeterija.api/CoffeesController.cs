using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Coffees;
using coffeterija.dataaccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoffeesController : Controller
    {
        private readonly List<string> AllowedFileTypes;
        private readonly ICreateCoffee createCommand;
        private readonly IDeleteCoffee deleteCommand;
        private readonly IGetCoffee listCommand;
        private readonly IShowCoffee showCommand;

        public CoffeesController(
            IConfiguration configuration,
            ICreateCoffee createCommand,
            IDeleteCoffee deleteCommand,
            IGetCoffee listCommand,
            IShowCoffee showCommand
            )
        {
            AllowedFileTypes = configuration.GetSection("AllowedFileUploadTypes")
                .AsEnumerable()
                .Where(p => p.Value != null)
                .Select(p => p.Value)
                .ToList();
            this.createCommand = createCommand;
            this.deleteCommand = deleteCommand;
            this.listCommand = listCommand;
            this.showCommand = showCommand;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get([FromQuery] CoffeeFilterDTO request)
        {
            return Ok(listCommand.Execute(request));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return Ok(showCommand.Execute(id));
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromForm] NewCoffeeDTO coffee)
        { 
            string ext = Path.GetExtension(coffee.Image.FileName);

            if (!AllowedFileTypes.Contains(ext))
            {
                throw new UnsuportedFileTypeException(ext);
            }

            var fileName = Guid.NewGuid().ToString() + "_" + coffee.Image.FileName;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
            coffee.ImagePath = $"{HttpContext.Request.Host}/Images/{fileName}";
            coffee.Image.CopyTo(new FileStream(uploadPath, FileMode.Create));
            createCommand.Execute(coffee);
            
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            deleteCommand.Execute(id);
            return Ok();
        }
    }
}
