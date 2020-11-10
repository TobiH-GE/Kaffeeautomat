using System;
using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    class Program // TODO: Fehlerbehandlung
    {
        static void Main(string[] args)
        {
            Automat aAutomat = new Automat(1000, 1000, 500); // Kaffee, Wassermenge, Milch
            
            List<UIObject> UIElements = new List<UIObject>();

            for (int i = 0; i < Automat.sorten.Count; i++)
            {
                UIElements.Add(new Button(Automat.sorten[i].bezeichnung, 5, i + 5));
            }       
            UIElements.Add(new Text("Kaffeeautomat by TobiH!", 0, 0));
            UIElements.Add(new Text(aAutomat.GetStatusString(), 0, 1));
            UIElements.Add(new Text("Folgende Sorten stehen zur Auswahl:", 0, 3));
            UIElements.Add(new Text("W -> Wartung, X -> ausschalten", 0, 12));
            UIElements.Add(new Text("Info: ", 0, 14));

            int UIElementWithStatus = Automat.sorten.Count + 1;

            Console.Clear();
            Console.CursorVisible = false;

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();
            do
            {
                UIElements[aAutomat.auswahl].selected = true;
                DrawUIElements(ref UIElements);

                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        UIElements[aAutomat.Auswahl--].selected = false;
                        break;
                    case ConsoleKey.DownArrow:
                        UIElements[aAutomat.Auswahl++].selected = false;
                        break;
                    case ConsoleKey.W:
                        Warten();
                        break;
                    case ConsoleKey.Enter:
                        Auswahl();
                        break;
                    default:
                        break;
                }
            } while (UserInput.Key != ConsoleKey.X);

            void Warten()
            {
                aAutomat.aktuellerStatus = Automat.status.Wartung;
                UIElements[UIElementWithStatus].TextWithDraw = aAutomat.GetStatusString();
                PrintInfo($"Automat wird aufgefüllt, bitte warten!");
                aAutomat.Warten();
                PrintInfo($"Wartung beendet.");
                UIElements[UIElementWithStatus].TextWithDraw = aAutomat.GetStatusString();
            }

            void Auswahl()
            {
                if (!aAutomat.Pruefen(Automat.sorten[aAutomat.auswahl]))
                {
                    PrintInfo($"Fehler! Bitte warten Sie das Gerät!");
                    aAutomat.aktuellerStatus = Automat.status.Wartung;
                    UIElements[UIElementWithStatus].text = aAutomat.GetStatusString();
                    return;
                }
                PrintInfo($"{Automat.sorten[aAutomat.auswahl].bezeichnung} wird zubereitet, bitte warten!");
                aAutomat.aktuellerStatus = Automat.status.beschaeftigt;
                UIElements[UIElementWithStatus].TextWithDraw = aAutomat.GetStatusString();
                Thread.Sleep(1000);
                if (aAutomat.Zubereiten(Automat.sorten[aAutomat.auswahl]))
                {
                    PrintInfo($"Vorgang beendet.");
                }
                else
                {
                    PrintInfo($"Es ist ein Fehler aufgetreten!");
                }
                Thread.Sleep(1000);
                UIElements[UIElementWithStatus].text = aAutomat.GetStatusString();
            }

            void PrintInfo(string info)
            {
                UIElements[Automat.sorten.Count + 4].TextWithDraw = "Info: "+info+"                                    ";
            }

            void DrawUIElements(ref List<UIObject> UIElementes)
            {
                for (int i = 0; i < UIElementes.Count; i++)
                {
                    UIElementes[i].Draw();
                }
            }
        }
    }
    
}
