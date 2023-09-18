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
            set { SetTitle(value); }
        }
        public DateTime Date
        {
            get { return this.date; }
            set { SetDate(value); }
        }
        public int MaxSeatsCapacity {
            get { return this.maxSeatsCapacity; }
            private set { SetMaxSeatsCapacity(value); }
        }
        public int BookedSeats { get; private set; }

        // CONSTRUCTOR

        public Event(string title, DateTime date, int maxSeatsCapacity)
        {
            this.Title = title;
            this.Date = date;
            this.MaxSeatsCapacity = maxSeatsCapacity;
            this.BookedSeats = 0;
        }

        // METHODS

        public void BookSeats(int seatsToBook)
        {
            ValidateSeatsToBook(seatsToBook);
            this.BookedSeats += seatsToBook;

            void ValidateSeatsToBook(int seatsToBook)
            {
                if (seatsToBook <= 0)
                    throw new ArgumentException("Field must be greater than zero", "seatsToBook");

                DateTime today = DateTime.Today;
                if (today >= this.Date)
                    throw new Exception("Cannot book seats, event is already ended");

                if (this.BookedSeats + seatsToBook > this.MaxSeatsCapacity)
                    throw new Exception($"Cannot book more than {this.MaxSeatsCapacity - this.BookedSeats} seats.");
            }
        }

        public void CancelBookedSeats(int seatsToCancel)
        {
            ValidateSeatsToCancel(seatsToCancel);
            this.BookedSeats -= seatsToCancel;

            void ValidateSeatsToCancel(int seatsToCancel)
            {
                if (seatsToCancel <= 0)
                    throw new ArgumentException("Field must be greater than zero", "seatsToCancel");

                DateTime today = DateTime.Today;
                if (today >= this.Date)
                    throw new Exception("Cannot cancel seats, event is already ended");

                if (this.BookedSeats < seatsToCancel)
                    throw new Exception($"Cannot cancel more than {this.BookedSeats} seats.");
            }
        }

        public override string ToString()
        {
            return $"{this.Date.ToString("dd/MM/yyyy")} - {this.Title}";
        }

        // SETTERS
        private void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new ArgumentException("This field cannot be empty or null.", "Title");
            else
                this.title = title;

        }

        private void SetDate(DateTime date)
        {
            DateTime today = DateTime.Today;
            if (date <= today)
                throw new ArgumentException($"Dates before {today.AddDays(1)} are not valid.", "Date");
            else
                this.date = date;

        }

        private void SetMaxSeatsCapacity(int seats)
        {
            if (seats <= 0)
                throw new ArgumentException("This field must be greater than zero", "MaxSeatsCapacity");
            else
                this.maxSeatsCapacity = seats;

        }


    }
}
