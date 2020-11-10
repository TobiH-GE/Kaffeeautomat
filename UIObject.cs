using System;

namespace Kaffeeautomat
{
    class UIObject
    {
        public string text;
        protected int x;
        protected int y;
        protected ConsoleColor fColor;
        protected ConsoleColor bColor;
        public bool selected;

        public string TextWithDraw
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                Draw();
            }
        }
        
        public void Draw()
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
    
}
