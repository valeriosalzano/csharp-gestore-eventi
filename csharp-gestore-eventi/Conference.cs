using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csharp_gestore_eventi
{
    internal class Conference : Event
    {
        //ATTRIBUTES
        private string speaker = "";
        private double price;

        // PROPERTIES
        public string Speaker 
        {
            get { return this.speaker; }
            set { SetSpeaker(value); }
        }
        public double Price
        {
            get { return this.price; }
            set { SetPrice(value); }
        }

        // CONSTRUCTOR
        public Conference(string title, DateTime date, int maxSeatsCapacity, string speaker ,double price) : base(title, date, maxSeatsCapacity)
        {
            base.Title = title;
            base.Date = date;
            base.MaxSeatsCapacity = maxSeatsCapacity;
            this.Speaker = speaker;
            this.Price = price;
        }

        // SETTERS
        private void SetSpeaker(string speaker)
        {
            if (string.IsNullOrEmpty(speaker))
                throw new Exception("Il campo relatore non può essere vuoto o nullo.");
            else
                this.speaker = speaker;

        }
        private void SetPrice(double price)
        {
            if (price <= 0)
                throw new Exception("Il prezzo deve essere maggiore di zero.");
            else
                this.price = price;

        }

        // METHODS
        public string GetFormattedDate()
        {
            return this.Date.ToLongDateString();
        }

        public string GetFormattedPrice()
        {
            return this.Price.ToString("0.00");
        }

        public override string ToString()
        {
            return $"{GetFormattedDate()} - {this.Title} - {this.Speaker} - {GetFormattedPrice}" ;
        }
    }
}
