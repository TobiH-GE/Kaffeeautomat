using System;
using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    class Program // TODO: Fehlerbehandlung
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();

            List<Getraenk> sorten = new List<Getraenk>()
            {
                // Bezeichnung, Fach, Kaffeemenge, Wassermenge(ms), Milchmenge, mahlen
                new Getraenk("Kaffee schwarz, klein", 1, 15, 1500, 0, true),
                new Getraenk("Kaffee schwarz, gross", 1, 30, 3000, 0, true),
                new Getraenk("Kaffee mit Milch", 2, 20, 2000, 1000, true),
                new Getraenk("Cappucino", 3, 30, 1000, 2000, true),
                new Getraenk("Espresso", 4, 30, 1000, 0, true),
                new Getraenk("Wasser heiss", 0, 0, 3000, 0, false)
            };

            Automat ersterAutomat = new Automat(1000, 1000, 500, Automat.status.bereit, sorten.Count - 1); // Kaffee, Wassermenge, Milch, Status festlegen
            
            List<UIObject> UIElements = new List<UIObject>();

            for (int i = 0; i < sorten.Count; i++)
            {
                UIElements.Add(new Button(sorten[i].bezeichnung, 5, i + 5));
            }       
            UIElements.Add(new Text("Kaffeeautomat by TobiH!", 0, 0));
            UIElements.Add(new Text(ersterAutomat.GetStatusString(), 0, 1));
            UIElements.Add(new Text("Folgende Sorten stehen zur Auswahl:", 0, 3));
            UIElements.Add(new Text("W -> Wartung, X -> ausschalten", 0, 15));
            UIElements.Add(new Text("Info: ", 0, 17));

            Console.Clear();
            Console.CursorVisible = false;

            do
            {
                SelectUIElement(ref UIElements, ersterAutomat.auswahl); //TODO: UIElements.Draw() verbessern
                DrawUIElements(ref UIElements);

                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        ersterAutomat.Auswahl--;
                        break;
                    case ConsoleKey.DownArrow:
                        ersterAutomat.Auswahl++;
                        break;
                    case ConsoleKey.W:
                        // Wartung
                        ersterAutomat.Warten();
                        UIElements[sorten.Count + 1].text = ersterAutomat.GetStatusString();
                        break;
                    case ConsoleKey.Enter:
                        // Auswahl
                        UIElements[sorten.Count + 4].TextWithDraw = $"{sorten[ersterAutomat.auswahl].bezeichnung} wird zubereitet, bitte warten!  ";
                        Thread.Sleep(1000);
                        if (ersterAutomat.Zubereiten(sorten[ersterAutomat.auswahl]))
                        {
                            UIElements[sorten.Count + 4].TextWithDraw = $"Vorgang beendet.                                 ";
                        }
                        else
                        {
                            UIElements[sorten.Count + 4].TextWithDraw = $"Es ist ein Fehler aufgetreten!                       ";
                        }
                        Thread.Sleep(1000);
                        UIElements[sorten.Count + 1].text = ersterAutomat.GetStatusString();
                        break;
                    default:
                        break;
                }
            } while (UserInput.Key != ConsoleKey.X);
        }
        public static void DrawUIElements(ref List<UIObject> UIElementes)
        {
            for (int i = 0; i < UIElementes.Count; i++)
            {
                UIElementes[i].Draw();
            }
        }
        public static void SelectUIElement(ref List<UIObject> UIElementes, int index)
        {
            for (int i = 0; i < UIElementes.Count; i++)
            {
                if (i == index)
                {
                    UIElementes[i].selected = true;
                }
                else
                {
                    UIElementes[i].selected = false;
                }
            }
        }
    }
    
}
