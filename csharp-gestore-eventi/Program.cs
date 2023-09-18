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
                    2 - Prenota posti all'evento
                    3 - Disdici posti a un evento
                    0 - Esci
                    ");
                userChoice = Console.ReadLine()?? "";

                switch (userChoice)
                {
                    case "1":
                        AddEvent();
                        break;

                }
            } while (userChoice != "0");
        }

        public static void AddEvent() 
        {
            string userTitle;
            DateTime userDate;

            Console.WriteLine("--- Aggiunta di un evento ---");

            
            Console.Write("Inserisci il nome dell'evento: ");
            while (!IsUserTitleValid()) 
                Console.Write("Inserisci un nome valido: ");

            Console.Write("Inserisci la data dell'evento (gg/mm/yyyy): ");
            while (!IsUserDateValid())
                Console.Write("Inserisci una data valida (gg/mm/yyyy): ");

            bool IsUserTitleValid()
            {
                userTitle = Console.ReadLine() ?? "";

                if (string.IsNullOrEmpty(userTitle))
                    return false;
                else
                    return true;

            }
            bool IsUserDateValid()
            {
                return true;
            }


        }
    }
}