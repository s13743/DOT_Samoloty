using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System;

namespace Samoloty.Models
{
    public class Aircraft
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public int PaxCapacity { get; set; }
        public DateTime Bought { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }
    }

    public class AircraftDBCtxt : DbContext
    {
        public DbSet<Aircraft> Aircrafts { get; set; }
        public DbSet<Flight> Flights { get; set; }
    }
}