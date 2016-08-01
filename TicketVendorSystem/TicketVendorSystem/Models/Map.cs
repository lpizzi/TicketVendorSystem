using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVendorSystem.Common;

namespace TicketVendorSystem.Models
{
    public class Map
    {
        private Map() { }

        private static Map uniqueInstance;
        public Dictionary<Coords, Event> World { get; set; }
        public static Map GetInstance()
        {
            if (uniqueInstance == null) uniqueInstance = new Map();

            return uniqueInstance;
        }

        public void RandomDataGenerator()
        {
            var rnd = new Random();

            if (World == null)
            {
                World = new Dictionary<Coords, Event>();

                int eventId = 0;

                List<Ticket> tickets;

                for (int i = -10; i <= 10; i = i + Int32.Parse(RandomGenerators.RandomValue(1, Constants.RandomIndexSet, rnd)))
                {
                    for (int j = -10; j <= 10; j = j + Int32.Parse(RandomGenerators.RandomValue(1, Constants.RandomIndexSet, rnd)))
                    {
                        eventId++;

                        tickets = new List<Ticket>();

                        int numberOfTickets = Int32.Parse(RandomGenerators.RandomValue(1, Constants.RandomValueSet, rnd));

                        for (int k = 0; k < numberOfTickets; k++)
                        {
                            tickets.Add(new Ticket() { Price = Double.Parse(RandomGenerators.RandomPrice(2, rnd)) });

                        }

                        var thisevent = new Event();
                        thisevent.ID = eventId.ToString();
                        thisevent.Place = new Coords(i, j);
                        thisevent.Tickets = tickets;

                        World.Add(new Coords(i, j), thisevent);
                    }
                }

            }
        }

    }
}
