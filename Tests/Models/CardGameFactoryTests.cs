using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PokerHand.Models;

namespace PokerHand.Tests.Models
{
	[TestFixture]
	public class CardGameFactoryTests
	{
		[Test]
		public void ResolveWithoutParametersThrowsArgumentException()
		{
			var factory = new CardGameFactory ();
			Assert.Throws<ArgumentException> (() =>  factory.Resolve(""), "unknown card game type.");
		}

		[Test]
		public void ResolveWithUnknownNameThrowsArgumentException()
		{
			var factory = new CardGameFactory ();
			Assert.Throws<ArgumentException> (() =>  factory.Resolve("Should not find a match"), "unknown card game type.");
		}

		[Test]
		[TestCase("poker", typeof(PokerGame))]
		[TestCase("texas", typeof(TexasHoldemGame))]
		public void ResolveWithNameOfGameReturnsCorrectGame(string name, Type gameType)
		{
			var factory = new CardGameFactory ();
			var game = factory.Resolve (name);

			Assert.IsNotNull (game);
			Assert.AreEqual(gameType, game.GetType());
		}
	}
}