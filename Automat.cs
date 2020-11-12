using System.Collections.Generic;
using System.Threading;

namespace Kaffeeautomat
{
    partial class Automat
    {
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
        public UI UserInterface;

        public status AktuellerStatus
        {
            get
            {
                return aktuellerStatus;
            }
            set
            {
                aktuellerStatus = value;
                UserInterface.PrintStatus(GetStatusString()); // Status hat sich geändert, automatisch im UserInterface ausgeben
            }
        }   
        public Automat(int kaffee, int wasser, int milch, UI ui)
        {
            this.kaffee = kaffee;
            this.wasser = wasser;
            this.milch = milch;
            this.UserInterface = ui;
            this.aktuellerStatus = status.bereit;

            UserInterface.Start(this); // starte das UserInterface und übergebe den Automaten
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
            UserInterface.PrintInfo($"Automat wird aufgefüllt, bitte warten!");
            Thread.Sleep(2000);
            kaffee = 1000;
            wasser = 1000;
            milch = 500;
            AktuellerStatus = status.bereit;
            UserInterface.PrintInfo($"Wartung beendet.");
        }
        public void AuswahlAusfuheren(int auswahl)
        {
            if (!Pruefen(sorten[auswahl]))
            {
                UserInterface.PrintInfo($"Fehler! Bitte warten Sie das Gerät!");
                AktuellerStatus = status.benoetigt_Wartung;
                return;
            }
            UserInterface.PrintInfo($"{sorten[auswahl].bezeichnung} wird zubereitet, bitte warten!");
            AktuellerStatus = status.beschaeftigt;
            Thread.Sleep(1000);
            if (Zubereiten(sorten[auswahl]))
            {
                UserInterface.PrintInfo($"Vorgang beendet.");
            }
            else
            {
                UserInterface.PrintInfo($"Es ist ein Fehler aufgetreten!");
            }
            Thread.Sleep(1000);
        }
    }   
}
