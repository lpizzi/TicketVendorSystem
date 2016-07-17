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
                Map worldMap = new Map();
                
                //Random generation of seed data 
                worldMap.RandomDataGenerator();

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
                Dictionary<KeyValuePair<Coords, Event>, Double> events = new Dictionary<KeyValuePair<Coords, Event>, Double>();
                events = DistanceEvaluation(userX, userY, ref worldMap);

                System.Console.WriteLine("Closest event to: ({0}): ", userXY);
                System.Console.WriteLine();

                //Sorting the events sorting by distance
                var orderedList = events.OrderBy(x => x.Value).ToList();

                //Removing from the view all the Events without available tickets
                orderedList.RemoveAll(x => x.Key.Value.Tickets.Count() == 0);

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
                    var ticketLowPrice = ol.Key.Value.Tickets.Min(entry => entry.Price);


                    System.Console.WriteLine("Event {0} - Cheapest Ticket: ${1:0.00} at ({2},{3}) distance: {4:0} ",
                        ol.Key.Value.ID,
                        ticketLowPrice,
                        ol.Key.Value.Place.x,
                        ol.Key.Value.Place.y,
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
        /// Evaluate the Manhattan distance between a specified point and all the points present in the speficied List
        /// </summary>
        /// <param name="userX">User x parameter </param>
        /// <param name="userY">USer y paramente</param>
        /// <param name="worldMap">The reference of the world Map object</param>
        /// <returns>A Dictionary<Coords, Double> where for each Key presents in the dictionary there is associated a value representing the distance prom the input value</returns>
        public static Dictionary<KeyValuePair<Coords, Event>, Double> DistanceEvaluation(String userX, String userY, ref Map worldMap)
        {

            Dictionary<KeyValuePair<Coords, Event>, Double> events = new Dictionary<KeyValuePair<Coords, Event>, Double>();

            foreach (var ev in worldMap.World)
            {
                //Distance evaluating
                //Double euclideanDistance = Math.Sqrt(Math.Pow(ev.Key.x - Double.Parse(userX), 2) + Math.Pow(ev.Key.y - Double.Parse(userY), 2));
                Double manhattanDistance = Math.Abs(ev.Key.x - Double.Parse(userX)) + Math.Abs(ev.Key.y - Double.Parse(userY));
                events.Add(ev, manhattanDistance);

            }

            return events;
        }

    }
}
