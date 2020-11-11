using System.Collections.Generic;

namespace Kaffeeautomat
{
    class UIConsole
    {
        public List<UIObject> UIElements = new List<UIObject>();

        public void DrawUIElements()
        {
            for (int i = 0; i < UIElements.Count; i++)
            {
                UIElements[i].Draw();
            }
        }
        public void PrintInfo(string info)
        {
            UIElements[Automat.sorten.Count + 4].TextWithDraw = "Info: " + info + "                                    ";
        }
    }
}
