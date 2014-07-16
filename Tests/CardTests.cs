using System;
using System.Linq;
using NUnit.Framework;

namespace PokerHand.Models.Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void CreateAceOfSpacesFromString()
        {
            var aceSpades = new Card("AS");

            Assert.That(Face.Ace, Is.EqualTo(aceSpades.Face));
            Assert.That(Suit.Spade, Is.EqualTo(aceSpades.Suit));
        }

        [Test]
        [TestCase("22S", "Face could not be matched")]
        [TestCase("12S", "Face could not be matched")]
        [TestCase("4B", "Suit could not be matched")]
        public void InvalidStringShouldThrowException(string cardValue, string message)
        {
            Assert.Throws<ArgumentException>(() => new Card(cardValue), message);
        }

        [Test]
        public void AceSpadeIsNotEqualToTheAceOfHearts()
        {
            var aceSpade = new Card("AS");
            var aceHeart = new Card("AH");

            Assert.AreNotEqual(aceHeart, aceSpade);
        }

        [Test]
        public void AceSpadeIsValuedSameAsAceOfHearts()
        {
            var aceSpade = new Card(Face.Ace, Suit.Spade);
            var aceHeart = new Card(Face.Ace, Suit.Heart);

            Assert.AreEqual(0, aceSpade.CompareTo(aceHeart));
        }

        [Test]
        public void NineOfDiamondsIsLessThanKingOfHearts()
        {
            var nineDiamond = new Card(Face.Nine, Suit.Diamond);
            var aceHeart = new Card(Face.Ace, Suit.Heart);
            Assert.IsTrue(nineDiamond < aceHeart);
        }

        [Test]
        public void SortListOfCards()
        {
            var cards = new[]
            {
                new Card(Face.Nine, Suit.Diamond),
                new Card(Face.Ace, Suit.Spade),
                new Card(Face.Three, Suit.Club),
                new Card(Face.Queen, Suit.Heart),
                new Card(Face.Jack, Suit.Diamond)
            }.ToList();

            cards.Sort();

            Assert.IsTrue(
                cards.SequenceEqual(new[]
                {
                    new Card(Face.Ace, Suit.Spade),
                    new Card(Face.Queen, Suit.Heart),
                    new Card(Face.Jack, Suit.Diamond),
                    new Card(Face.Nine, Suit.Diamond),
                    new Card(Face.Three, Suit.Club)
                }));

        }
    }
}