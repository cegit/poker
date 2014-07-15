using System.Collections.Generic;

namespace PokerHand.Models
{
    public interface ICardGame
    {

        IHand Deal( string selectedCards="");
        int CardsInHand { get; }
        IEnumerable<IHand> GetWinners(IEnumerable<IHand> hands);
    }
}