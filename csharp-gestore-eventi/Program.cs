using System.Runtime.InteropServices;

namespace csharp_gestore_eventi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string userChoice;
            Event userEvent;

            do
            {
                Console.WriteLine(
                    @"
***** Menu *****
1 - Crea un evento
0 - Esci
                    ");
                userChoice = Console.ReadLine()?? "";

                switch (userChoice)
                {
                    case "1":
                        userEvent = CreateEvent();
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

        public static Event CreateEvent() 
        {
            string userTitle;
            DateTime userDate;
            int userSeatsCapacity;
            Event userEvent;
            int userBookedSeats;

            Console.WriteLine("--- Aggiunta di un evento ---");

            Console.Write("Inserisci il nome dell'evento: ");
            userTitle = Console.ReadLine() ?? "";

            Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");
            userDate = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Inserisci il numero di posti totali: ");
            int.TryParse(Console.ReadLine(),out userSeatsCapacity);

            userEvent = new Event(userTitle, userDate, userSeatsCapacity);

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
    }
}