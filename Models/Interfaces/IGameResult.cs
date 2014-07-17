using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHand.Models
{
    public interface IGameResult
    {
        bool IsATie { get; }
        IList<IHand> Winners { get; }
        IHand Winner { get; }
    }
    
}