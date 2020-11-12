namespace Kaffeeautomat
{
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
}
