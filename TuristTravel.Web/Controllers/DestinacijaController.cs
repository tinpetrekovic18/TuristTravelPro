using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristTravel.DAL;
using TuristTravel.Model;
using TuristTravel.Web.Models;

namespace TuristTravel.Web.Controllers
{
    public class DestinacijaController : Controller
    {
        private readonly ClientManagerDbContext _dbContext;

        public DestinacijaController(ClientManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string query = null)
        {
            var destinacijaQuery = _dbContext.Destinacije.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                destinacijaQuery = destinacijaQuery.Where(p => p.Naziv.ToLower().Contains(query.ToLower()));

            return View(destinacijaQuery.ToList());
        }

        [HttpPost]
        public ActionResult AdvancedSearch(DestinacijaFilterModel filter)
        {
            var destinacijaQuery = _dbContext.Destinacije.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Naziv))
                destinacijaQuery = destinacijaQuery.Where(p => p.Naziv.ToLower().Contains(filter.Naziv.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Opis))
                destinacijaQuery = destinacijaQuery.Where(p => p.Opis.ToLower().Contains(filter.Opis.ToLower()));


            var model = destinacijaQuery.ToList();
            return View("Index", model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Destinacija model)
        {
            ModelState.Remove("Ponude");

            if (ModelState.IsValid)
            {
                _dbContext.Destinacije.Add(model);
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Log ModelState errors
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            return View(model);
        }

        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Destinacije.FirstOrDefault(c => c.ID == id);
            return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {

            
            ModelState.Remove("Ponude");
            var destinacija = _dbContext.Destinacije.Include(p=>p.Ponude).Where(d=>d.ID==id).FirstOrDefault();
            ModelState.Remove("Ponude");
            if (destinacija == null)
            {
                return NotFound();
            }

            

            var ok = await TryUpdateModelAsync(destinacija);

            if (ok && ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage); 
                }
            }

            return View(destinacija);
        }
    }
}
