using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TuristTravel.Model;

namespace TuristTravel.Web.Models
{
    public class PonudaFilterModel
    {
        public  Destinacija Destinacija { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public int Cijena { get; set; }

        public Hotel Hotel { get; set; }    
        public DateTime pocetakPutovanja { get; set; }
        public DateTime krajPutovanja { get; set; }
    }
}
