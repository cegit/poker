using System;
using Moq;
using NUnit.Framework;
using NDesk.Options;
using PokerHand.Console;
using PokerHand.Models;

namespace PokerHand.Tests.Console
{
	[TestFixture]
    public class ArgumentHandlerTests
    {
		[Test]
		public void VerifyThatFactoryReturnsPoker() {
			//ASSIGN
			var mockGame = new Mock<ICardGame> ();

			var mockFactory = new Mock<ICardGameFactory> ();
			mockFactory.Setup (f => f.Resolve("poker", It.IsAny<Action<ICardGame>>())).Returns(mockGame.Object).Verifiable();

			var args = new[] {"-poker", "-p", "[4C", "7S", "6C","AD", "2D]"};
			var argHandler = new ArgumentHandler (mockFactory.Object);

			//ACT
			Assert.Throws<OptionException> (() => argHandler.Handle (args), "unknown arguments. Please use -h for proper command line argument instructions.");

			//ASSERT
			mockFactory.VerifyAll();
		}

		[Test]
		public void VerifyThatFactoryReturnsTexasHoldEm() {
			//ASSIGN
			var mockGame = new Mock<ICardGame> ();

			var mockFactory = new Mock<ICardGameFactory> ();
			mockFactory.Setup (f => f.Resolve("texas", It.IsAny<Action<ICardGame>>())).Returns(mockGame.Object).Verifiable();

			var args = new[] {"-texas", "[AS", "3D", "5C", "AD", "KH]", "-p", "[4C", "2D]"};
			var argHandler = new ArgumentHandler (mockFactory.Object);

			//ACT
			Assert.Throws<OptionException> (() => argHandler.Handle (args), "unknown arguments. Please use -h for proper command line argument instructions.");

			//ASSERT
			mockFactory.VerifyAll();
		}

		[Test]
		public void VerifyThatPokerGamePlaysWithTwoPlayers() {
			//ASSIGN
			var mockHand = new Mock<IHand> ();

			var mockGame = new Mock<ICardGame> ();
			mockGame.Setup (g => g.Deal (" [AS 3D 5C AD KH] ")).Returns (mockHand.Object).Verifiable ();
			mockGame.Setup (g => g.Deal (" [4C 7S 6C AD 2D] ")).Returns (mockHand.Object).Verifiable ();

			var mockFactory = new Mock<ICardGameFactory> ();
			mockFactory.Setup (f => f.Resolve("poker", It.IsAny<Action<ICardGame>>())).Returns(mockGame.Object).Verifiable();

			var args = new[] {"-poker", "-p", "[AS", "3D", "5C", "AD", "KH]", "-p", "[4C", "7S", "6C","AD", "2D]"};
			var argHandler = new ArgumentHandler (mockFactory.Object);

			//ACT
			argHandler.Handle (args);

			//ASSERT
			mockGame.VerifyAll ();
			mockFactory.VerifyAll();
		}

		[Test]
		[TestCase(1, new string[] {},  TestName="With no parameters" )]
		[TestCase(1, new string[] {"-p"}, TestName="With out specifying a hand for a player"  )]
		[TestCase(1, new string[] {"-p", "[AS", "3D", "5C", "AD", "KH]"}, TestName="With 1 Player" )]
		public void ThrowsExceptionIfLessThanTwoPlayers(int x,  string[] args )
		{
			var argHandler = new ArgumentHandler(new CardGameFactory());
			Assert.Throws<OptionException> (() => argHandler.Handle (args), "Not enough players specified.");
		}


    }
}