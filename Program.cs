using System;
using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    class Program // TODO: Fehlerbehandlung
    {
        static void Main(string[] args)
        {
            int auswahl = 0;
            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();

            // der Automat kann Kaffee frisch mahlen oder Kakao/ Tee aus einem Fach holen
            Automat ersterAutomat = new Automat(1000, 1000, 500, Automat.status.bereit); // Wassermenge festlegen in ml, Becher, Status festlegen

            List<Getraenk> sorten = new List<Getraenk>()
            {
                // Bezeichnung, Fach, Wassermenge(ms), Milchmenge, mahlen
                new Getraenk("Kaffee schwarz, klein", 1, 15, 1500, 0, true),
                new Getraenk("Kaffee schwarz, gross", 1, 30, 3000, 0, true),
                new Getraenk("Kaffee mit Milch", 2, 20, 2000, 1000, true),
                new Getraenk("Cappucino", 3, 30, 1000, 2000, true),
                new Getraenk("Espresso", 4, 30, 1000, 0, true),
                new Getraenk("Wasser heiss", 0, 0, 3000, 0, false)
            };

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Kaffeeautomat!\n\nKaffee (g): {0} Wasserstand: {1} Milch: {2} Status: {3}\n", ersterAutomat.kaffee, ersterAutomat.wasser, ersterAutomat.milch, ersterAutomat.aktuellerStatus);
                Console.WriteLine("Folgende Sorten stehen zur Auswahl:\n\n");
                Console.WriteLine("{0,-5} {1,-30}\n", "Nr", "Bezeichnung");

                for (int i = 0; i < sorten.Count; i++)
                {
                    if (auswahl == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine("{0,-5} {1,-30}", i, sorten[i].bezeichnung);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine("\n\nW -> Wartung, X -> ausschalten");

                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        auswahl--;
                        if (auswahl < 0) auswahl = sorten.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        auswahl++;
                        if (auswahl > sorten.Count - 1) auswahl = 0;
                        break;
                    case ConsoleKey.W:
                        // Wartung
                        ersterAutomat.Warten();
                        break;
                    case ConsoleKey.Enter:
                        // Auswahl
                        Console.WriteLine("{0} wird zubereitet, bitte warten!", sorten[auswahl].bezeichnung);
                        if (ersterAutomat.Zubereiten(sorten[auswahl]))
                        {
                            Console.WriteLine("Vorgang beendet.", sorten[auswahl].bezeichnung);
                        }
                        else
                        {
                            Console.WriteLine("Es ist ein Fehler aufgetreten!");
                        }
                        Thread.Sleep(1000);
                        break;
                    default:
                        break;
                }
            } while (UserInput.Key != ConsoleKey.X);
        }
    }

    class Getraenk
    {
        public string bezeichnung;
        public int fach;
        public int mengeKaffee;
        public int dauerWasser;
        public int dauerMilch;
        public bool mahlen;
        public Getraenk(string bezeichnung, int fach, int mengeKaffee, int dauerWasser, int dauerMilch, bool mahlen)
        {
            this.bezeichnung = bezeichnung;
            this.fach = fach;
            this.mengeKaffee = mengeKaffee;
            this.mahlen = mahlen;
            this.dauerWasser = dauerWasser;
            this.dauerMilch = dauerMilch;
        }
    }

    class Button
    {
        string text;
        int x;
        int y;
        public Button(string text, int x, int y, ConsoleColor fColor, ConsoleColor bColor, bool selected)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fColor;
            Console.BackgroundColor = bColor;
            Console.Write(text);
            Console.ResetColor();
        }
    }


    class Automat
    {
        public enum status { bereit, beschaeftigt, warnung, fehler}

        public int kaffee;
        public int wasser;
        public int milch;
        public status aktuellerStatus;

        public Automat(int kaffee, int wasser, int milch, status aktuellerStatus)
        {
            this.kaffee = kaffee;
            this.wasser = wasser;
            this.milch = milch;
            this.aktuellerStatus = aktuellerStatus;
        }
        public bool Zubereiten(Getraenk auswahl)
        {
            aktuellerStatus = status.beschaeftigt;
            Kaffee(auswahl.mengeKaffee);
            if (auswahl.mahlen) Mahlen(auswahl.fach);
            Wasser(auswahl.dauerWasser);
            Milch(auswahl.dauerMilch);
            aktuellerStatus = status.bereit;
            return true;
        }
        private bool Kaffee(int menge)
        {
            kaffee -= menge; // in g           
            Thread.Sleep(1000);
            return true;
        }
        private bool Mahlen(int fach)
        {
            // -> Mahlwerk aktivieren
            Thread.Sleep(2000);
            return true;
        }
        private bool Wasser(int dauer)
        {
            wasser -= dauer / 10; // 100ml pro Sekunde           
            Thread.Sleep(dauer);
            return true;
        }

        private bool Milch(int dauer)
        {
            milch -= dauer / 10; // 100ml pro Sekunde
            Thread.Sleep(dauer);
            return true;
        }

        public bool Warten()
        {
            kaffee = 1000;
            wasser = 1000;
            milch = 500;
            return true;
        }
    }
}
