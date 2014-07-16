using System.Linq;
using NUnit.Framework;
using PokerHand.Models;

namespace PokerHand.Tests.Models
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void DeckShouldHave52Cards()
        {
            var deck = new Deck();
            Assert.AreEqual(52, deck.Cards.Count);
        }

        [Test]
        public void DeckCanDealSelectedCards()
        {
            var deck = new Deck();
            var cards = deck.Deal("[3H,  3C, 3S,AH,    JD]");
            Assert.That(cards.Count, Is.EqualTo(5));
        }

        [Test]
        public void DeckShouldHaveOnlyOneOfEachCard()
        {
            var deck = new Deck();

            var distinctList = deck.Cards.Distinct().ToList();
            Assert.IsTrue(distinctList.SequenceEqual(deck.Cards));
        }

		[Test]
		public void DeckCanHaveWildCards()
		{
			var deck = new Deck ();
			var anyWildCards = deck.Cards.Any (c => c.IsWild);

			deck.MakeCardWild(Face.Two).Shuffle();

			Assert.IsFalse (anyWildCards);
			Assert.That (deck.Cards.Count (c => c.IsWild), Is.EqualTo (4));
		}

		[Test]
		public void DeckCanRevertWildCards()
		{
			var deck = new Deck ().MakeCardWild (Face.Ace);
			Assert.That (deck.Cards.Count (c => c.IsWild), Is.EqualTo (4));

			deck.RevertWildCard();
			Assert.That (deck.Cards.Count (c => c.IsWild), Is.EqualTo (0));
		}

        [Test]
        public void DeckShouldBeRandomizedAfterShuffle()
        {
            var deck = new Deck();

            var originalCardsOrder = deck.Cards.ToList();
            deck.Shuffle();
            Assert.IsFalse(deck.Cards.SequenceEqual(originalCardsOrder));
        }
    }
}
