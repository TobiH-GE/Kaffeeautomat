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

            Automat ersterAutomat = new Automat(1000, 1000, 500, Automat.status.bereit); // Kaffee, Wassermenge, Milch, Status festlegen

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
            List<UIObject> UIElements = new List<UIObject>();

            for (int i = 0; i < sorten.Count; i++)
            {
                UIElements.Add(new Button(UIObject.Type.Button, sorten[i].bezeichnung, 5, i + 5));
            }       
            UIElements.Add(new Text(UIObject.Type.Text, "Kaffeeautomat by TobiH!", 0, 0));
            UIElements.Add(new Text(UIObject.Type.Text, ersterAutomat.GetStatusString(), 0, 1));
            UIElements.Add(new Text(UIObject.Type.Text, "Folgende Sorten stehen zur Auswahl:", 0, 3));
            UIElements.Add(new Text(UIObject.Type.Text, "W -> Wartung, X -> ausschalten", 0, 15));

            Console.Clear();
            Console.CursorVisible = false;
            
            do
            {
                SelectUIElement(ref UIElements, ersterAutomat.auswahl);
                DrawUIElements(ref UIElements);
                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        ersterAutomat.auswahl--;
                        if (ersterAutomat.auswahl < 0) ersterAutomat.auswahl = sorten.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        ersterAutomat.auswahl++;
                        if (ersterAutomat.auswahl > sorten.Count - 1) ersterAutomat.auswahl = 0;
                        break;
                    case ConsoleKey.W:
                        // Wartung
                        ersterAutomat.Warten();
                        UIElements[sorten.Count + 1].text = ersterAutomat.GetStatusString();
                        Console.Clear();
                        break;
                    case ConsoleKey.Enter:
                        // Auswahl
                        Console.WriteLine("{0} wird zubereitet, bitte warten!", sorten[ersterAutomat.auswahl].bezeichnung);
                        if (ersterAutomat.Zubereiten(sorten[ersterAutomat.auswahl]))
                        {
                            Console.WriteLine("Vorgang beendet.", sorten[ersterAutomat.auswahl].bezeichnung);
                        }
                        else
                        {
                            Console.WriteLine("Es ist ein Fehler aufgetreten!");
                        }
                        Thread.Sleep(1000);
                        UIElements[sorten.Count + 1].text = ersterAutomat.GetStatusString();
                        Console.Clear();
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

    class UIObject
    {
        public string text;
        public enum Type
        {
            Button,
            Text
        }         
        protected Type type;
        protected int x;
        protected int y;
        protected ConsoleColor fColor;
        protected ConsoleColor bColor;
        public bool selected;

        public virtual void Draw()
        {

        }
    }
    class Button : UIObject
    {
        public Button(Type type, string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false)
        {
            this.type = type;
            this.text = text;
            this.x = x;
            this.y = y;
            this.fColor = fColor;
            this.bColor = bColor;
            this.selected = selected;
        }
        public override void Draw() //TODO: evtl. in Basisklasse packen
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fColor;
            if (selected)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.BackgroundColor = bColor;
            }
            Console.Write(text);
            Console.ResetColor();
        }
    }
    class Text : UIObject
    {
        public bool selected;

        public Text(Type type, string text, int x, int y, ConsoleColor fColor = ConsoleColor.White, ConsoleColor bColor = ConsoleColor.Black, bool selected = false)
        {
            this.type = type;
            this.text = text;
            this.x = x;
            this.y = y;
            this.fColor = fColor;
            this.bColor = bColor;
            this.selected = selected;
        }
        public override void Draw() //TODO: evtl. in Basisklasse packen
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = fColor;
            if (selected)
            {
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else
            {
                Console.BackgroundColor = bColor;
            }
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
        public int auswahl;

        public Automat(int kaffee, int wasser, int milch, status aktuellerStatus, int auswahl = 0)
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

        public string GetStatusString()
        {
            string myString = $"Kaffee(g): {kaffee} Wasserstand: {wasser} Milch: {milch} Status: {aktuellerStatus}";
            return myString;
        }
    }
    
}
