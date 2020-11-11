using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    partial class Automat
    {
        public UIConsole UIConsole1 = new UIConsole();

        public static List<Getraenk> sorten = new List<Getraenk>()
            {
                // Bezeichnung, Fach, Kaffeemenge, Wassermenge, Milchmenge, mahlen
                new Getraenk("Kaffee schwarz, klein", 1, 15, 1500, 0, true),
                new Getraenk("Kaffee schwarz, gross", 1, 30, 3000, 0, true),
                new Getraenk("Kaffee mit Milch", 2, 20, 2000, 1000, true),
                new Getraenk("Cappucino", 3, 30, 1000, 2000, true),
                new Getraenk("Espresso", 4, 30, 1000, 0, true),
                new Getraenk("Wasser heiss", 0, 0, 3000, 0, false)
            };

        public int kaffee;
        public int wasser;
        public int milch;
        public status aktuellerStatus;

        public status AktuellerStatus
        {
            get
            {
                return aktuellerStatus;
            }
            set
            {
                aktuellerStatus = value;
                UIConsole1.PrintStatus(GetStatusString()); // Status hat sich geändert, automatisch in der UIConsole ausgeben
            }
        }

        public UIConsole Start()
        { 
            UIConsole1.Start(this); // UIConsole starten, übergibt den Automaten
            return UIConsole1;      // UIConsole zurückgeben an Main
        }       
        public Automat(int kaffee, int wasser, int milch, status aktuellerStatus = status.bereit, int auswahl = 0)
        {
            this.kaffee = kaffee;
            this.wasser = wasser;
            this.milch = milch;
            this.aktuellerStatus = aktuellerStatus;
        }
        public bool Pruefen(Getraenk auswahl)
        {
            if (auswahl.mengeKaffee > kaffee ||
                auswahl.dauerWasser > wasser*10 ||
                auswahl.dauerMilch > milch*10)
                return false;
            return true;
        }

        public bool Zubereiten(Getraenk auswahl)
        {
            Kaffee(auswahl.mengeKaffee);
            if (auswahl.mahlen) Mahlen(auswahl.fach);
            Wasser(auswahl.dauerWasser);
            Milch(auswahl.dauerMilch);
            AktuellerStatus = status.bereit;
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

        public string GetStatusString()
        {
            string myString = $"Kaffee(g): {kaffee} Wasserstand: {wasser} Milch: {milch} Status: {AktuellerStatus}                 ";
            return myString;
        }

        public void Warten()
        {
            AktuellerStatus = status.Wartung;
            UIConsole1.PrintInfo($"Automat wird aufgefüllt, bitte warten!");
            Thread.Sleep(2000);
            kaffee = 1000;
            wasser = 1000;
            milch = 500;
            AktuellerStatus = status.bereit;
            UIConsole1.PrintInfo($"Wartung beendet.");
        }
        public void AuswahlAusfuheren(int auswahl)
        {
            if (!Pruefen(sorten[auswahl]))
            {
                UIConsole1.PrintInfo($"Fehler! Bitte warten Sie das Gerät!");
                AktuellerStatus = status.benoetigt_Wartung;
                return;
            }
            UIConsole1.PrintInfo($"{sorten[auswahl].bezeichnung} wird zubereitet, bitte warten!");
            AktuellerStatus = status.beschaeftigt;
            Thread.Sleep(1000);
            if (Zubereiten(sorten[auswahl]))
            {
                UIConsole1.PrintInfo($"Vorgang beendet.");
            }
            else
            {
                UIConsole1.PrintInfo($"Es ist ein Fehler aufgetreten!");
            }
            Thread.Sleep(1000);
        }
    }   
}
