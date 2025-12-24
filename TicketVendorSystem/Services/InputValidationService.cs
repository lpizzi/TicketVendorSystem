using System;
using System.Text.RegularExpressions;

namespace TicketVendorSystem.Services
{
    /// <summary>
    /// Service for validating and parsing user input coordinates
    /// </summary>
    public class InputValidationService
    {
        private const string CoordinatePattern = @"^((-?\d+),(-?\d+))$";
        private readonly Regex _coordinateRegex = new Regex(CoordinatePattern);

        /// <summary>
        /// Validates that the input string matches the coordinate format (e.g., "4,2" or "-5,10")
        /// </summary>
        /// <param name="input">The input string to validate</param>
        /// <returns>True if input is valid coordinate format, false otherwise</returns>
        public bool IsValidCoordinateFormat(string input)
        {
            return !string.IsNullOrWhiteSpace(input) && _coordinateRegex.Match(input).Success;
        }

        /// <summary>
        /// Parses coordinate string into X and Y values
        /// </summary>
        /// <param name="input">Coordinate string in format "X,Y"</param>
        /// <param name="x">Out parameter for X coordinate</param>
        /// <param name="y">Out parameter for Y coordinate</param>
        /// <returns>True if parsing succeeded, false otherwise</returns>
        public bool TryParseCoordinates(string input, out double x, out double y)
        {
            x = 0;
            y = 0;

            if (!IsValidCoordinateFormat(input))
            {
                return false;
            }

            try
            {
                string[] parts = input.Split(',');
                if (parts.Length != 2)
                {
                    return false;
                }

                return double.TryParse(parts[0], out x) && double.TryParse(parts[1], out y);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if coordinates are within the valid world boundaries
        /// </summary>
        /// <param name="x">X coordinate</param>
        /// <param name="y">Y coordinate</param>
        /// <param name="minBound">Minimum boundary value</param>
        /// <param name="maxBound">Maximum boundary value</param>
        /// <returns>True if coordinates are within bounds, false otherwise</returns>
        public bool AreCoordinatesWithinBounds(double x, double y, int minBound, int maxBound)
        {
            return x >= minBound && x <= maxBound && y >= minBound && y <= maxBound;
        }
    }
}
