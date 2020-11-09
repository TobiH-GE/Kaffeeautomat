using System;
using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    class Program
    {      
        static void Main(string[] args)
        {
            int auswahl = 0;
            string eingabe;

            Automat ersterAutomat = new Automat();
            ersterAutomat.wasser = 10000; // Wassermenge festlegen in ml
            ersterAutomat.aStatus = Automat.status.bereit;

            List<Getraenk> Sorten = new List<Getraenk>()
            {
                new Getraenk("Kaffee schwarz", 1.50, 1, 2000, true),
                new Getraenk("Kaffee weiss", 1.50, 2, 2000, true),
                new Getraenk("Cappucino", 1.50, 3, 2000, true),
                new Getraenk("Espresso", 1.50, 4, 1000, true),
                new Getraenk("Tee", 1, 5, 2000, true),
                new Getraenk("Wasser heiss", 1.50, 0, 2000, true),
                new Getraenk("Wasser", 1.50, 0, 2000, false)
            };
            Console.WriteLine("Kaffeeautomat!\n Wasserstand: {0} Status: {1}", ersterAutomat.wasser, ersterAutomat.aStatus);
            Console.WriteLine("Folgende Sorten stehen zur Auswahl:\n\n");
            for (int i = 0; i < Sorten.Count; i++)
            {
                Console.WriteLine("{0} {1} {2}", i, Sorten[i].bezeichnung, Sorten[i].preis);
            }
            do
            {
                Console.WriteLine("\nBitte Nummer wählen:");
                eingabe = Console.ReadLine();
            } while (!int.TryParse(eingabe, out auswahl));

            Console.WriteLine("Bitte warten!\n");
            if (ersterAutomat.Zubereiten(Sorten[auswahl].fach, Sorten[auswahl].dauer, Sorten[auswahl].kochen) == true)
            {
                Console.WriteLine("{0} erfolgreich zubereitet, bitte nehmen Sie das Getränk!", Sorten[auswahl]);
            }
            Console.ReadLine();
        }
    }

    class Getraenk
    {
        public string bezeichnung;
        public double preis;
        public int fach;
        public int dauer;
        public bool kochen;
        public Getraenk(string bezeichnung, double preis, int fach, int dauer, bool kochen)
        {
            this.bezeichnung = bezeichnung;
            this.preis = preis;
            this.fach = fach;
            this.dauer = dauer;
            this.kochen = kochen;
        }      
    }
    class Automat
    {
        public enum status { bereit, beschaeftigt, fehler}

        public int wasser;
        public status aStatus;
        public bool Zubereiten(int fach, int dauer, bool kochen)
        {
            aStatus = status.beschaeftigt;
            Becher();
            Mahlen(fach);
            Wasser(dauer, true);
            return true;
        }
        private bool Becher()
        {
            return true;
        }
        private bool Mahlen(int fach)
        {
            Thread.Sleep(3000);
            return true;
        }

        private bool Wasser(int dauer, bool kochen)
        {
            Thread.Sleep(dauer);
            return true;
            aStatus = status.bereit;
        }
    }
    class GeldTransaktion
    {
        //TODO: einbauen
    }
}
