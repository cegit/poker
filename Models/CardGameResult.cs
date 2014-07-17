using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerHand.Models
{
    public class CardGameResult: IGameResult
    {
        public CardGameResult(IList<IHand> winningHands)
        {
            Winners = winningHands;
            IsATie = Winners.Count > 1;
        }

        public bool IsATie { get; private set; }

        public IList<IHand> Winners { get; private set; }

        public IHand Winner { get { return IsATie ? null : Winners.First(); } }

        public override string ToString()
        {
            var numOfWinners = Winners.Count;
            if (numOfWinners == 0) return "No hands found.";

            if (!IsATie)
                return string.Format("Winning Hand:  {0}", Winners.First());

            if (numOfWinners == 2)
                return string.Format("Tie: {0} - {1}", Winners[0], Winners[1]);

            var sb = new StringBuilder();
            sb.AppendFormat("{0}-way Tie:", numOfWinners);
            foreach (var winner in Winners)
                sb.AppendFormat(" {0} -", winner);

            sb.Length = sb.Length - 2;
            return sb.ToString();
        }
    }
}