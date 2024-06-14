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
            var ponudaQuery  = _dbContext.Ponude
                                        .Include(p => p.Destinacija)
                                        .Include(p => p.Hotel)
                                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
                ponudaQuery = ponudaQuery.Where(p => p.Naziv.ToLower().Contains(query.ToLower()));

            return View(ponudaQuery.ToList());
        }

        [HttpPost]
        [Route("IndexFilter")]
        public IActionResult IndexFilter(PonudaFilterModel? filter = null)
        {
            filter ??= new PonudaFilterModel();

            var clientQuery = _dbContext.Ponude
                                        .Include(p => p.Destinacija)
                                        .Include(p => p.Hotel)
                                        .AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Destinacija?.Naziv))
                clientQuery = clientQuery.Where(p => p.Destinacija.Naziv.ToLower().Contains(filter.Destinacija.Naziv.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Naziv))
                clientQuery = clientQuery.Where(p => p.Naziv.ToLower().Contains(filter.Naziv.ToLower()));

            if (!string.IsNullOrWhiteSpace(filter.Opis))
                clientQuery = clientQuery.Where(p => p.Opis.ToLower().Contains(filter.Opis.ToLower()));

            if (filter.Cijena != null)
                clientQuery = clientQuery.Where(p => p.Cijena == filter.Cijena);

            if (!string.IsNullOrWhiteSpace(filter.Hotel?.Naziv))
                clientQuery = clientQuery.Where(p => p.Hotel.Naziv.ToLower().Contains(filter.Hotel.Naziv.ToLower()));

            if (filter.pocetakPutovanja != null)
                clientQuery = clientQuery.Where(p => p.pocetakPutovanja == filter.pocetakPutovanja);

            if (filter.krajPutovanja != null)
                clientQuery = clientQuery.Where(p => p.krajPutovanja == filter.krajPutovanja);

            var model = clientQuery.ToList();

            return PartialView("IndexTable", model);
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
            this.FillDropdownValues2();
			return View();
        }

        [HttpPost]
        public IActionResult Create(Ponuda model)
        {

			ModelState.Remove("Hotel");
			ModelState.Remove("Destinacija");

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
			this.FillDropdownValues2();
			return View(model);
        }

        [ActionName(nameof(Edit))]
        public IActionResult Edit(int id)
        {
            var model = _dbContext.Ponude.FirstOrDefault(c => c.ID == id);
			this.FillDropdownValues();
			this.FillDropdownValues2();
			return View(model);
        }

        [HttpPost]
[ActionName(nameof(Edit))]
public async Task<IActionResult> EditPost(int id)
{

            var ponuda = await _dbContext.Ponude.Include(p => p.Destinacija).Include(p => p.Hotel).FirstOrDefaultAsync(p => p.ID == id);

            if (ponuda == null)
            {
                return NotFound();
            }

            ModelState.Remove("Hotel");
            ModelState.Remove("Destinacija");

            var ok = await TryUpdateModelAsync(ponuda);

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

            this.FillDropdownValues();
            this.FillDropdownValues2();
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

		private void FillDropdownValues2()
		{
			var selectItems = new List<SelectListItem>();

			//Polje je opcionalno
			var listItem = new SelectListItem();
			listItem.Text = "- odaberite -";
			listItem.Value = "";
			selectItems.Add(listItem);

			foreach (var category in _dbContext.Hoteli)
			{
				listItem = new SelectListItem(category.Naziv, category.ID.ToString());
				selectItems.Add(listItem);
			}

			ViewBag.PossibleHoteli = selectItems;
		}
	}
}
