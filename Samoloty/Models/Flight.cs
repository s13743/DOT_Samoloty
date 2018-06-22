using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samoloty.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string dep { get; set; }
        public string dest { get; set; }
        public int AircraftID { get; set; }

        public virtual Aircraft Aircraft { get; set; }
    }
}