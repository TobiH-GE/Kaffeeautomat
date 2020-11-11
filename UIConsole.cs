using System.Collections.Generic;

namespace Kaffeeautomat
{
    class UIConsole
    {
        public List<UIObject> UIElements = new List<UIObject>();
        int UIElementWithStatus = Automat.sorten.Count + 1; // welches UIElement enthält den Status-Text
        int UIElementWithInfo = Automat.sorten.Count + 4; // welches UIElement enthält den Info-Text

        public void DrawUIElements()
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                UIElements[i].Draw();
            }
        }
        public void PrintInfo(string info)
        {
            UIElements[UIElementWithInfo].TextWithDraw = "Info: " + info + "                                    ";
        }
        public void PrintStatus(string statusstr)
        {
            UIElements[UIElementWithStatus].TextWithDraw = statusstr + "                                    ";
        }
    }
}
