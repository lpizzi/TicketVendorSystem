/* 

Ticket Vendor System App Test

Scenario
 
- Your program should randomly generate seed data
- Your program should operate in a world that ranges from -10 to +10 (Y axis), and -10 to +10 (X axis)
- Your program should assume that each co-ordinate can hold a maximum of one event
- Each event has a unique numeric identifier (e.g. 1, 2, 3)
- Each event has zero or more tickets 
- Each ticket has a non-zero price, expressed in US Dollars.
- The distance between two points should be computed as the Manhattan distance.
 
Instructions
 
- You are required to write a program which accepts a user location as a pair of co-ordinates, and returns a list of the five closest events, along with the cheapest ticket price for each event
- Please detail any assumptions you have made
- How might you change your program if you needed to support multiple events at the same location?
- How would you change your program if you were working with a much larger world size?
 
Example Program Run
 
Please Input Coordinates:
 
> 4,2
 
Closest Events to (4,2):
 
Event 003 - $30.29, Distance 3
Event 001 - $35.20, Distance 5
Event 006 - $01

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TicketVendorSystem.Models;
using TicketVendorSystem.Common;

namespace TicketVendorSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //var watch1 = System.Diagnostics.Stopwatch.StartNew();
            // the code that you want to measure comes here



            try
            {
                //Map Creation
                List<Coords> worldMap = new List<Coords>();

                //Random generation of seed data 
                worldMap = RandomDataGenerator();

                //watch1.Stop();
                //var elapsedMs = watch1.ElapsedMilliseconds;
                //Console.Write("Total elapsed time: {0}ms\n", elapsedMs);

                /* THIS IS THE POINT IN WHICH STARTS THE USER INTERACTION */

                //Inster user input data 
                string userX = String.Empty;
                string userY = String.Empty;

                //User input validation
                String reg = "^((-?\\d+),(-?\\d+))$";

                Regex inputRegularExpression = new Regex(reg);

                Console.Write("Please Input Coordinates in the following format > 4,2 : ");
                string userXY = Console.ReadLine();

                while (inputRegularExpression.Match(userXY).Success == false)
                {
                    Console.WriteLine("Invalid input, Please insert comma separated values.");
                    Console.Write("Please Input Coordinates in the following format > 4,2 : ");
                    userXY = Console.ReadLine();
                }
                //var watch2 = System.Diagnostics.Stopwatch.StartNew();

                String[] userCoordsArray = userXY.Split(',');

                userX = userCoordsArray[0];
                userY = userCoordsArray[1];

                //User input evaluation with all the points
                Dictionary<Coords, Double> events = new Dictionary<Coords, Double>();
                events = DistanceEvaluation(userX, userY, ref worldMap);

                System.Console.WriteLine("Closest event to: ({0}): ", userXY);
                System.Console.WriteLine();

                //Sorting the events sorting by distance
                var orderedList = events.OrderBy(x => x.Value).ToList();

                //Removing from the view all the Events without available tickets
                orderedList.RemoveAll(x => x.Key.Event.Tickets.Count() == 0);

                //var ticketLowPrices =
                //        from ordered in events
                //        where ordered.Key.Event.Tickets.Count() > 0
                //        orderby ordered.Value ascending
                //        select new { ordered.Key.Event.ID, ordered.Value };

                int processed = 0;

                foreach (var ol in orderedList)
                {
                    if (processed++ == Constants.NumberOfResults)
                    {
                        break;
                    }

                    //Selection of the cheapest event ticket
                    var ticketLowPrice = ol.Key.Event.Tickets.Min(entry => entry.Price);


                    System.Console.WriteLine("Event {0} - Cheapest Ticket: ${1:0.00} at ({2},{3}) distance: {4:0} ",
                        ol.Key.Event.ID,
                        ticketLowPrice,
                        ol.Key.Event.Place.x,
                        ol.Key.Event.Place.y,
                        ol.Value
                    );

                }
                //watch2.Stop();
                //var elapsed2Ms = watch2.ElapsedMilliseconds;
                //Console.Write("Total elapsed time: {0}ms\n", elapsed2Ms);
                System.Console.ReadLine();

            }
            catch (Exception ex)
            {
                Console.WriteLine("It has not been possible to complete the operation.\n" + ex.Message.ToString());
                System.Console.ReadLine();

            }

        }


        /// <summary>
        /// Random String Generation
        /// </summary>
        /// <param name="length">Lenght of the desidered random String</param>
        /// <param name="random">Random object</param>
        /// <returns>A random string generated based on the specified characteristics</returns>
        public static string RandomString(int length, Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

        }

        /// <summary>
        /// Random Value Generation
        /// </summary>
        /// <param name="length">Lenght of the desidered maxinum price</param>
        /// <param name="random">Random object</param>
        /// <returns>A random price generated based on the specified characteristics</returns>
        public static string RandomPrice(int length, Random random)
        {
            const string chars = "0123456789";

            var randomPrice = new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());

            if (randomPrice == "00") randomPrice = "0.99";

            return randomPrice;

        }

        /// <summary>
        /// Random Value Generation
        /// </summary>
        /// <param name="length">Lenght of the desidered string</param>
        /// <param name="value">The possible random value to generate</param>
        /// <param name="random">Random object</param>
        /// <returns>A random value generated based on the specified characteristics</returns>
        public static string RandomValue(int length, string value, Random random)
        {
            var randomValue = new string(Enumerable.Repeat(value, length).Select(s => s[random.Next(s.Length)]).ToArray());

            return randomValue;

        }

        /// <summary>
        /// Utility responsible of the random data generation 
        /// </summary>
        /// <returns>A list of Coordinates object randomly seeded</returns>
        public static List<Coords> RandomDataGenerator()
        {
            var rnd = new Random();

            List<Coords> worldMap = new List<Coords>();

            int eventId = 0;

            List<Ticket> tickets;

            for (int i = -10; i <= 10; i = i + Int32.Parse(RandomValue(1, Constants.RandomIndexSet, rnd)))
            {
                for (int j = -10; j <= 10; j = j + Int32.Parse(RandomValue(1, Constants.RandomIndexSet, rnd)))
                {
                    eventId++;

                    tickets = new List<Ticket>();
                    int numberOfTickets = Int32.Parse(RandomValue(1, Constants.RandomValueSet, rnd));

                    for (int k = 0; k < numberOfTickets; k++)
                    {
                        tickets.Add(new Ticket() { Price = Double.Parse(RandomPrice(2, rnd)) });

                    }

                    worldMap.Add(new Coords(i, j)
                    {
                        Event = new Event
                        {
                            ID = eventId.ToString(),
                            Place = new Coords(i, j),
                            Tickets = tickets
                        }
                    });
                }
            }

            return worldMap;
        }

        /// <summary>
        /// Evaluate the Manhattan distance between a specified point and all the points present in the speficied List
        /// </summary>
        /// <param name="userX">User x parameter </param>
        /// <param name="userY">USer y paramente</param>
        /// <param name="worldMap">The reference of the world Map object</param>
        /// <returns>A Dictionary<Coords, Double> where for each Key presents in the dictionary there is associated a value representing the distance prom the input value</returns>
        public static Dictionary<Coords, Double> DistanceEvaluation(String userX, String userY, ref List<Coords> worldMap)
        {

            Dictionary<Coords, Double> events = new Dictionary<Coords, Double>();

            foreach (var ev in worldMap)
            {
                //Distance evaluating
                Double euclideanDistance = Math.Sqrt(Math.Pow(ev.x - Double.Parse(userX), 2) + Math.Pow(ev.y - Double.Parse(userY), 2));
                Double manhattanDistance = Math.Abs(ev.x - Double.Parse(userX)) + Math.Abs(ev.y - Double.Parse(userY));
                events.Add(ev, manhattanDistance);

            }

            return events;
        }

    }
}
