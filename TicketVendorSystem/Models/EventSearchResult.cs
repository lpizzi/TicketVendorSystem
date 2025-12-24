using System;

namespace TicketVendorSystem.Models
{
    /// <summary>
    /// Encapsulates the result of searching for closest events
    /// </summary>
    public class EventSearchResult
    {
        public Event Event { get; set; }
        public double Distance { get; set; }
        public double CheapestTicketPrice { get; set; }

        public EventSearchResult(Event @event, double distance, double cheapestTicketPrice)
        {
            Event = @event;
            Distance = distance;
            CheapestTicketPrice = cheapestTicketPrice;
        }
    }
}
