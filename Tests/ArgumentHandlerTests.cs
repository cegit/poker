using NUnit.Framework;
using PokerHand.Console;

namespace PokerHand.Tests.Console
{
	[TestFixture]
    public class ArgumentHandlerTests
    {
		[Test]
        public void TestHandlingArguments()
        {
            var argHandler = new ArgumentHandler();
            var args = new[] {"-texas", "[AS", "3D", "5C", "AD", "KH]", "-p", "[4C", "2D]"};

            argHandler.Handle(args);
        }
    }
}