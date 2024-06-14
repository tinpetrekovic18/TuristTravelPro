using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuristTravel.Model;

namespace TuristTravel.DAL
{
    public class ClientManagerDbContext : DbContext
    {

        public ClientManagerDbContext(DbContextOptions<ClientManagerDbContext> options) : base(options){ }

        public DbSet<Destinacija> Destinacije { get; set; }
        public DbSet<Hotel> Hoteli { get; set; }
        public DbSet<Korisnik> Korisnici { get; set; }
        public DbSet<Ponuda> Ponude { get; set; }
        public DbSet<Putovanje> Putovanja { get; set; }

		

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Destinacija>().HasData(new Destinacija { ID = 1, Naziv = "Zagreb", Opis="Very nice" });
			modelBuilder.Entity<Destinacija>().HasData(new Destinacija { ID = 2, Naziv = "Pula", Opis = "Sehr gut" });
			modelBuilder.Entity<Destinacija>().HasData(new Destinacija { ID = 3, Naziv = "Dubrovnik", Opis = "Mucho gusto" });

			modelBuilder.Entity<Hotel>().HasData(new Hotel { ID = 1, Naziv = "Sheraton",brojZvjezdica=4, Adresa="Zagreb", cijenaNocenja=100 });

            modelBuilder.Entity<Korisnik>().HasData(new Korisnik { ID = 1, Ime="Tin",Prezime="Petreković", Adresa="Pustodol Začretski 49a", Email="tin.petrekovic@gmail.com", Password="lozinka123" });

        }
    }
}
