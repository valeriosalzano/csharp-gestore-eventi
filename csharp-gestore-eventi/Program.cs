using System.Globalization;
using System.Runtime.InteropServices;

namespace csharp_gestore_eventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userChoice;
            Event userEvent;
            UpcomingEvents userUpcomingEvents;

            do
            {
                Console.WriteLine(
                    @"
***** Menu *****
1 - Crea un evento (eccezioni non gestite)
2 - Crea un programma di eventi
0 - Esci
****************
                    ");

                Console.Write("Inserisci il numero del comando desiderato: ");
                userChoice = Console.ReadLine()?? "";

                switch (userChoice)
                {
                    case "1":
                        userEvent = CreateEventWithBookedSeats();
                        break;
                    case "2":
                        userUpcomingEvents = CreateUpcomingEventsList();
                        break;
                    case "0":
                        Console.WriteLine("Arrivederci\n");
                        break;
                    default:
                        Console.WriteLine("Comando non riconosciuto\n");
                        break;

                }
            } while (userChoice != "0");
        }

        public static Event CreateEventWithBookedSeats() 
        {
            int userBookedSeats;

            Event userEvent = CreateEvent();

            Console.Write("Quanti posti desideri prenotare? ");
            int.TryParse(Console.ReadLine(), out userBookedSeats);
            userEvent.BookSeats(userBookedSeats);

            bool userCanCancel = true;
            while(userCanCancel)
            {
                Console.Write("Vuoi disdire dei posti? (si/no) ");
                if(Console.ReadLine() == "si")
                {
                    int userSeatsToCancel;

                    Console.Write("Indica il numero di posti da disdire: ");
                    int.TryParse(Console.ReadLine(), out userSeatsToCancel);
                    userEvent.CancelBookedSeats(userSeatsToCancel);

                }else
                {
                    userCanCancel = false;
                    Console.WriteLine("Ok va bene!");
                }
                Console.WriteLine(@$"
Numero di posti prenotati: {userEvent.BookedSeats}
Numero di posti disponibili: {userEvent.MaxSeatsCapacity - userEvent.BookedSeats}
                ");
            }

            return userEvent;
        }

        public static Event CreateEvent()
        {
            string userTitle;
            DateTime userDate;
            int userSeatsCapacity;

            Console.WriteLine("\n--- Aggiunta di un evento ---\n");

            Console.Write("Inserisci il nome dell'evento: ");
            userTitle = Console.ReadLine();
            while(string.IsNullOrEmpty(userTitle))
            {
                Console.Write("Inserisci un titolo valido: ");
                userTitle = Console.ReadLine();
            }

            Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");
            while (!DateTime.TryParseExact(Console.ReadLine(),"dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out userDate))
            {
                Console.Write("Inserisci un formato di data valido: ");
            }

            Console.Write("Inserisci il numero di posti totali: ");
            while(!int.TryParse(Console.ReadLine(), out userSeatsCapacity))
            {
                Console.Write("Inserisci un numero valido: ");
            }
            

            return new Event(userTitle, userDate, userSeatsCapacity);
        }
        public static UpcomingEvents CreateUpcomingEventsList()
        {
            Console.WriteLine("\n--- Aggiunta di un nuovo programma di eventi ---\n");

            string userTitle = GetTitleFromUser();

            UpcomingEvents userUpcomingEvents = new UpcomingEvents(userTitle);

            int eventsListCount = GetListLengthFromUser();

            while(userUpcomingEvents.CountEvents() < eventsListCount)
            {
                try
                {
                    Event newEvent = CreateEvent();
                    userUpcomingEvents.Events.Add(newEvent);

                }catch (Exception error) 
                {
                    Console.WriteLine(error.Message);
                }
            }

            Console.WriteLine($"\nIl numero di eventi nel programma è: {userUpcomingEvents.CountEvents()} \nEcco il tuo programma eventi:\n{userUpcomingEvents.ToString()}");

            DateTime searchedDate;
            Console.Write("Inserisci una data per sapere che eventi ci saranno (gg/mm/yyyy): ");
            while(!DateTime.TryParse(Console.ReadLine(),out searchedDate))
            {
                Console.Write("Inserisci un formato di data valido: ");
            }
            List<Event> eventsList = SearchEventsByDate(searchedDate);
            if(eventsList.Count > 0)
            {
                foreach (Event _event in eventsList)
                {
                    Console.WriteLine(_event.ToString());
                }
            }
            else
            {
                Console.WriteLine("Nessun evento trovato per la data selezionata.");
            }

            return userUpcomingEvents;

            string GetTitleFromUser()
            {
                string userTitle;
                Console.Write("Inserisci il nome del tuo programma Eventi: ");
                userTitle = Console.ReadLine();

                while (string.IsNullOrEmpty(userTitle))
                {
                    Console.Write("Inserisci un nome valido: ");
                    userTitle = Console.ReadLine();
                }
                return userTitle;
            }

            int GetListLengthFromUser()
            {
                int eventsListCount;

                Console.Write("Indica il numero di eventi da inserire: ");

                while (!int.TryParse(Console.ReadLine(), out eventsListCount) || eventsListCount <= 0)
                {
                    Console.Write("Inserisci un valore valido: ");
                }

                return eventsListCount;

            }

            List<Event> SearchEventsByDate(DateTime date)
            {
                List<Event> eventsFound = userUpcomingEvents.GetEventsByDate(date);

                return eventsFound;
            }
        }
    }
}