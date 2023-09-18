using System.Globalization;
using System.Runtime.InteropServices;
using ConsoleTables;

namespace csharp_gestore_eventi
{
    internal class Program
    {

        static void Main(string[] args)
        {
            string userChoice;
            Event userEvent;
            UpcomingEvents? userUpcomingEvents = null;

            do
            {
                var menu = new ConsoleTable("                   MENU");

                menu.AddRow("1 - Crea un evento(eccezioni non gestite) e modifica i posti prenotati.");
                menu.AddRow("2 - Crea un programma di eventi e cerca gli eventi per data.");
                menu.AddRow("3 - Crea un programma di eventi e aggiungi una conferenza. (BONUS)");
                if(userUpcomingEvents is not null)
                    menu.AddRow("4 - Stampa l'attuale lista di eventi.");
                menu.AddRow("0 - Esci");

                Console.WriteLine("\n");
                menu.Write();
                Console.WriteLine("\n");

                Console.Write("Inserisci il numero del comando desiderato: ");
                userChoice = Console.ReadLine()?? "";

                switch (userChoice)
                {
                    case "1":
                        userEvent = CreateEventWithBookedSeatsOperations();
                        break;
                    case "2":
                        userUpcomingEvents = CreateUpcomingEventsListAndSearchByDate();
                        break;
                    case "3":
                        userUpcomingEvents = CreateUpcomingEventsListWithConference();
                        break;
                    case "4":
                        if(userUpcomingEvents is not null)
                            Console.WriteLine(userUpcomingEvents);
                        break;
                    case "0":
                        Console.WriteLine("Arrivederci\n");
                        break;
                    default:
                        Console.WriteLine("Comando non riconosciuto\n");
                        break;

                }
            } while (userChoice != "0");

            Console.WriteLine("\n--- Programma terminato ---\n");
        }

        // PROGRAM OPTIONS
        public static Event CreateEventWithBookedSeatsOperations() 
        {
            int userBookedSeats;

            Event userEvent = CreateEvent();

            Console.Write("Quanti posti desideri prenotare? ");
            userBookedSeats = GetValidPositiveIntegerFromUser();

            userEvent.BookSeats(userBookedSeats);

            ModifyBookedSeats();
            
            return userEvent;

            void ModifyBookedSeats()
            {
                PrintBookedSeats();

                string userAnswer = "";
                while (userAnswer != "no" )
                {
                    Console.Write("Vuoi disdire dei posti? (si/no) ");
                    userAnswer = GetValidStringFromUser().ToLower();

                    if (userAnswer == "si")
                    {
                        int userSeatsToCancel;

                        Console.Write("Indica il numero di posti da disdire: ");
                        userSeatsToCancel = GetValidPositiveIntegerFromUser();

                        userEvent.CancelBookedSeats(userSeatsToCancel);
                        Console.WriteLine("Modifica effettuata con successo!");
                        PrintBookedSeats();
                    }
                    else if (userAnswer == "no")
                    {
                        Console.WriteLine("Ok, va bene!");
                        PrintBookedSeats();
                    }
                    else
                        Console.WriteLine("Comando non riconosciuto.");

                }
            }

            void PrintBookedSeats()
            {
                var seatsTable = new ConsoleTable("Numero di posti prenotati", "Numero di posti disponibili");
                seatsTable.AddRow(userEvent.BookedSeats, userEvent.MaxSeatsCapacity - userEvent.BookedSeats);

                Console.WriteLine();
                seatsTable.Write();
                Console.WriteLine();
            }
        }
        public static UpcomingEvents CreateUpcomingEventsListAndSearchByDate()
        {
            UpcomingEvents userUpcomingEvents = CreateUpcomingEventsList();

            PrintUpcomingEvents();

            Console.Write("Inserisci una data per sapere che eventi ci saranno (gg/mm/yyyy): ");
            DateTime userDate = GetValidDateFromUser();
            List<Event> foundEvents = userUpcomingEvents.GetEventsByDate(userDate);

            PrintFoundEvents();

            return userUpcomingEvents;

            
            void PrintUpcomingEvents()
            {
                Console.WriteLine($"\nIl numero di eventi nel programma è: {userUpcomingEvents.CountEvents()} \nEcco il tuo programma eventi:\n{userUpcomingEvents.ToString()}");
            }
            void PrintFoundEvents()
            {
                if (foundEvents.Count > 0)
                    foreach (Event _event in foundEvents)
                        Console.WriteLine(_event.ToString());
                else
                    Console.WriteLine("Nessun evento trovato per la data selezionata.");
            }
        }
        public static UpcomingEvents CreateUpcomingEventsListWithConference()
        {
            UpcomingEvents userUpcomingEvents = CreateUpcomingEventsList();

            Console.WriteLine("\n---- BONUS ----\n\nAggiungiamo anche una conferenza!");
            userUpcomingEvents.AddEvent(CreateConference());

            Console.WriteLine($"\nEcco il tuo programma eventi con conferenza inclusa:\n{userUpcomingEvents}");
            return userUpcomingEvents;
        }
        

