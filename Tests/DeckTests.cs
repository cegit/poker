using System.Linq;
using NUnit.Framework;

namespace PokerHand.Models.Tests
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
        public void DeckShouldBeRandomizedAfterShuffle()
        {
            var deck = new Deck();

            var originalCardsOrder = deck.Cards.ToList();
            deck.Shuffle();
            Assert.IsFalse(deck.Cards.SequenceEqual(originalCardsOrder));
        }
    }
}
