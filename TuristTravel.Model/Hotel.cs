using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace TuristTravel.Model
{
    public class Hotel
    {
        [Key]
        public int ID { get; set; }

		[Required(ErrorMessage = "Naziv is required")]
		public string Naziv { get; set; }

        [Range(1, 5, ErrorMessage = "Hotel mora imati između 1 i 5 zvjezdica")]
        public int brojZvjezdica { get; set; }

		[Required(ErrorMessage = "Adresa is required")]
		public string Adresa { get; set; }

		[Range(1, int.MaxValue, ErrorMessage = "Cijena noćenja must be a positive number")]
		public int cijenaNocenja { get; set; }
        public virtual ICollection<Putovanje> Putovanja { get; set; }
    }
}
