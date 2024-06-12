using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TuristTravel.DAL;
using TuristTravel.Model;
using TuristTravel.Web.Models;

namespace TuristTravel.Web.Controllers
{
    public class PonudaController : Controller
    {
        private readonly ClientManagerDbContext _dbContext;

        public PonudaController(ClientManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index(string query = null)
        {
            var ponudaQuery = _dbContext.Ponude.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                ponudaQuery = ponudaQuery.Where(p => p.Naziv.ToLower().Contains(query.ToLower()));

            return View(ponudaQuery.ToList());
        }

        [HttpPost]
        public ActionResult AdvancedSearch(DestinacijaFilterModel filter)
        {
            var ponudaQuery = _dbContext.Destinacije.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Naziv))
                ponudaQuery = ponudaQuery.Where(p => p.Naziv.ToLower().Contains(filter.Naziv.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Opis))
                ponudaQuery = ponudaQuery.Where(p => p.Opis.ToLower().Contains(filter.Opis.ToLower()));


            var model = ponudaQuery.ToList();
            return View("Index", model);
        }

        public IActionResult Create()
        {
			this.FillDropdownValues();
			return View();
        }

        [HttpPost]
        public IActionResult Create(Ponuda model)
        {
            ModelState.Remove("Ponude");

            if (ModelState.IsValid)
            {
                _dbContext.Ponude.Add(model);
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
			this.FillDropdownValues();
			return View(model);
        }

        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Ponude.FirstOrDefault(c => c.ID == id);
			this.FillDropdownValues();
			return View(model);
        }

        [HttpPost]
        [ActionName(nameof(Edit))]
        public async Task<IActionResult> EditPost(int id)
        {


            var ponuda = _dbContext.Ponude.Include(p=>p.Korisnici).Where(d=>d.ID==id).FirstOrDefault();
            if (ponuda == null)
            {
                return NotFound();
            }

            // Remove validation for properties not being edited
          //  ModelState.Remove("Ponude");

            var ok = await TryUpdateModelAsync(ponuda);

            if (ok && ModelState.IsValid)
            {
                _dbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            // Log ModelState errors
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    Console.WriteLine(error.ErrorMessage); // Replace with a proper logging mechanism
                }
            }
			this.FillDropdownValues();
			return View(ponuda);
        }
		private void FillDropdownValues()
		{
			var selectItems = new List<SelectListItem>();

			//Polje je opcionalno
			var listItem = new SelectListItem();
			listItem.Text = "- odaberite -";
			listItem.Value = "";
			selectItems.Add(listItem);

			foreach (var category in _dbContext.Destinacije)
			{
				listItem = new SelectListItem(category.Naziv, category.ID.ToString());
				selectItems.Add(listItem);
			}

			ViewBag.PossibleDestinacije = selectItems;
		}
	}
}
