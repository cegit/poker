using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace PokerHand.Models.Tests
{
    [TestFixture]
    public class PokerGameTests
    {
        [Test]
        [TestCase("3C 3H 3S 8C 8H", "2H 4H 4D 4S 4C", false, "Four Of A Kind")]
        [TestCase("3C 3H 3S 8C 8H", "2H 4H 6H 9H JH", true, "Full House")]
        [TestCase("2H 4H 6H 8H JH", "2D 3D 4C 5S 6C", true, "Flush")]
        [TestCase("3C 3H 3S 2C 8H", "6H 5D 4H 3D 2D", false, "Straight High Card: 6")]
        [TestCase("3C 3H 3S 2C 8H", "2H 2D 4H 4S 6C", true, "Three Of A Kind")]
        [TestCase("JC 3H QS 2C 8H", "2H 2D 4H 4S 6C", false, "Two Pairs 4s & 2s")]
        [TestCase("JC 3H 3S 2C 8H", "2H 3D 5S 9C KD", true, "Pair Of 3s")]
        [TestCase("2H 3D 5S 9C KD", "2C 3H 4S 8C AH", false, "High Card: A")]

        // Test when both hands have the same rank but the high-card breaks the “score-tie"
        [TestCase("2H 3H 4H 5H 6H", "3C 4C 5C 6C 7C", false, "Straight Flush High Card: 7")]
        [TestCase("4H 4D 4S 4C 8H", "2H 3H 3D 3S 3C", true, "Four Of A Kind")]
        [TestCase("3C 3H 3S 8C 8H", "4C 4H 4S JH JD", false, "Full House")]
        [TestCase("2H 4H 6H 8H JH", "2C 4C 7C 8C JC", false, "Flush")]
        [TestCase("4C 5H 6S 7C 8H", "6H 5D 4H 3S 2C", true, "Straight High Card: 8")]
        [TestCase("3C 3H 3S 6C 8H", "2H 2D 2C 4S QC", true, "Three Of A Kind")]
        [TestCase("JC 3H 3S 2C 8H", "2H 3D 3C 9C KD", false, "Pair Of 3s")]
        [TestCase("2H 3D 5S 9C KD", "2C 3H 4S 8C AH", false, "High Card: A")]
        [TestCase("2C 2H 4S 4C 8H", "2S 2D 4H 4D 6C", true, "Two Pairs 4s & 2s")]

        // Test a straight-forward winner
        [TestCase("2H 3H 4H 5H AH", "3C 3D 4S 8C AD", true, "Straight Flush High Card: 5")]
        [TestCase("3C 3H 3S 2D 8H", "AH 5D 4H 3D 2C", false, "Straight High Card: 5")]

        // Test when both hands have the same rank but the high-card breaks the “score-tie"
        [TestCase("2H 3H 4H 5H 6H", "AC 2C 3C 4C 5C", true, "Straight Flush High Card: 6")]
        [TestCase("AC 2S 3C 4D 5C", "6H 5D 4H 3S 2C", false, "Straight High Card: 6")]
        public void JaimeTests(string black, string white, bool blackWins, string handMessage)
        {
            var poker = new PokerGame();
            var hands = new List<IHand> {poker.Deal(black), poker.Deal(white)};
            var result = poker.GetWinners(hands);

            var didBlackWin = hands[0].Equals(result.Winner);

            Assert.IsFalse(result.IsATie);
            Assert.AreEqual(blackWins, didBlackWin);
            Assert.IsTrue(result.Winner.ToString().Contains(handMessage));
        }

        [Test]
        public void CheckForATie()
        {
            var poker = new PokerGame();
            var hands = new List<IHand> { poker.Deal("2H 3D 5S 9C KD"), 
                poker.Deal("2C 3H 5C 9H KC") };
            var result = poker.GetWinners(hands);

            Assert.IsTrue(result.IsATie);
        }

        [Test]
        public void CheckForAThreeWayTie()
        {
            var poker = new PokerGame();
            var hands = new List<IHand> { 
                poker.Deal("2H 3H 4H 5H 6H"), 
                poker.Deal("2D 3D 4D 5D 6D"),
                poker.Deal("2S 3S 4S 5S 6S"),
            };
            var result = poker.GetWinners(hands);

            Assert.IsTrue(result.IsATie);
            Assert.AreEqual(result.Winners.Count, 3);
        }

        [Test]
        [TestCase(0, HandType.ThreeOfAKind, "3H, 3C, 3S,AH, JD", "AC, AD, KS, KC, 3D", "4H,5D,7C,JS,QH" )]
        [TestCase(2, HandType.Straight, "[3H, 2C, 3S, AH, JD]", "[AC, AD, KS, KC, 3D]", "4H, 5D, 6H, 7C, 8H")]
        [TestCase(1, HandType.StraightFlush, "[2H 3H 4H 5H QH]", "[2D 3D 4D 5D AD]", "2S 3S 4S 5S KS")]
        public void DetermineWinningHand(int win, HandType winningHand, params string[] handVals)
        {
            //ASSIGN
            var poker = new PokerGame();

            //ACT
            var hands = handVals.Select(poker.Deal).ToList();
            var result = poker.GetWinners(hands);

            //ASSERT
            Assert.That(result.Winner.Kind, Is.EqualTo(winningHand));
            Assert.That(result.Winner, Is.EqualTo(hands[win]));
        }

        [Test]
        [TestCase(HandType.OnePair, "9D AS 9C QH JD")]
        [TestCase(HandType.TwoPair, "9D KS 9C QH KD")]
        [TestCase(HandType.ThreeOfAKind, "9D KS 8C KH KD")]
        [TestCase(HandType.FullHouse, "9D AS AC 9H AD")]
        [TestCase(HandType.FourOfAKind, "9D AS AC AH AD")]
        [TestCase(HandType.Straight, "9D JH 8C 10D 7H")]
        [TestCase(HandType.Straight, "[3D 4H 2C AD 5H]")] //ace low
        [TestCase(HandType.Straight, "JD 10S QC KD AH")] //ace high
        [TestCase(HandType.StraightFlush, "9D JD 8D 10D QD")]
        [TestCase(HandType.RoyalFlush, "JD KD 10D QD AD")]
        public void TestKind(HandType kind, string selectedCards)
        {
            var poker = new PokerGame();
            var hand = poker.Deal(selectedCards);
            Assert.That(kind, Is.EqualTo(hand.Kind));
        }

        [Test]
        public void Check_OneThousand_RandomHands()
        {
            for (var g = 0; g < 1000; g++)
            {
                var poker = new PokerGame();
                var hands = new List<IHand>
                {
                    poker.Deal(), poker.Deal(), poker.Deal(), poker.Deal(), poker.Deal(), poker.Deal()
                };

                var result = poker.GetWinners(hands);

                if (!result.IsATie)
                {
                    var winner = result.Winner;
                    Assert.That(winner, Is.Not.Null);
                    foreach (var hand in hands)
                    {
                        Assert.That(winner, Is.GreaterThanOrEqualTo(hand));
                    }
                }
                for (var h = 0; h < result.Winners.Count - 1; h++)
                    Assert.IsTrue(result.Winners[h].Kind >= result.Winners[h + 1].Kind);
            }
        }
    }
}