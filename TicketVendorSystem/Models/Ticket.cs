using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketVendorSystem.Models
{
    /// <summary>
    /// Represents a ticket available for purchase at an event
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// Gets or sets the price of this ticket in US Dollars
        /// </summary>
        public double Price { get; set; }
    }
}
