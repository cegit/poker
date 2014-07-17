using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHand.Models
{
    public interface IHand: IComparable<IHand>
    {
        IList<Card> Cards { get; }
        HandType Kind { get; set; }
        string PlayerName { get; set; }
    }
    
}