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

        public IEnumerable<IHand> GetWinners(IEnumerable<IHand> hands)
        {
            IHand prev = null;
            foreach (var hand in hands.OrderByDescending(h => h))
            {
                if (prev == null)
                {
                    prev = hand;
                    yield return hand;
                }
                else if (hand.Equals(prev))
                    yield return hand;

                else
                    break;
            }
        }
    }
}