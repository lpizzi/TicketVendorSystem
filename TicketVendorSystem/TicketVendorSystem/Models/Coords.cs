using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVendorSystem.Models
{
    public class Coords
    {
        public Coords(Double xAxys, Double yAxys) { this.x = xAxys; this.y = yAxys; }
        public Double x { get; set; }
        public Double y { get; set; }
        //public Event Event { get; set; }

    }
}
