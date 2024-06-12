using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuristTravel.Model
{
    public class Ponuda
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Destinacija")]
        public int DestinacijaID { get; set; }
        public virtual Destinacija Destinacija { get; set; }

        [Required]
        public string Naziv { get; set; }
        public string Opis { get; set; }

		[Required]
		public int Cijena { get; set; }

		[Required]
		public DateTime pocetakPutovanja { get; set; }

		[Required]
		public DateTime krajPutovanja { get; set; }
        public virtual ICollection<Korisnik> Korisnici { get; set; }
    }
}
