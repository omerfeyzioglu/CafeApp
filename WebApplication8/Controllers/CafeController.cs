using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication8.Data;
using WebApplication8.Models;
using WebApplication8.Models.Domain;

namespace WebApplication8.Controllers
{
    public class CafeController : Controller
    {
        private readonly MyDbContext myDbContext;

        public CafeController(MyDbContext myDbContext)
        {
            this.myDbContext = myDbContext;
        }
        [HttpGet]

        public async Task<IActionResult> Index() { 
        
         var cafes =  await myDbContext.Cafes.ToListAsync();
          return View(cafes);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["WelcomeMessage"] = "Cafe Properties";

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCafeViewModel addCafeRequest)
          
          {
            var cafe = new Cafe()
            {
                Id = Guid.NewGuid(),
                Name = addCafeRequest.Name,
                Description = addCafeRequest.Description,
                City = addCafeRequest.City,
                Email = addCafeRequest.Email,
                detailedAdress = addCafeRequest.detailedAdress,
                imageUrl = addCafeRequest.imageUrl
            };
             
            await myDbContext.Cafes.AddAsync(cafe);
            await myDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
          }

        [HttpGet]
        public async Task<ActionResult> View(Guid id )
        {
            ViewBag.WelcomeMessage = "Edit Cafe";

            var cafe = await myDbContext.Cafes.FirstOrDefaultAsync(x => x.Id == id);

            if (cafe != null) {
                var viewModel = new UpdateCafeViewModel()
                {
                    Id = cafe.Id,
                    Name = cafe.Name,
                    Description = cafe.Description,
                    City = cafe.City,
                    Email = cafe.Email,
                    detailedAdress = cafe.detailedAdress,
                    imageUrl = cafe.imageUrl
                };

                return await Task.Run(() => View("View",viewModel));
            }
            
            return RedirectToAction("Index");        
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateCafeViewModel model)
        {
            var cafe = await myDbContext.Cafes.FindAsync(model.Id);

            if (cafe != null)
            {
                cafe.Name = model.Name;
                cafe.Description = model.Name;
                cafe.City = model.City;
                cafe.Email = model.Email;
                cafe.detailedAdress = model.detailedAdress;
                cafe.imageUrl = model.imageUrl;

                await myDbContext.SaveChangesAsync();

                return RedirectToAction("Index");

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCafeViewModel model) 
        
        {
           var cafe = await myDbContext.Cafes.FindAsync(model.Id);

            if(cafe != null)
            {
                myDbContext.Cafes.Remove(cafe);

                await myDbContext.SaveChangesAsync();


                return RedirectToAction("Index");

            }

            return RedirectToAction("Index");
        }
       
    }
}
