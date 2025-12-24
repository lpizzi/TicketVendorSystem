/* 

TICKET VENDOR SYSTEM - EVENT FINDER

SCENARIO
- Your program should randomly generate seed data
- Your program should operate in a world that ranges from -10 to +10 (Y axis), and -10 to +10 (X axis)
- Your program should assume that each co-ordinate can hold a maximum of one event
- Each event has a unique numeric identifier (e.g. 1, 2, 3)
- Each event has zero or more tickets 
- Each ticket has a non-zero price, expressed in US Dollars.
- The distance between two points should be computed as the Manhattan distance.

INSTRUCTIONS
- You are required to write a program which accepts a user location as a pair of co-ordinates, and returns a list of the five closest events, along with the cheapest ticket price for each event
- Please detail any assumptions you have made
- How might you change your program if you needed to support multiple events at the same location?
- How would you change your program if you were working with a much larger world size?

ASSUMPTIONS MADE:
1. DISTANCE METRIC: Manhattan distance is used instead of Euclidean distance. This measures distance as the sum of absolute differences in X and Y coordinates.
   
2. TICKET FILTERING: Events without available tickets are excluded from the results. This provides better UX by only showing viable options.
   
3. COORDINATE BOUNDS: User input coordinates are expected to be within the world bounds (-10 to +10). Input validation checks this.
   
4. RESULT ORDERING: Results are ordered by ascending distance from the user's location.
   
5. SINGLETON MAP: The Map uses a singleton pattern to ensure only one world instance exists throughout the application lifecycle.

6. UNIQUE COORDINATES: Each coordinate can hold only one event, enforced by using Dictionary<Coords, Event> as the data structure.

MULTI-LOCATION SUPPORT:
To support multiple events at the same location, change:
- Current: Dictionary<Coords, Event> World
- To: Dictionary<Coords, List<Event>> World
- This would require updating RandomDataGenerator() and the search logic accordingly.

LARGER WORLD SIZE OPTIMIZATION:
For much larger worlds (e.g., -1000 to +1000), consider:
1. SPATIAL INDEXING: Implement a quadtree or grid-based partitioning to avoid O(n) full scans
2. LAZY LOADING: Load only relevant map sections into memory
3. CACHING: Cache distance calculations for frequently queried locations
4. BATCH PROCESSING: Process multiple user queries together
Current approach is O(n) where n = total number of events. For large n, this becomes inefficient.

EXAMPLE PROGRAM RUN:
Please Input Coordinates:
> 4,2

Closest Events to (4,2):
Event 003 - Cheapest Ticket: $30.29 at (5,2) distance: 1
Event 001 - Cheapest Ticket: $35.20 at (4,5) distance: 3
Event 006 - Cheapest Ticket: $12.50 at (2,3) distance: 3

*/

using System;
using System.Collections.Generic;
using TicketVendorSystem.Models;
using TicketVendorSystem.Services;
using TicketVendorSystem.Common;

namespace TicketVendorSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Initialize the world map with random seed data
                Map worldMap = Map.GetInstance();
                worldMap.RandomDataGenerator();

                // Get user input and find closest events
                EventSearchResult[] closestEvents = GetClosestEventsFromUserInput(worldMap);

                // Display results
                DisplayResults(closestEvents);

                System.Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                System.Console.ReadLine();
            }
        }

        /// <summary>
        /// Prompts user for coordinates and returns the closest events
        /// </summary>
        /// <param name="worldMap">The world map containing all events</param>
        /// <returns>Array of EventSearchResult objects representing closest events</returns>
        private static EventSearchResult[] GetClosestEventsFromUserInput(Map worldMap)
        {
            var validationService = new InputValidationService();
            double userX, userY;

            // Prompt user for input with validation loop
            while (true)
            {
                Console.Write("Please Input Coordinates in format X,Y (e.g., 4,2): ");
                string userInput = Console.ReadLine();

                // Validate format
                if (!validationService.IsValidCoordinateFormat(userInput))
                {
                    Console.WriteLine("Invalid format. Please use comma-separated values (e.g., 4,2).");
                    continue;
                }

                // Parse coordinates
                if (!validationService.TryParseCoordinates(userInput, out userX, out userY))
                {
                    Console.WriteLine("Failed to parse coordinates.");
                    continue;
                }

                // Validate bounds
                if (!validationService.AreCoordinatesWithinBounds(userX, userY, 
                    Constants.WorldMinCoordinate, Constants.WorldMaxCoordinate))
                {
                    Console.WriteLine(
                        $"Coordinates must be within bounds ({Constants.WorldMinCoordinate} to {Constants.WorldMaxCoordinate}).");
                    continue;
                }

                break;
            }

            // Find and return closest events
            List<EventSearchResult> results = EventSearchService.FindClosestEvents(
                userX, userY, worldMap, Constants.NumberOfResults);

            return results.ToArray();
        }

        /// <summary>
        /// Displays the search results in a formatted manner
        /// </summary>
        /// <param name="results">Array of EventSearchResult to display</param>
        private static void DisplayResults(EventSearchResult[] results)
        {
            if (results.Length == 0)
            {
                Console.WriteLine("No events found with available tickets.");
                return;
            }

            Console.WriteLine();
            Console.WriteLine("Closest Events:");
            Console.WriteLine(new string('-', 70));

            foreach (var result in results)
            {
                Console.WriteLine(
                    "Event {0} - Cheapest Ticket: ${1:0.00} at ({2},{3}) distance: {4:0}",
                    result.Event.ID,
                    result.CheapestTicketPrice,
                    result.Event.Place.x,
                    result.Event.Place.y,
                    result.Distance
                );
            }

            Console.WriteLine(new string('-', 70));
        }
    }
}
