using System.Threading;

namespace Kaffeeautomat
{
    class Automat
    {
        public enum status { bereit, beschaeftigt, warnung, fehler}

        public int kaffee;
        public int wasser;
        public int milch;
        public status aktuellerStatus;
        public int maxRange;
        public int auswahl;

        public int Auswahl
        {
            get
            {
                return auswahl;
            }
            set
            {
                auswahl = value;
                if (Auswahl < 0) auswahl = maxRange;
                else if (Auswahl > maxRange) auswahl = 0;
            }
        }
        public Automat(int kaffee, int wasser, int milch, status aktuellerStatus, int maxRange, int auswahl = 0)
        {
            this.kaffee = kaffee;
            this.wasser = wasser;
            this.milch = milch;
            this.aktuellerStatus = aktuellerStatus;
            this.maxRange = maxRange;
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

        public string GetStatusString()
        {
            string myString = $"Kaffee(g): {kaffee} Wasserstand: {wasser} Milch: {milch} Status: {aktuellerStatus}      ";
            return myString;
        }
    }
    
}
