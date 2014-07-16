using System.Collections.Generic;

namespace PokerHand.Models
{
    public interface ICardGame
    {

        IHand Deal( string selectedCards="");
        int CardsInHand { get; }
        IGameResult GetWinners(IEnumerable<IHand> hands);
    }
}