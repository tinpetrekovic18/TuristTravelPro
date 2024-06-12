using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TuristTravel.DAL;
using TuristTravel.Model;
using TuristTravel.Web.Models;
using System.Linq;

namespace TuristTravel.Web.Controllers
{
    public class HotelController : Controller
    {
        private readonly ClientManagerDbContext _dbContext;

        public HotelController(ClientManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string query = null)
        {
            var hotelQuery = _dbContext.Hoteli.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                hotelQuery = hotelQuery.Where(p => p.Naziv.ToLower().Contains(query.ToLower()));

            return View(hotelQuery.ToList());
        }

        [HttpPost]
        public ActionResult AdvancedSearch(HotelFilterModel filter)
        {
            var hotelQuery = _dbContext.Hoteli.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Naziv))
                hotelQuery = hotelQuery.Where(p => p.Naziv.ToLower().Contains(filter.Naziv.ToLower()));

            if (filter.brojZvjezdica.HasValue)
                hotelQuery = hotelQuery.Where(p => p.brojZvjezdica == filter.brojZvjezdica.Value);

            if (!string.IsNullOrWhiteSpace(filter.Adresa))
                hotelQuery = hotelQuery.Where(p => p.Adresa.ToLower().Contains(filter.Adresa.ToLower()));

            if (filter.cijenaNocenja.HasValue)
                hotelQuery = hotelQuery.Where(p => p.cijenaNocenja == filter.cijenaNocenja.Value);

            var model = hotelQuery.ToList();
            return View("Index", model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Hotel model)
        {
            ModelState.Remove("Putovanja");

            if (ModelState.IsValid)
            {
                _dbContext.Hoteli.Add(model);
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
			var model = _dbContext.Hoteli.FirstOrDefault(c => c.ID == id);
			return View(model);
		}

		[HttpPost]
		[ActionName(nameof(Edit))]
		public async Task<IActionResult> EditPost(int id)
		{


            var client = _dbContext.Hoteli.Include(p => p.Putovanja).Where(cl => cl.ID == id).FirstOrDefault();
			var ok = await this.TryUpdateModelAsync(client);

            

            if (ok && this.ModelState.IsValid)
			{
				_dbContext.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			return View();
		}
	}
}
