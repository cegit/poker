namespace PokerHand.Console
{
    using System;
    using System.Linq;
    using Models;

    class Program
    {
        static void Main(string[] args)
        {
            var deck = new Deck();

            foreach (var g in deck.Cards.GroupBy(c=>c.Suit))
            {
                foreach (var card in g)
                {
                    Console.Write("{0} ", card);                    
                }
                Console.WriteLine();

            }
            Console.ReadKey();
        }
    }
}
