using System;
using System.Collections.Generic;

namespace TicketVendorSystem.Models
{
    /// <summary>
    /// Represents an event at a specific location in the world
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets the unique identifier for this event
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the coordinate location of this event
        /// </summary>
        public Coords Place { get; set; }

        /// <summary>
        /// Gets or sets the list of tickets available for this event
        /// </summary>
        public List<Ticket> Tickets { get; set; }
    }
}
