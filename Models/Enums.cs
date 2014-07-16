using System.ComponentModel;

namespace PokerHand.Models
{
    public enum Face
    {
        [Description("2")]
        Two = 2,
        [Description("3")]
        Three = 3,
        [Description("4")]
        Four = 4,
        [Description("5")]
        Five = 5,
        [Description("6")]
        Six = 6,
        [Description("7")]
        Seven = 7,
        [Description("8")]
        Eight = 8,
        [Description("9")]
        Nine = 9,
        [Description("10")]
        Ten = 10,
        [Description("J")]
        Jack = 11,
        [Description("Q")]
        Queen = 12,
        [Description("K")]
        King = 13,
        [Description("A")]
        Ace = 14
    }
    public enum Suit 
    {
        [Description("C")] Club = 10,
        [Description("D")] Diamond = 100,
        [Description("H")] Heart = 1000,
        [Description("S")] Spade = 10000
    }
    public enum HandType
    {
        [Description("High Card")] HighCard = 1,
        [Description("Pair")] OnePair,
        [Description("Two Pair")] TwoPair,
        [Description("Three Of A Kind")] ThreeOfAKind,
        [Description("Straight")] Straight,
        [Description("Flush")] Flush,
        [Description("Full House")] FullHouse,
        [Description("Four Of A Kind")] FourOfAKind,
        [Description("Straight Flush")] StraightFlush,
        [Description("Royal Flush")] RoyalFlush,
		[Description("Five Of A Kind")] FiveOfAKind  // only possible with wild cards
    }
}