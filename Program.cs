using System;
using System.Threading;

namespace Kaffeeautomat
{
    class Program
    {
        static void Main(string[] args)
        {
            UI UserInterface = new UIConsole(); // erstelle ein neues Konsolen-UserInterface
            //UI UserInterface = new UIGraphic(); // TODO: alternativ ein grafisches UserInterface
            Automat aAutomat = new Automat(1000, 1000, 500, UserInterface); // neuer Automat (Kaffee, Wassermenge, Milch, UserInterface)

            do
            {
                UserInterface.WaitForInput();
            } while (aAutomat.AktuellerStatus != status.ausgeschaltet);
            
        }
    }
}
