using System;
using System.Linq;

namespace PokerHand.Models
{
    public class PokerGame : CardGame
    {
        public PokerGame()
        {
            CardsInHand = 5;
        }

        private static bool MatchStraight(IHand hand)
        {
            var start = hand.Cards.Skip(1).First();
            var diff = start.CardValue - hand.Cards.Last().CardValue;
            if (diff != 3) return false;

            var first = hand.Cards.First();
            if (!first.IsAnAce) return (first.CardValue - start.CardValue) == 1;
            if (start.Face != Face.King && start.Face != Face.Five) return false;
            if (start.Face == Face.Five)
            {
                hand.Cards.RemoveAt(0);
                hand.Cards.Add(first);
            }
            return true;
        }
        private static bool MatchFlush(IHand hand)
        {
            return hand.Cards.Select(c => c.Suit).Distinct().Count() == 1;
        }
        private static void MatchByCount(IHand hand)
        {
            var meta = hand.Cards
                .GroupBy(c => c.Face)
                .OrderByDescending(g => g.Count())
                .ThenByDescending(g => g.First().CardValue)
                .ToList();

            var firstCount = meta[0].Count();
            switch (meta.Select(m => m.Key).Count())
            {
                case 2:
                    hand.Kind = (firstCount == 4) ? HandType.FourOfAKind : HandType.FullHouse;
                    break;
                case 3:
                    hand.Kind = (firstCount == 3) ? HandType.ThreeOfAKind : HandType.TwoPair;
                    break;
                case 4:
                    hand.Kind = HandType.OnePair;
                    break;
                default:
                    return;
            }

            var i = 0;
            foreach (var card in meta.SelectMany(m => m))
                hand.Cards[i++] = card;
        }

        public override IHand Deal(string selectedCards="")
        {
            Setup();

            var cards = string.IsNullOrEmpty(selectedCards) ? Deck.Deal(5) : Deck.Deal(selectedCards);
            if (cards.Count != 5) throw  new ArgumentException("A Poker hand must contain 5 cards");
            cards = cards.OrderByDescending(card => card.CardValue).ToList();

            var hand = new Hand(cards);
            if (MatchStraight(hand)) hand.Kind = HandType.Straight;
            if (MatchFlush(hand)) hand.Kind = HandType.Flush;
            if (hand.Kind < HandType.StraightFlush) MatchByCount(hand);
            return hand;
        }
    }
}