using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuristTravel.Model
{
    public class Destinacija
    {
        [Key]
        public int ID { get; set; }

        [Required]  
        public string Naziv { get; set; }
        public string Opis { get; set; }

        public virtual ICollection<Ponuda> Ponude { get; set; }
    }
}
