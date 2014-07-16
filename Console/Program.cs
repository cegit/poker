namespace PokerHand.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
            var argHandler = new ArgumentHandler();
            argHandler.Handle(args);
        }
    }
}