        // CLASS CREATION FUNCTIONS
        public static Event CreateEvent()
        {
            Console.WriteLine("\n--- Crea un evento ---\n");

            string? userTitle;
            DateTime userDate;
            int userSeatsCapacity;

            Console.Write("Inserisci il nome dell'evento: ");
            userTitle = GetValidStringFromUser();

            Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");
            userDate = GetValidDateFromUser();

            Console.Write("Inserisci il numero di posti totali: ");
            userSeatsCapacity = GetValidPositiveIntegerFromUser();

            return new Event(userTitle, userDate, userSeatsCapacity);
        }
        public static UpcomingEvents CreateUpcomingEventsList()
        {
            Console.WriteLine("\n--- Crea un nuovo programma di eventi ---\n");

            Console.Write("Inserisci il nome del tuo programma di eventi: ");
            string? userTitle = GetValidStringFromUser();

            UpcomingEvents userUpcomingEvents = new UpcomingEvents(userTitle);

            Console.Write("Indica il numero di eventi da inserire: ");
            int eventsListCount = GetValidPositiveIntegerFromUser();

            while (!IsEventsListFull())
                AddNewEventToList();

            return userUpcomingEvents;

            bool IsEventsListFull()
            {
                return userUpcomingEvents.CountEvents() >= eventsListCount;
            }
            void AddNewEventToList()
            {
                try
                {
                    Event newEvent = CreateEvent();
                    userUpcomingEvents.Events.Add(newEvent);

                }
                catch (Exception error)
                {
                    Console.WriteLine("Operazione non andata a buon fine.");
                    Console.WriteLine(error.Message);
                }
            }
        }
        public static Conference CreateConference()
        {
            string? userTitle;
            DateTime userDate;
            int userSeatsCapacity;
            string? userSpeaker;
            double userPrice;

            Console.WriteLine("\n--- Aggiunta di una conferenza ---\n");

            Console.Write("Inserisci il nome della conferenza: ");
            userTitle = Console.ReadLine();
            while (string.IsNullOrEmpty(userTitle))
            {
                Console.Write("Inserisci un nome valido: ");
                userTitle = Console.ReadLine();
            }

            Console.Write("Inserisci la data della conferenza (gg/mm/yyyy): ");
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out userDate))
            {
                Console.Write("Inserisci un formato di data valido: ");
            }

            Console.Write("Inserisci il numero di posti per la conferenza: ");
            while (!int.TryParse(Console.ReadLine(), out userSeatsCapacity))
            {
                Console.Write("Inserisci un numero valido: ");
            }

            Console.Write("Inserisci il relatore della conferenza: ");
            userSpeaker = Console.ReadLine();
            while (string.IsNullOrEmpty(userSpeaker))
            {
                Console.Write("Inserisci un nome valido: ");
                userSpeaker = Console.ReadLine();
            }

            Console.Write("Inserisci il prezzo della conferenza: ");
            while (!double.TryParse(Console.ReadLine(), out userPrice) || userPrice <= 0)
            {
                Console.Write("Inserisci un valore valido: ");
            }

            return new Conference(userTitle, userDate, userSeatsCapacity, userSpeaker, userPrice);
        }

        // USER INPUT FUNCTIONS
        public static string GetValidStringFromUser()
        {
            string? userInput = Console.ReadLine();
            while (string.IsNullOrEmpty(userInput))
            {
                Console.Write("Inserisci un valore valido: ");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
        public static DateTime GetValidDateFromUser()
        {
            DateTime userInput;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out userInput))
            {
                Console.Write("Inserisci un formato di data valido: ");
            }
            return userInput;
        }
        public static int GetValidPositiveIntegerFromUser()
        {
            int userInput;
            while (!int.TryParse(Console.ReadLine(), out userInput) || userInput <= 0)
            {
                Console.Write("Inserisci un numero positivo valido: ");
            }
            return userInput;
        }

    }
}