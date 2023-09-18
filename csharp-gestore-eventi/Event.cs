using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_gestore_eventi
{
    internal class Event
    {
        // ATTRIBUTES
        private string title;
        private DateTime date;

        // PROPERTIES
        public string Title 
        {
            get { return this.title; }
            set { this.title = ValidateTitle(value); }
        }
        public DateTime Date {
            get { return this.date; }
            set { this.date = ValidateDate(value); }
        public int MaxSeatsCapacity { get; }
        public int AvailableSeats { get; }


        // METHODS
        private static string ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new Exception("This field cannot be empty or null.");
            else
                return title;
        }

        private static DateTime ValidateDate(DateTime date)
        {
            return date;
        }
    }
}
