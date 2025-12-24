using System;
using System.Collections.Generic;
using TicketVendorSystem.Common;

namespace TicketVendorSystem.Models
{
    /// <summary>
    /// Singleton class representing the world map containing all events
    /// </summary>
    public class Map
    {
        private Map() { }

        private static Map uniqueInstance;
        public Dictionary<Coords, Event> World { get; set; }

        /// <summary>
        /// Gets the singleton instance of the Map
        /// </summary>
        /// <returns>The single Map instance</returns>
        public static Map GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new Map();
            }

            return uniqueInstance;
        }

        /// <summary>
        /// Generates random seed data populating the world with events and tickets
        /// </summary>
        public void RandomDataGenerator()
        {
            if (World != null)
            {
                return; // Already initialized
            }

            World = new Dictionary<Coords, Event>();
            var random = new Random();
            int eventId = 0;

            // Generate events at random coordinates within world bounds
            for (int i = Constants.WorldMinCoordinate; i <= Constants.WorldMaxCoordinate; 
                i += int.Parse(RandomGenerators.RandomValue(1, Constants.RandomIndexSet, random)))
            {
                for (int j = Constants.WorldMinCoordinate; j <= Constants.WorldMaxCoordinate; 
                    j += int.Parse(RandomGenerators.RandomValue(1, Constants.RandomIndexSet, random)))
                {
                    eventId++;

                    // Generate random tickets for this event
                    var tickets = new List<Ticket>();
                    int numberOfTickets = int.Parse(RandomGenerators.RandomValue(1, Constants.RandomValueSet, random));

                    for (int k = 0; k < numberOfTickets; k++)
                    {
                        tickets.Add(new Ticket 
                        { 
                            Price = double.Parse(RandomGenerators.RandomPrice(Constants.RandomTicketPriceDecimalPlaces, random)) 
                        });
                    }

                    // Create and add event to world
                    var @event = new Event
                    {
                        ID = eventId.ToString(),
                        Place = new Coords(i, j),
                        Tickets = tickets
                    };

                    World.Add(new Coords(i, j), @event);
                }
            }
        }
    }
}
