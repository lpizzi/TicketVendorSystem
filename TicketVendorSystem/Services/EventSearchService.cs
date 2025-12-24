using System;
using System.Collections.Generic;
using System.Linq;
using TicketVendorSystem.Models;

namespace TicketVendorSystem.Services
{
    /// <summary>
    /// Service for searching and filtering events based on user location
    /// </summary>
    public class EventSearchService
    {
        /// <summary>
        /// Calculates Manhattan distance between two points
        /// </summary>
        /// <param name="x1">First point X coordinate</param>
        /// <param name="y1">First point Y coordinate</param>
        /// <param name="x2">Second point X coordinate</param>
        /// <param name="y2">Second point Y coordinate</param>
        /// <returns>Manhattan distance between the two points</returns>
        public static double CalculateManhattanDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }

        /// <summary>
        /// Finds the closest events to a given user location
        /// </summary>
        /// <param name="userX">User X coordinate</param>
        /// <param name="userY">User Y coordinate</param>
        /// <param name="worldMap">The world map containing all events</param>
        /// <param name="numberOfResults">Maximum number of results to return (default: 5)</param>
        /// <returns>List of EventSearchResult ordered by distance, containing only events with available tickets</returns>
        /// <remarks>
        /// ASSUMPTIONS:
        /// - Manhattan distance is used for distance calculations (not Euclidean)
        /// - Events without available tickets are filtered out from results
        /// - User coordinates are expected to be within the world bounds (-10 to +10)
        /// - Results are ordered by ascending distance from user location
        /// </remarks>
        public static List<EventSearchResult> FindClosestEvents(double userX, double userY, Map worldMap, int numberOfResults)
        {
            if (worldMap?.World == null || worldMap.World.Count == 0)
            {
                return new List<EventSearchResult>();
            }

            // Calculate distances for all events
            var eventsWithDistance = new Dictionary<Event, double>();
            
            foreach (var eventEntry in worldMap.World)
            {
                double distance = CalculateManhattanDistance(
                    userX, 
                    userY, 
                    eventEntry.Key.x, 
                    eventEntry.Key.y
                );
                eventsWithDistance.Add(eventEntry.Value, distance);
            }

            // Filter events that have tickets, calculate cheapest price, and create results
            var results = eventsWithDistance
                .Where(e => e.Key.Tickets.Count > 0)  // Only events with available tickets
                .OrderBy(e => e.Value)                // Sort by distance ascending
                .Take(numberOfResults)                // Take only the required number
                .Select(e => new EventSearchResult(
                    e.Key,
                    e.Value,
                    e.Key.Tickets.Min(t => t.Price)
                ))
                .ToList();

            return results;
        }
    }
}
