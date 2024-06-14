using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TuristTravel.Model
{
    public class Putovanje
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Korisnik")]
        public int KorisnikID { get; set; }
        public virtual Korisnik Korisnik { get; set; }


        [Required]
        [ForeignKey("Ponuda")]
        public int PonudaID { get; set; }
        public Ponuda Ponuda { get; set; }

        public string Status { get; set; }

        
    }
}
