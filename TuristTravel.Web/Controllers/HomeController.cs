using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TuristTravel.Web.Models;

namespace TuristTravel.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("saznaj-vise")]
        [Route("ONama")]
        public IActionResult ONama()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SubmitQuery(IFormCollection formData)
        {
            var ime = formData["ime"];
            var prezime = formData["prezime"];
            var email = formData["email"];
            var poruka = formData["poruka"];
            var tip = formData["tip"];
            var newsletter = bool.Parse(formData["newsletter"].FirstOrDefault());

            var msg = $"Dear {ime} {prezime} ({email}), we have received your message and will get back to you soon. Your message content: [{tip}] {poruka}. Also, you will {(newsletter ? "" : "not ")}be notified of future updates via newsletter.";

            ViewBag.Message = msg;

            return View("ONamaFormaPoslana"); // Make sure to create a ContactSuccess.cshtml view to show the success message.
        }
    }
}
