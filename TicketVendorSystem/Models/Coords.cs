using System;

namespace TicketVendorSystem.Models
{
    /// <summary>
    /// Represents a coordinate point in the world map
    /// </summary>
    public class Coords
    {
        /// <summary>
        /// Initializes a new Coords instance with X and Y coordinates
        /// </summary>
        /// <param name="x">The X axis coordinate</param>
        /// <param name="y">The Y axis coordinate</param>
        public Coords(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Gets or sets the X axis coordinate
        /// </summary>
        public double x { get; set; }

        /// <summary>
        /// Gets or sets the Y axis coordinate
        /// </summary>
        public double y { get; set; }

        /// <summary>
        /// Returns a string representation of the coordinate
        /// </summary>
        public override string ToString()
        {
            return $"({x},{y})";
        }
    }
}
