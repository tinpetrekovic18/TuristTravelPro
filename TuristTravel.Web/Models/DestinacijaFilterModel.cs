using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TuristTravel.Web.Models
{
    public class DestinacijaFilterModel
    {
       
        public string Naziv { get; set; }
        public string Opis { get; set; }
    }
}
