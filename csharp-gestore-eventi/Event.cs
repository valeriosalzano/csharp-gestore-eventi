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
            ValidateSeatsToBook();
            this.BookedSeats += seatsToBook;

            void ValidateSeatsToBook()
            {
                if (seatsToBook <= 0)
                    throw new Exception("Il numero di posti da prenotare deve essere maggiore di zero.");

                DateTime today = DateTime.Today;
                if (today >= this.Date)
                    throw new Exception("Impossibile effettuare prenotazioni, l'evento è già terminato.");

                if (this.BookedSeats + seatsToBook > this.MaxSeatsCapacity)
                    throw new Exception($"Impossibile prenotare più di {this.MaxSeatsCapacity - this.BookedSeats} posti.");
            }
        }

        public void CancelBookedSeats(int seatsToCancel)
        {
            ValidateSeatsToCancel();
            this.BookedSeats -= seatsToCancel;

            void ValidateSeatsToCancel()
            {
                if (seatsToCancel <= 0)
                    throw new Exception("Il numero di prenotazioni deve essere maggiore di zero.");

                DateTime today = DateTime.Today;
                if (today >= this.Date)
                    throw new Exception("Impossibile cancellare le prenotazioni, l'evento è già terminato.");

                if (this.BookedSeats < seatsToCancel)
                    throw new Exception($"Impossibile cancellare più di {this.BookedSeats} prenotazioni.");
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
                throw new Exception("Il campo titolo non può essere vuoto o nullo.");
            else
                this.title = title;

        }

        private void SetDate(DateTime date)
        {
            DateTime today = DateTime.Today;
            if (date <= today)
                throw new Exception($"Le date precedenti al {today.AddDays(1).ToShortDateString()} non sono valide.");
            else
                this.date = date;

        }

        private void SetMaxSeatsCapacity(int seats)
        {
            if (seats <= 0)
                throw new Exception("Il numero di posti disponibili deve essere maggiore di zero.");
            else
                this.maxSeatsCapacity = seats;

        }


    }
}
