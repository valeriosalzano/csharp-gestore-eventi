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
        private int maxSeatsCapacity;

        // PROPERTIES
        public string Title 
        {
            get { return this.title; }
            set { 
                ValidateTitle(value);
                this.title = value;
            }
        }
        public DateTime Date
        {
            get { return this.date; }
            set 
            { 
                ValidateDate(value);
                this.date = value;
            }
        }
        public int MaxSeatsCapacity {
            get { return this.maxSeatsCapacity; }
            private set 
            { 
                ValidateMaxSeatsCapacity(value);
                this.maxSeatsCapacity = value;
            }
        }
        public int ReservedSeats { get; private set; }

        // CONSTRUCTOR

        public Event(string title, DateTime date, int maxSeatsCapacity)
        {
            this.Title = title;
            this.Date = date;
            this.MaxSeatsCapacity = maxSeatsCapacity;
            this.ReservedSeats = 0;
        }

        // METHODS
        private static void ValidateTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("This field cannot be empty or null.","Title");

        }

        private static void ValidateDate(DateTime date)
        {
            DateTime today = DateTime.Today;
            if (date <= today)
                throw new ArgumentException($"Dates before {today.AddDays(1)} are not valid.", "Date");

        }

        private static void ValidateMaxSeatsCapacity(int seats)
        {
            if (seats <= 0)
                throw new ArgumentException("This field must be greater than zero", "MaxSeatsCapacity");

        }
    }
}
