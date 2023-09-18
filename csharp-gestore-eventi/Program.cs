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
1 - Crea un evento
2 - Crea un programma di eventi
0 - Esci
                    ");
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
                        Console.WriteLine("Arrivederci");
                        break;
                    default:
                        Console.WriteLine("Comando non riconosciuto");
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

            Console.WriteLine("--- Aggiunta di un evento ---");

            Console.Write("Inserisci il nome dell'evento: ");
            userTitle = Console.ReadLine() ?? "";

            Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");
            userDate = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Inserisci il numero di posti totali: ");
            int.TryParse(Console.ReadLine(), out userSeatsCapacity);

            return new Event(userTitle, userDate, userSeatsCapacity);
        }
        public static UpcomingEvents CreateUpcomingEventsList()
        {

            string userTitle = GetTitleFromUser();

            UpcomingEvents userUpcomingEvents = new UpcomingEvents(userTitle);

            int eventsListCount = GetListLengthFromUser();

            for(int i = 0; i < eventsListCount; i++)
            {

            }

            string GetTitleFromUser()
            {
                string userTitle;
                Console.Write("Inserisci il nome del tuo programma Eventi: ");
                userTitle = Console.ReadLine();

                while (string.IsNullOrEmpty(userTitle))
                {
                    Console.Write("Inserisci un nome valido.");
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
        }
    }
}