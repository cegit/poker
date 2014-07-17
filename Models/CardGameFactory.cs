using System;

namespace PokerHand.Models
{
	public class CardGameFactory: ICardGameFactory
	{

		public ICardGame Resolve(string name, Action<ICardGame> postSetup=null )
		{
			name = string.IsNullOrEmpty (name) ? "" : name.ToLower();
			ICardGame game = null;
			switch(name)
			{
			case "poker":
				game = new PokerGame ();
				break;

			case "texas":
				game = new TexasHoldemGame();
				break;
			
			default:
				throw new ArgumentException ("unknown card game type.", name);
			}

			if (postSetup != null)
				postSetup.Invoke (game);

			return game;
		}
	}
}