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
        public int maxRange = sorten.Count - 1;
        public int auswahl;

        public void Start()
        {
            for (int i = 0; i<sorten.Count; i++)
            {
                UIConsole1.UIElements.Add(new Button(sorten[i].bezeichnung, 5, i + 5));
            }
            UIConsole1.UIElements.Add(new Text("Kaffeeautomat by TobiH!", 0, 0));
            UIConsole1.UIElements.Add(new Text(GetStatusString(), 0, 1));
            UIConsole1.UIElements.Add(new Text("Folgende Sorten stehen zur Auswahl:", 0, 3));
            UIConsole1.UIElements.Add(new Text("W -> Wartung, X -> ausschalten", 0, 12));
            UIConsole1.UIElements.Add(new Text("Info: ", 0, 14));

            UIConsole1.UIElements[auswahl].selected = true;

            UIConsole1.DrawUIElements();
        }

        public int Auswahl
        {
            get
            {
                return auswahl;
            }
            set
            {
                UIConsole1.UIElements[auswahl].selected = false;
                auswahl = value;
                if (Auswahl < 0) auswahl = maxRange;
                else if (Auswahl > maxRange) auswahl = 0;

                UIConsole1.UIElements[auswahl].selected = true;
                UIConsole1.DrawUIElements();
            }
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

        public string GetStatusString()
        {
            string myString = $"Kaffee(g): {kaffee} Wasserstand: {wasser} Milch: {milch} Status: {aktuellerStatus}                 ";
            return myString;
        }

        public void Warten()
        {
            aktuellerStatus = status.Wartung;
            UIConsole1.PrintStatus(GetStatusString());
            UIConsole1.PrintInfo($"Automat wird aufgefüllt, bitte warten!");

            Thread.Sleep(2000);
            kaffee = 1000;
            wasser = 1000;
            milch = 500;
            aktuellerStatus = status.bereit;

            UIConsole1.PrintInfo($"Wartung beendet.");
            UIConsole1.PrintStatus(GetStatusString());
        }
        public void AuswahlAusfuheren()
        {
            if (!Pruefen(sorten[auswahl]))
            {
                UIConsole1.PrintInfo($"Fehler! Bitte warten Sie das Gerät!");
                aktuellerStatus = status.Wartung;
                UIConsole1.PrintStatus(GetStatusString());
                return;
            }
            UIConsole1.PrintInfo($"{sorten[auswahl].bezeichnung} wird zubereitet, bitte warten!");
            aktuellerStatus = status.beschaeftigt;
            UIConsole1.PrintStatus(GetStatusString());
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
            UIConsole1.PrintStatus(GetStatusString());
        }
    }
    
}
