using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerHand.Models
{
    public interface IHand: IComparable<IHand>
    {
        IList<Card> Cards { get; }
        HandType Kind { get; set; }
    }

    public class Hand : IHand
    {
        public Hand(IEnumerable<Card> cards)
        {
            Cards = cards.OrderByDescending(c=>c.CardValue).ToList();
        }
        public Hand(params Card[] cards):this(cards.ToList()) { }

        public IList<Card> Cards { get; private set; }

        private HandType _kind = HandType.HighCard;
        public HandType Kind
        {
            get { return _kind; }
            set
            {
                if (_kind == HandType.Straight && value == HandType.Flush ||
                    _kind == HandType.Flush && value == HandType.Straight)
                    _kind = Cards.First().IsAnAce ? HandType.RoyalFlush : HandType.StraightFlush;
                
                else if (value > _kind) 
                    _kind = value;
            }
        }

        public override string ToString()
        {
            switch (Kind)
            {
                case HandType.HighCard:
                    return string.Format("High Card: {0}",
                        Cards.First().Face.GetDescription());

                case HandType.Straight:
                case  HandType.StraightFlush:
                    return string.Format("{0} High Card: {1}", 
                        Kind.GetDescription(), 
                        Cards.First().Face.GetDescription());
                
                case HandType.OnePair:
                    return string.Format("Pair Of {0}s", Cards.First().Face.GetDescription());

                case HandType.TwoPair:
                    return string.Format("Two Pairs {0}s & {1}s",
                        Cards[0].Face.GetDescription(),
                        Cards[2].Face.GetDescription());

                default:
                    return string.Format("{0}", Kind.GetDescription());
            }
        }

        public int CompareTo(IHand other)
        {
            if (other == null) return 1;
            if (Kind > other.Kind) return 1;
            if (Kind < other.Kind) return -1;

            for (var i = 0; i < Cards.Count; i++)
            {
                if (Cards[i] > other.Cards[i]) return 1;
                if (Cards[i] < other.Cards[i]) return -1;
            }
            return 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Hand)obj);
        }
        protected bool Equals(Hand other)
        {
            return other != null && CompareTo(other) == 0;
        }
        public static bool operator ==(Hand left, Hand right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Hand left, Hand right)
        {
            return !Equals(left, right);
        }
    }
}