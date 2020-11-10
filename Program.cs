using System;
using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    class Program // TODO: Fehlerbehandlung
    {
        static void Main(string[] args)
        {
            List<Getraenk> sorten = new List<Getraenk>()
            {
                // Bezeichnung, Fach, Kaffeemenge, Wassermenge, Milchmenge, mahlen
                new Getraenk("Kaffee schwarz, klein", 1, 15, 1500, 0, true),
                new Getraenk("Kaffee schwarz, gross", 1, 30, 3000, 0, true),
                new Getraenk("Kaffee mit Milch", 2, 20, 2000, 1000, true),
                new Getraenk("Cappucino", 3, 30, 1000, 2000, true),
                new Getraenk("Espresso", 4, 30, 1000, 0, true),
                new Getraenk("Wasser heiss", 0, 0, 3000, 0, false)
            };

            Automat ersterAutomat = new Automat(1000, 1000, 500, Automat.status.bereit, sorten.Count - 1); // Kaffee, Wassermenge, Milch, Status, Anzahl Auswahl
            
            List<UIObject> UIElements = new List<UIObject>();

            for (int i = 0; i < sorten.Count; i++)
            {
                UIElements.Add(new Button(sorten[i].bezeichnung, 5, i + 5));
            }       
            UIElements.Add(new Text("Kaffeeautomat by TobiH!", 0, 0));
            UIElements.Add(new Text(ersterAutomat.GetStatusString(), 0, 1));
            UIElements.Add(new Text("Folgende Sorten stehen zur Auswahl:", 0, 3));
            UIElements.Add(new Text("W -> Wartung, X -> ausschalten", 0, 12));
            UIElements.Add(new Text("Info: ", 0, 14));

            int UIElementWithStatus = sorten.Count + 1;

            Console.Clear();
            Console.CursorVisible = false;

            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();
            do
            {
                UIElements[ersterAutomat.auswahl].selected = true; ;
                DrawUIElements(ref UIElements);

                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        UIElements[ersterAutomat.Auswahl--].selected = false;
                        break;
                    case ConsoleKey.DownArrow:
                        UIElements[ersterAutomat.Auswahl++].selected = false;
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
                PrintInfo($"Automat wird aufgefüllt, bitte warten!");
                ersterAutomat.Warten();
                UIElements[UIElementWithStatus].text = ersterAutomat.GetStatusString();
                PrintInfo($"Wartung beendet.");
            }

            void Auswahl()
            {
                if (!ersterAutomat.Pruefen(sorten[ersterAutomat.auswahl]))
                {
                    PrintInfo($"Fehler! Bitte warten Sie das Gerät!");
                    ersterAutomat.aktuellerStatus = Automat.status.Wartung;
                    UIElements[UIElementWithStatus].text = ersterAutomat.GetStatusString();
                    return;
                }
                PrintInfo($"{sorten[ersterAutomat.auswahl].bezeichnung} wird zubereitet, bitte warten!");
                ersterAutomat.aktuellerStatus = Automat.status.beschaeftigt;
                UIElements[UIElementWithStatus].TextWithDraw = ersterAutomat.GetStatusString();
                Thread.Sleep(1000);
                if (ersterAutomat.Zubereiten(sorten[ersterAutomat.auswahl]))
                {
                    PrintInfo($"Vorgang beendet.");
                }
                else
                {
                    PrintInfo($"Es ist ein Fehler aufgetreten!");
                }
                Thread.Sleep(1000);
                UIElements[UIElementWithStatus].text = ersterAutomat.GetStatusString();
            }

            void PrintInfo(string info)
            {
                UIElements[sorten.Count + 4].TextWithDraw = "Info: "+info+"                                    ";
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
