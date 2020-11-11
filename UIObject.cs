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

        /// <summary>
        /// ändert den Text in einem UIObject und zeichnet nur dieses UIObject mit Draw() neu
        /// </summary>
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
        
        /// <summary>
        /// zeichnet ein UI-Element an Position x,y
        /// </summary>
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
