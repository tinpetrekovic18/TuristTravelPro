using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TuristTravel.Web.Models
{
    public class HotelFilterModel
    {
        public string Naziv { get; set; }
        public int? brojZvjezdica { get; set; }
        public string Adresa { get; set; }
        public int? cijenaNocenja { get; set; }
    }
}
