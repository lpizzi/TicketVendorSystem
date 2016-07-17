using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketVendorSystem.Models;

namespace TicketVendorSystem.Common
{
    class RandomGenerators
    {


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

    }
}
