﻿using System;
using System.Linq;

namespace PokerHand.Models
{
    public class Card : IComparable<Card>
    {
		protected readonly int _equalityValue;

		public Card(Face face, Suit suit, bool isWild=false) 
        {
            Face = face;
            Suit = suit;
            _equalityValue = (int)Face * (int)Suit;
			IsWild = isWild;
        }

        public Card(string val)
        {
			val = val.ToUpper();
            Suit = val.Last().ToSuit();
            val = val.Remove(val.Length - 1);
            Face = val.ToFace();
            _equalityValue = (int)Face * (int)Suit;
        }

        public Face Face { get; private set; }
		public Suit Suit { get; private set; }

        public int CardValue { get { return (int)Face; } }

        public bool IsAnAce { get { return Face == Face.Ace; } }
		public bool IsWild { get; private set;}

        public override string ToString()
        {
            return string.Format("{0}{1}", Face.GetDescription(), Suit.GetDescription());
        }
        
        #region Equals
        protected bool Equals(Card other)
        {
            return other._equalityValue == _equalityValue;
        }
        protected bool Equals(string other)
        {
            try
            {
				other = other.ToUpper();
                Suit = other.Last().ToSuit();
                var val = other.Remove(other.Length - 1);
                Face = val.ToFace();
                return _equalityValue == ((int) Face*(int) Suit);
            }
            catch 
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return ((int)Suit * 397) ^ (int)Face;
            }
        }
        public static bool operator ==(Card left, Card right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(Card left, Card right)
        {
            return !Equals(left, right);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var s = obj as string;
            if (s != null) return Equals(s);

            return obj.GetType() == GetType() && Equals((Card)obj);
        }
        #endregion

        #region IComparable
        int IComparable<Card>.CompareTo(Card other)
        {
            return CompareTo(other);
        }
        public int CompareTo(Card other)
        {
            return other == null ? 1 : other.CardValue.CompareTo(CardValue);
        }
        public static bool operator >(Card c1, Card c2)
        {
            return c1.CardValue > c2.CardValue;
        }
        public static bool operator >=(Card c1, Card c2)
        {
            return c1.CardValue >= c2.CardValue;
        }
        public static bool operator <(Card c1, Card c2)
        {
            return c1.CardValue < c2.CardValue;
        }
        public static bool operator <=(Card c1, Card c2)
        {
            return c1.CardValue < c2.CardValue;
        }
        #endregion
    }
}
