using System;

namespace PokerHand.Models
{
	public interface ICardGameFactory {
		ICardGame Resolve (string name, Action<ICardGame> postSetup=null);
	}
	
}
