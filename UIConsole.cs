using System;
using System.Collections.Generic;

namespace Kaffeeautomat
{
    class UIConsole : UI
    {
        public List<UIObject> UIElements = new List<UIObject>();
        int UIElementWithStatus = Automat.sorten.Count + 1; // welches UIElement enthält den Status-Text
        int UIElementWithInfo = Automat.sorten.Count + 4; // welches UIElement enthält den Info-Text
        public int maxRange = Automat.sorten.Count - 1;
        public int auswahl;
        public Automat aAutomat;

        public int Auswahl
        {
            get
            {
                return auswahl;
            }
            set
            {
                UIElements[auswahl].selected = false;
                auswahl = value;
                if (Auswahl < 0) auswahl = maxRange;
                else if (Auswahl > maxRange) auswahl = 0;

                UIElements[auswahl].selected = true;
                DrawUIElements(); // Auswahl hat sich geändert, zeichne das UserInterface neu
            }
        }

        public override void Start(Automat automat)
        {
            aAutomat = automat;

            for (int i = 0; i < Automat.sorten.Count; i++)
            {
                UIElements.Add(new UIButton(Automat.sorten[i].bezeichnung, 5, i + 5));
            }
            UIElements.Add(new UIText("Kaffeeautomat by TobiH!", 0, 0));
            UIElements.Add(new UIText(automat.GetStatusString(), 0, 1));
            UIElements.Add(new UIText("Folgende Sorten stehen zur Auswahl:", 0, 3));
            UIElements.Add(new UIText("W -> Wartung, X -> ausschalten", 0, 12));
            UIElements.Add(new UIText("Info: ", 0, 14));

            UIElements[auswahl].selected = true;

            Console.Clear();
            Console.CursorVisible = false;
            DrawUIElements();
        }
        public override void WaitForInput()
        {
            ConsoleKeyInfo UserInput = new ConsoleKeyInfo();
            do
            {
                UserInput = Console.ReadKey();

                switch (UserInput.Key)
                {
                    case ConsoleKey.UpArrow:
                        Auswahl--;
                        break;
                    case ConsoleKey.DownArrow:
                        Auswahl++;
                        break;
                    case ConsoleKey.W:
                        aAutomat.Warten();
                        break;
                    case ConsoleKey.Enter:
                        aAutomat.AuswahlAusfuheren(auswahl);
                        break;
                    default:
                        break;
                }
            } while (UserInput.Key != ConsoleKey.X);

            aAutomat.AktuellerStatus = status.ausgeschaltet;
        }
        public void DrawUIElements()
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                UIElements[i].Draw();
            }
        }
        public override void PrintInfo(string info)
        {
            UIElements[UIElementWithInfo].TextWithDraw = "Info: " + info + "                                    ";
        }
        public override void PrintStatus(string statusstr)
        {
            UIElements[UIElementWithStatus].TextWithDraw = statusstr + "                                    ";
        }
    }
}
