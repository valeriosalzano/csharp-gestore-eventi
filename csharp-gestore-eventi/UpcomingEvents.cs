using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace csharp_gestore_eventi
{
    internal class UpcomingEvents
    {
        //ATTRIBUTES
        private string title = "";

        // PROPERTIES
        public string Title 
        {
            get { return title; }
            private set { SetTitle(value); }
        }
        public List<Event> Events { get; private set; }

        // CONSTRUCTOR
        public UpcomingEvents(string title)
        {
            Title = title;
            Events = new List<Event>();
        }

        // GETTERS

        public List<Event> GetEventsByDate(DateTime date)
        {
            List<Event> eventsFound = new List<Event>();

            foreach(Event _event in Events) 
            {
                if(_event.Date == date )
                    eventsFound.Add(_event);
            }

            return eventsFound;
        }

        // SETTERS

        private void SetTitle(string title)
        {
            if (string.IsNullOrEmpty(title))
                throw new Exception("Il titolo non può essere vuoto.");
            else
                this.title = title;

        }

        // METHODS
        public void AddEvent(Event newEvent)
        {
            this.Events.Add(newEvent);
            this.Events.Sort((x, y) => DateTime.Compare(x.Date, y.Date));
        }

        public static string ListToString(List<Event> events)
        {
            string result = "";

            foreach( Event _event in events)
            {
                result += _event.ToString()+"\n";
            }

            return result;
        }

        public int CountEvents()
        {
            return Events.Count();
        }

        public void ClearEventsList()
        {
            Events.Clear();
        }

        public override string ToString()
        {
            string result = Title + "\n";
            foreach( Event _event in Events)
            {
                result += "\t" +_event.ToString()+"\n";
            }

            return result;
        }
    }
}
