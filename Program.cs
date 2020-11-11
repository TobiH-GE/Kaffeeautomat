using System;
using System.Threading;

namespace Kaffeeautomat
{
    class Program
    {
        static void Main(string[] args)
        {
            Automat aAutomat = new Automat(1000, 1000, 500); // Kaffee, Wassermenge, Milch
            
            Console.Clear();
            Console.CursorVisible = false;

            aAutomat.Start();

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();
            do
            {
                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        aAutomat.Auswahl--;
                        break;
                    case ConsoleKey.DownArrow:
                        aAutomat.Auswahl++;
                        break;
                    case ConsoleKey.W:
                        aAutomat.Warten();
                        break;
                    case ConsoleKey.Enter:
                        aAutomat.AuswahlAusfuheren();
                        break;
                    default:
                        break;
                }
            } while (UserInput.Key != ConsoleKey.X);
        }
    }
}
