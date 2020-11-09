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
            Automat ersterAutomat = new Automat(10000, 100, Automat.status.bereit); // Wassermenge festlegen in ml, Becher, Status festlegen

            List<Getraenk> sorten = new List<Getraenk>()
            {
                // Bezeichnung, Preis, Fach, Wassermenge(ms), mahlen, kochen
                new Getraenk("Kaffee schwarz", 1.50, 1, 2000, true, true),
                new Getraenk("Kaffee mit Milch", 1.50, 2, 2000, true, true),
                new Getraenk("Cappucino", 1.50, 3, 2000, true, true),
                new Getraenk("Espresso", 1.50, 4, 1000, true, true),
                new Getraenk("Kakao", 1.50, 5, 1000, true, true),
                new Getraenk("Tee", 1, 6, 2000, false, true),
                new Getraenk("Wasser heiss", 1.50, 0, 2000, false, true),
                new Getraenk("Wasser", 1.50, 0, 2000, false, false)
            };

            do
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Kaffeeautomat!\n\nWasserstand: {0} Becher: {1} Status: {2}\n", ersterAutomat.wasser, ersterAutomat.becher, ersterAutomat.aktuellerStatus);
                Console.WriteLine("Folgende Sorten stehen zur Auswahl:\n\n");
                Console.WriteLine("{0,-5} {1,-30} {2,-5:N2}\n", "Nr", "Bezeichnung", "Preis");

                for (int i = 0; i < sorten.Count; i++)
                {
                    if (auswahl == i)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    Console.WriteLine("{0,-5} {1,-30} {2,-5:N2}", i, sorten[i].bezeichnung, sorten[i].preis);
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
        public double preis;
        public int fach;
        public int dauer;
        public bool mahlen;
        public bool kochen;
        public Getraenk(string bezeichnung, double preis, int fach, int dauer, bool mahlen, bool kochen)
        {
            this.bezeichnung = bezeichnung;
            this.preis = preis;
            this.fach = fach;
            this.mahlen = mahlen;
            this.dauer = dauer;
            this.kochen = kochen;
        }      
    }
    class Automat
    {
        public enum status { bereit, beschaeftigt, warnung, fehler}

        public int wasser;
        public int becher;
        public status aktuellerStatus;

        public Automat(int wasser, int becher, status aktuellerStatus)
        {
            this.wasser = wasser;
            this.becher = becher;
            this.aktuellerStatus = aktuellerStatus;
        }
        public bool Zubereiten(Getraenk auswahl)
        {
            aktuellerStatus = status.beschaeftigt;
            Becher();
            if (auswahl.mahlen) Mahlen(auswahl.fach);
            else Pulver(auswahl.fach);
            Wasser(auswahl.dauer, true);
            return true;
        }
        private bool Becher() //TODO: Becher prüfen
        {
            becher--;
            // -> Becher bereitstellen
            return true;
        }
        private bool Mahlen(int fach)
        {
            // -> Mahlwerk aktivieren
            Thread.Sleep(2000);
            return true;
        }
        private bool Pulver(int fach)
        {
            // -> Fach mit Pulver öffnen
            Thread.Sleep(1000);
            return true;
        }
        private bool Wasser(int dauer, bool kochen)
        {
            wasser -= dauer / 1000 * 100; // 100ml pro Sekunde
            
            Thread.Sleep(dauer);

            if (kochen)
            {
                // -> Wasserkochen
                Thread.Sleep(2000);
            }
            return true;
        }

        public bool Warten()
        {
            becher = 100;
            wasser = 10000;
            return true;
        }
    }
    class GeldTransaktion
    {
        //TODO: einbauen
    }
}
