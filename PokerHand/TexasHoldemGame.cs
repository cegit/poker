using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand.Models
{
    public class TexasHoldemGame : CardGame
    {
        // the flop (3 cards)
        // the turn 1
        //the river 1
        private IList<Card> _theFlop;
        private Card _theTurn, _theRiver;
        public TexasHoldemGame() 
        {
            CardsInHand = 2;
        }

        protected override void Setup(Action extraSetup=null)
        {
            base.Setup(() =>
            {
                _theFlop = Deck.Deal(3);
                _theTurn = Deck.Deal(1).First();
                _theRiver = Deck.Deal(1).First();                
            });
        }

        public override IHand Deal(string selectedCards="")
        {
            var cards = Deck.Deal(2);

            var hand = new Hand(cards);

            return hand;
        }
    }
}