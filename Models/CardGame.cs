using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand.Models
{
    public abstract class CardGame : ICardGame
    {
        private bool _gameStarted;
        protected readonly Deck Deck;

        protected CardGame()
        {
            Deck = new Deck();
        }

        protected virtual void Setup(Action extraSetup=null)
        {
            if (_gameStarted) return;

            Deck.Shuffle();
            if (extraSetup != null) extraSetup();
            _gameStarted = true;
        }

        public abstract IHand Deal(string selectedCards = "");
        public  int CardsInHand { get; protected set; }

        public IGameResult GetWinners(IEnumerable<IHand> hands)
        {
            var winners = new List<IHand>();   
            foreach (var hand in hands.OrderByDescending(h => h))
            {
                if (!winners.Any() || winners.First().Equals(hand))
                    winners.Add(hand);
                else
                    break;
            }
            return new CardGameResult(winners);
        }
    }
}