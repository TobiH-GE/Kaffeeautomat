using System;
using System.Threading;

namespace Kaffeeautomat
{
    class Program
    {
        static void Main(string[] args)
        {
            Automat aAutomat = new Automat(1000, 1000, 500); // neuer Automat (Kaffee, Wassermenge, Milch)
            var aUIConsole = aAutomat.Start(); // Automat starten, liefert vom Automaten verwendete UI zurück

            do
            {
                aUIConsole.WaitForInput();
            } while (aAutomat.AktuellerStatus != status.ausgeschaltet);
            
        }
    }
}
