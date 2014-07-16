namespace PokerHand.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NDesk.Options;
    using Models;

    public class ArgumentHandler
    {
        public void Handle(string[] args)
        {
            ICardGame game = null;
            var showHelp = false;
            var printCards = false;
            var players = new List<string>();

            var cardsText = "";
            Action handleCards = null;

            // pokerhands -texas:[AD KH 8C 6S JC] -p:{NAME}=[2H 3C] -p:[4D 4H] -p:{NAME}=[9S 8C]
            // pokerhands -p:[2H 3C 7C 8S 6D] -p:[4D 4H] -p:[9S 8C]

            var p = new OptionSet
            {
                {
                    "poker",
                    "5 Card Poker. A player hands is 5 cards.",
                    v =>
                    {
                        if (game != null) throw new OptionException("Game was already set.", "poker");
                        game = new PokerGame();
                    }
                },
                {
                    "texas:",
                    "Texas Hold'Em Poker, set {[5 CARDS]}. A player hands is 2 cards.",
                    v =>
                    {
                        if (game != null) throw new OptionException("Game was already set.", "texas");
                        handleCards = () => game = new TexasHoldemGame(cardsText);
                    }
                },
                {
                    "p|player:", 
                    "a {[HAND]} to compare",
                    v =>
                    {
                        cardsText += v + " ";
                        handleCards = () => players.Add(cardsText);
                    }
                },
                {"printCards", "Display all the cards in a deck", v => printCards = true},
                {"h|?|help", "show this message and exit", v => showHelp = v != null},
                {
                    "<>", v =>
                    {
                        cardsText += v + " ";
                        if (!v.EndsWith("]")) return;
                        if (handleCards == null)
                            throw new OptionException(
                                "unknown arguments. Please use -h for proper command line argument instructions.","-p|player");
                        handleCards.Invoke();
                        cardsText = "";
                    }
                }
            };

            try
            {
                p.Parse(args);

                if (showHelp)
                    ShowHelp(p);

                else if (printCards)
                    PrintAllCards();
                
                else
                {
                    PlayGame(game, players.ToArray());
                }
            }
            catch (OptionException e)
            {
                Console.Write("pokerhand: ");
                Console.WriteLine(e.Message);
                Console.WriteLine("Try `pokerhand --help' for more information.");
            }
        }

        private void PlayGame(ICardGame game, params string[] players)
        {
            if (players.Count() < 2)
                throw new OptionException("Not enough players specified.", "-p|player");

            if (game == null) game = new PokerGame();

            Console.WriteLine();
            Console.WriteLine("--------------------------------------------");
            var hands = players.Select(game.Deal).ToList();
            foreach (var hand in hands) Console.WriteLine(hand);
            Console.WriteLine("--------------------------------------------");

            var result = game.GetWinners(hands);
            Console.WriteLine(result);
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        private void ShowHelp(OptionSet p)
        {
            Console.WriteLine("Usage: PokerHands [SETTINGS]+ message");
            Console.WriteLine("Select between standard 5-card poker(default) and Texas Hold'Em.");
            Console.WriteLine("Determine the winning hand from a multiple set of hands.");
            Console.WriteLine();
            Console.WriteLine("SETTINGS:");
            p.WriteOptionDescriptions(Console.Out);
        }
        private void PrintAllCards()
        {
            var deck = new Deck();
            foreach (var g in deck.Cards.GroupBy(c => c.Suit))
            {
                foreach (var card in g)
                    Console.Write("{0} ", card);
                Console.WriteLine();
            }
        }
    }
}