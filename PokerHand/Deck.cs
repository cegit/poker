﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand.Models
{
    public class Deck
    {
        private readonly Random _random = new Random();

        public Deck()
        {
            LoadCards();
        }
        public List<Card> Cards { get; private set; }

        public IList<Card> Deal(string selected)
        {
            var query = selected
                .Replace("[", "")
                .Replace("]", "")
                .Split(new[] {" ", ","}, StringSplitOptions.RemoveEmptyEntries);
                
            var result = new List<Card>();
            foreach (var q in query)
            {
                var card = Cards.FirstOrDefault(c => c.Equals(q));
                if (card == null) throw new Exception(string.Format("Card '{0}' not found in deck", q));
                result.Add(card);
                Cards.Remove(card);
            }
            return result;
        }
        public IList<Card> Deal(int cardsInHand)
        {
            var delt = Cards.Take(cardsInHand).ToList();
            Cards.RemoveRange(0, cardsInHand);
            return delt;
        }

        public void Shuffle()
        {
            if (Cards.Count != 52) LoadCards();

            for (var n = Cards.Count - 1; n > 0; --n)
            {
                var k = _random.Next(n + 1);
                var temp = Cards[n];
                Cards[n] = Cards[k];
                Cards[k] = temp;
            }
        }
        
        private void LoadCards()
        {
            Cards = new List<Card>();
            foreach (var suit in Enum.GetValues(typeof(Suit)).Cast<Suit>())
                foreach (var face in Enum.GetValues(typeof(Face)).Cast<Face>())
                    Cards.Add(new Card(face, suit));
        }
    }
}