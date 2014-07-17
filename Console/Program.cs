using PokerHand.Models;
using NDesk.Options;
using System;

namespace PokerHand.Console
{
    class Program
    {
        private static void Main(string[] args)
		{
			try {
				var argHandler = new ArgumentHandler (new CardGameFactory ());
				argHandler.Handle (args);
			} catch (OptionException e) {
				System.Console.Write ("pokerhand: ");
				System.Console.WriteLine (e.Message);
				System.Console.WriteLine ("Try `pokerhand --help' for more information.");
			}
		}
    }
}