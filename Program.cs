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
            string eingabe;

            // der Automat kann Kaffee frisch mahlen oder Kakao/ Tee aus einem Fach holen
            Automat ersterAutomat = new Automat(10000, Automat.status.bereit); // Wassermenge festlegen in ml, Status festlegen

            List<Getraenk> sorten = new List<Getraenk>()
            {
                new Getraenk("Kaffee schwarz", 1.50, 1, 2000, true, true),
                new Getraenk("Kaffee weiss", 1.50, 2, 2000, true, true),
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
                Console.WriteLine("Kaffeeautomat!\n\nWasserstand: {0} Status: {1}\n", ersterAutomat.wasser, ersterAutomat.aktuellerStatus);
                Console.WriteLine("Folgende Sorten stehen zur Auswahl:\n\n");
                for (int i = 0; i < sorten.Count; i++)
                {
                    Console.WriteLine("#{0} {1} -- Preis: {2}", i, sorten[i].bezeichnung, sorten[i].preis);
                }
                do
                {
                    Console.WriteLine("\nBitte Nummer wählen:");
                    eingabe = Console.ReadLine();
                } while (!int.TryParse(eingabe, out auswahl));

                if (auswahl >= 0 && auswahl < sorten.Count)
                {
                    Console.WriteLine("Bitte warten!\n");
                    if (ersterAutomat.Zubereiten(sorten[auswahl].fach, sorten[auswahl].dauer, sorten[auswahl].mahlen, sorten[auswahl].kochen) == true)
                    {
                        Console.WriteLine("{0} erfolgreich zubereitet, bitte nehmen Sie das Getränk!", sorten[auswahl].bezeichnung);
                    }
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Fehler bei der Auswahl!\n");
                    Console.ReadLine();
                }
                
            } while (true);
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
        public enum status { bereit, beschaeftigt, fehler}

        public int wasser;
        public status aktuellerStatus;

        public Automat(int wasser, status aktuellerStatus)
        {
            this.wasser = wasser;
            this.aktuellerStatus = aktuellerStatus;
        }
        public bool Zubereiten(int fach, int dauer, bool mahlen, bool kochen)
        {
            aktuellerStatus = status.beschaeftigt;
            Becher();
            if (mahlen) Mahlen(fach);
            else Pulver(fach);
            Wasser(dauer, true);
            aktuellerStatus = status.bereit;
            return true;
        }
        private bool Becher()
        {
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
            Thread.Sleep(2000);
            return true;
        }
        private bool Wasser(int dauer, bool kochen)
        {
            this.wasser -= dauer / 1000 * 100; // 100ml pro Sekunde
            Thread.Sleep(dauer);

            if (kochen)
            {
                // -> Wasserkochen
                Thread.Sleep(2000);
            }
            
            return true;
        }
    }
    class GeldTransaktion
    {
        //TODO: einbauen
    }
}
