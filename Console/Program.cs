namespace PokerHand.Console
{
    using System;
    using System.Linq;
    using Models;

    class Program
    {
        static void Main(string[] args)
        {
            ICardGame game;

            if (!args.Any() ||
                args.Any(a => a == "-h") ||
                args.Any(a => a == "-help"))
                ShowHelp();

            else if (args.Any(a => a == "-printCards"))
            {
                var deck = new Deck();
                foreach (var g in deck.Cards.GroupBy(c => c.Suit))
                {
                    foreach (var card in g)
                        Console.Write("{0} ", card);
                    Console.WriteLine();
                }
            }
            else
            {
                var gameDef = args.FirstOrDefault(a => a.StartsWith("-g"));
                if (string.IsNullOrEmpty(gameDef) == false)
                {
                    gameDef = gameDef.ToLower().Replace("-g", "").Replace("[", "").Replace("]", "");
                }
                if (gameDef != "texas")
                {
                    gameDef = "poker"; 
                    game = new PokerGame();
                }
                else
                {
                    game = new TexasHoldemGame();                    
                }
                Console.WriteLine("game mode = {0}", gameDef);
            }
            Console.ReadKey();
        }

        static void ShowHelp()
        {
            Console.WriteLine("-p[] => set player hand. ex -p[AS KH QD JC 10H]");
            Console.WriteLine("     => use -p to define multiple player hands.");
            Console.WriteLine("");
            Console.WriteLine("-g[?] or -game[?] => set game mode. ? = [poker, texas]");
            Console.WriteLine("                  => if not given default is 'poker'");
            Console.WriteLine("");
            Console.WriteLine("-c[] => if texas holdem, use -c to specify the 5 community cards");
            Console.WriteLine("");
            Console.WriteLine("-h or -help => show help");
            
        }
    }
}
