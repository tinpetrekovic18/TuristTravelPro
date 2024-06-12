using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuristTravel.Model
{
    public class Korisnik
    {
        [Key]
        public int ID { get; set; }

        [Required]  
        public string Ime { get; set; }

        [Required]  
        public string Prezime { get; set; }

        [Required]  
        public string Adresa { get; set; }

        [Required]  
        public string Email { get; set; }
        [Required]  
        public string Password { get; set; }

        public virtual ICollection<Putovanje> Putovanja { get; set; }
    }
}
