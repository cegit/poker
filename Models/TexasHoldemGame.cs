using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand.Models
{
    public class TexasHoldemGame : PokerGame
    {
        private IList<Card> _communityCards; 

        private readonly string _community;
        public TexasHoldemGame(string commmunity) : this()
        {
            _community = commmunity;
        }
        public TexasHoldemGame()
        {
            CardsInHand = 2;
        }

        private readonly IList<Hand> _possibilities = new List<Hand>();

        protected override bool MatchStraight(IHand source)
        {
            var temp = new List<Hand>();

            var cards = source.Cards.Concat(source.Cards.Take(2)).ToList();

            for (var i = 0; i < 5; i++)
            {
                var hand = new Hand(cards.Skip(i).Take(5));
                if (!base.MatchStraight(hand)) continue;
                hand.Kind = HandType.Straight;
                temp.Add(hand);
            }

            var found = GetWinners(temp).Cast<Hand>().FirstOrDefault();
            if (found == null) return false;
            _possibilities.Add(found);
            return true;
        }

        protected override bool MatchFlush(IHand hand)
        {
            var found = hand.Cards
                .GroupBy(c => c.Suit)
                .Where(g => g.Count() >= 5)
                .Select(g => new Hand(g.OrderByDescending(c=>c.CardValue).Take(5)))
                .FirstOrDefault();

            if (found == null) return false;
            found.Kind = HandType.Flush;
            _possibilities.Add(found);
            return true;
        }

        protected override void Setup(Action extraSetup=null)
        {
            _possibilities.Clear();
            base.Setup(() =>
            {
                _communityCards = (string.IsNullOrEmpty(_community)
                    ? Deck.Deal(5)
                    : Deck.Deal(_community))
                    .OrderByDescending(c => c.CardValue)
                    .ToList();
            });
        }

        public override IHand Deal(string selectedCards="")
        {
            Setup();

            var cards = string.IsNullOrEmpty(selectedCards) ? Deck.Deal(2) : Deck.Deal(selectedCards);
            if (cards.Count != 2) throw new ArgumentException("A Texas Hold'em hand must contain 2 cards");
            
            var fullTexasHand = new Hand(cards.Union(_communityCards));

            if (MatchStraight(fullTexasHand))
            {
                if (base.MatchFlush(_possibilities[0]))
                {
                    _possibilities[0].Kind = HandType.Flush;
                    return _possibilities[0];
                }
            }

            MatchFlush(fullTexasHand);

            for (var i = 0; i < 3; i++)
            {
                var hand = new Hand(fullTexasHand.Cards.Skip(i).Take(5));
                {
                    base.MatchByCount(hand);
                    _possibilities.Add(hand);
                }
            }

            return GetWinners(_possibilities).First();
        }
    }
}