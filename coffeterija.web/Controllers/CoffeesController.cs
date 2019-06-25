using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using coffeterija.application.Commands.Coffees;
using coffeterija.application.Requests.Coffees;
using coffeterija.dataaccess;
using Microsoft.AspNetCore.Mvc.Rendering;
using coffeterija.application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace coffeterija.web.Controllers
{
    public class CoffeesController : Controller
    {
        private readonly IGetCoffee getCommand;
        private readonly IShowCoffee showCommand;
        private readonly ICreateCoffee createCommand;
        private readonly IUpdateCoffee updateCommand;
        private readonly IDeleteCoffee deleteCommand;
        private readonly CoffeeContext context;

        public CoffeesController(
            IGetCoffee getCommand,
            IShowCoffee showCommand,
            ICreateCoffee createCommand,
            IUpdateCoffee updateCommand,
            IDeleteCoffee deleteCommand,
            CoffeeContext context)
        {
            this.getCommand = getCommand;
            this.showCommand = showCommand;
            this.createCommand = createCommand;
            this.updateCommand = updateCommand;
            this.deleteCommand = deleteCommand;
            this.context = context;
        }

        public IActionResult Edit(int id)
        {
            LoadData();
            try
            {
                var coffee = showCommand.Execute(id);
                var coffeeDTO = new UpdateCoffeeDTO()
                {
                    Id = coffee.Id,
                    Name = coffee.Name,
                    Price = coffee.Price
                };
                return View(coffeeDTO);
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [FromForm] UpdateCoffeeDTO request)
        {
            try
            {
                if (request.Image != null)
                {
                    request.ImagePath = HandleNewImage(request.Image);
                }

                request.Id = id;
                updateCommand.Execute(request);
                return RedirectToAction(nameof(Index));
            } catch (Exception)
            {
                TempData["error"] = "Something went wrong when submitting the form";
                LoadData();
                return View();
            }
        }

        public IActionResult Create()
        {
            LoadData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm] NewCoffeeDTO request)
        {
            try
            {
                request.ImagePath = HandleNewImage(request.Image);
                createCommand.Execute(request);
                return RedirectToAction(nameof(Index));
            } catch(Exception)
            {
                TempData["error"] = "Something went wrong when submitting the form";
                LoadData();
                return View();
            }
        }

        // GET: /<controller>/
        public IActionResult Index(CoffeeFilterDTO request)
        {
            var result = getCommand.Execute(request);
            return View(result);
        }

        public IActionResult Details(int id)
        {
            try
            {
                var coffee = showCommand.Execute(id);
                return View(coffee);
            } catch (Exception)
            {
                return View();
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                deleteCommand.Execute(id);
            } catch(Exception)
            {
                TempData["error"] = "Doslo je do greske"; 
            }

            return RedirectToAction(nameof(Index));
        }

        private void LoadData()
        {
            ViewBag.Countries = new SelectList(context.OriginCountries.ToList(), "Id", "Name");

        }

        private string HandleNewImage(IFormFile file)
        {
            string ext = Path.GetExtension(file.FileName);

            string[] niz = { ".jpg", ".gif", ".jpeg", ".png", ".webp" };
            List<string> lista = new List<string>(niz);


            if (!lista.Contains(ext))
            {
                throw new UnsuportedFileTypeException(ext);
            }

            var fileName = Guid.NewGuid().ToString() + "_" + file.FileName;

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            var uploadPath = Path.Combine(path, "coffeterija.api", "wwwroot", "Images", fileName);
            file.CopyTo(new FileStream(uploadPath, FileMode.Create));
            return $"localhost:5000/Images/{fileName}";
        }
    }
}
