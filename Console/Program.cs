using PokerHand.Models;

namespace PokerHand.Console
{
    class Program
    {
        private static void Main(string[] args)
        {
			var argHandler = new ArgumentHandler(new CardGameFactory());
            argHandler.Handle(args);
        }
    }
}