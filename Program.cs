using System;

namespace Golf
{
    class Program
    {
        static void Main(string[] args)
        {

            PrintWelcomeMessage();
            Console.ReadKey();

            Game game = new Game();

            PrintExitMessage();
            Console.ReadKey();


            static void PrintWelcomeMessage()
            {
                Console.WriteLine("Welcome to the Golf practice game!");
                Console.WriteLine("Here you can practice your swings, one hole at a time!");
                Console.WriteLine("Press any key to begin!\n");
            }

            static void PrintExitMessage()
            {
                Console.WriteLine("Thank you for playing the Golf practice game!");
                Console.WriteLine("Press any key to exit!");

            }
        }
    }
}
