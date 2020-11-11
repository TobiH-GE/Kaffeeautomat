namespace Kaffeeautomat
{
    abstract class UI
    {
        public abstract void PrintStatus(string str);
        public abstract void PrintInfo(string str);
        public abstract void Start(Automat automat);
        public abstract void WaitForInput();
    }
}