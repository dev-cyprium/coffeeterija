using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using coffeterija.api.Filters;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Exceptions;
using coffeterija.application.Requests.Coffees;
using coffeterija.dataaccess;
using Microsoft.AspNetCore.Http;
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
        private readonly IUpdateCoffee updateCommand;

        public CoffeesController(
            IConfiguration configuration,
            ICreateCoffee createCommand,
            IDeleteCoffee deleteCommand,
            IGetCoffee listCommand,
            IShowCoffee showCommand,
            IUpdateCoffee updateCommand
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
            this.updateCommand = updateCommand;
        }

        // GET: api/values
        [HttpGet]
        [LoggedIn]
        public IActionResult Get([FromQuery] CoffeeFilterDTO request)
        {
            return Ok(listCommand.Execute(request));
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
        public IActionResult Post([FromForm] NewCoffeeDTO coffee)
        {
            coffee.ImagePath = HandleNewImage(coffee.Image);
            createCommand.Execute(coffee);
            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [LoggedIn]
        public IActionResult Put(int id, [FromForm] UpdateCoffeeDTO request)
        {
            if(request.Image != null)
            {
                request.ImagePath = HandleNewImage(request.Image);
            }

            request.Id = id;
            updateCommand.Execute(request);
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

        private string HandleNewImage(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);

            if (!AllowedFileTypes.Contains(ext))
            {
                throw new UnsuportedFileTypeException(ext);
            }

            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", fileName);
            file.CopyTo(new FileStream(uploadPath, FileMode.Create));
            return $"{HttpContext.Request.Host}/Images/{fileName}";
            
        }
    }
}
