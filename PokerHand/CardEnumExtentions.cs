using System;
using System.Globalization;

namespace PokerHand.Models
{
    public static class CardEnumExtentions
    {
        public static Suit ToSuit(this char val)
        {
            return ToSuit(val.ToString(CultureInfo.InvariantCulture));
        }
        public static Suit ToSuit(this string val)
        {
            switch (val)
            {
                case "C": return Suit.Club;
                case "D": return Suit.Diamond;
                case "H": return Suit.Heart;
                case "S": return Suit.Spade;
            }
            throw new ArgumentException("Suit could not be matched");
        }

        public static Face ToFace(this string val)
        {
            switch (val)
            {
                case "2": return Face.Two;
                case "3": return Face.Three;
                case "4": return Face.Four;
                case "5": return Face.Five;
                case "6": return Face.Six;
                case "7": return Face.Seven;
                case "8": return Face.Eight;
                case "9": return Face.Nine;
                case "10": return Face.Ten;
                case "J": return Face.Jack;
                case "Q": return Face.Queen;
                case "K": return Face.King;
                case "A": return Face.Ace;
            }
            throw new ArgumentException("Face could not be matched");
        }
    }
}