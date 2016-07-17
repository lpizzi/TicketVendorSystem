using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVendorSystem.Models
{
    class Event
    {
        public String ID { get; set; }

        //public String Name { get; set; }
        public Coords Place { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
