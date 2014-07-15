using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PokerHand.Models;

namespace PokerHand.Tests
{
    [TestFixture]
    public class TexasHoldemGameTests
    {
        [Test]
        [TestCase("AH KD 2S 10D 3D", 0, HandType.ThreeOfAKind, "[3S, 3C]", "[AS, 6D]", "[7H, 5D]")]
        [TestCase("AH KD 2D 10D 3D", 2, HandType.Flush, "[3S, 3C]", "[4C, 5D]", "[8C, 8D]")]
        [TestCase("AD KD 2S 10D 3D", 1, HandType.RoyalFlush, "[3S, 3C]", "[QD, JD]", "[8C, 8D]")]
        public void DetermineWinningHand(string community, int win, HandType winningHand, params string[] handVals)
        {
            //ASSIGN
            var texas = new TexasHoldemGame(community);

            //ACT
            var hands = handVals.Select(texas.Deal).ToList();
            var winner = texas.GetWinners(hands).Single();


            //ASSERT
            Assert.That(winner.Kind, Is.EqualTo(winningHand));
            Assert.That(winner, Is.EqualTo(hands[win]));
        }

        [Test]
        [TestCase(HandType.RoyalFlush, Face.Ace, "[AH JH 9C 10H 5D]", "[QH KH]")]
        [TestCase(HandType.StraightFlush, Face.Six, "[AD 2D 3D 4D 5D]", "[6D JD]")]
        [TestCase(HandType.FourOfAKind, Face.Seven, "[7H 7D 3C 4D 5D]", "[7S 7C]")]
        [TestCase(HandType.FullHouse, Face.Seven, "[7H 7D QC 4D QD]", "[7S 2C]")]
        [TestCase(HandType.Flush, Face.Jack, "[AH 2D 3C 4D 5D]", "[6D JD]")]
        [TestCase(HandType.Straight, Face.Jack, "[8H 2D 6C 7S 9D]", "[10D JD]")]
        [TestCase(HandType.ThreeOfAKind, Face.Eight, "[8H 2D 6C 8S 9D]", "[8D JD]")]
        [TestCase(HandType.TwoPair, Face.Queen, "[8H QD 6C 8S 9D]", "[4D QH]")]
        [TestCase(HandType.OnePair, Face.Queen, "[8H QD 6C 7S 9D]", "[4D QH]")]
        [TestCase(HandType.HighCard, Face.Queen, "[8H JD 6C 7S 9D]", "[4D QH]")]
        public void TestKind(HandType kind, Face highCard, string community, string playerHand)
        {
            //ASSIGN
            var texas = new TexasHoldemGame(community);

            //ACT
            var hand = texas.Deal(playerHand);
            
            Assert.That(kind, Is.EqualTo(hand.Kind));
            Assert.AreEqual(highCard, hand.Cards.First().Face);
        }

        [Test]
        public void Check_OneThousand_RandomHands()
        {
            for (var g = 0; g < 1000; g++)
            {
                var texas = new TexasHoldemGame();
                var hands = new List<IHand>
                {
                    texas.Deal(), texas.Deal(), texas.Deal(), texas.Deal(), texas.Deal(), texas.Deal()
                };
                var winning = texas.GetWinners(hands).ToList();

                for (var h = 0; h < winning.Count - 1; h++)
                    Assert.IsTrue(winning[h].Kind >= winning[h + 1].Kind);
            }
        }

    }
}