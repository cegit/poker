using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PokerHand.Console.Tests
{
    [TestClass]
    public class ArgumentHandlerTests
    {
        [TestMethod]
        public void TestHandlingArguments()
        {
            var argHandler = new ArgumentHandler();
            var args = new[] {"-texas", "[AS", "3D", "5C", "AD", "KH]", "-p", "[4C", "2D]"};

            argHandler.Handle(args);
        }
    }
}
