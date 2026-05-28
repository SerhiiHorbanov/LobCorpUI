using UI.Widgets;

namespace EventBuses
{
	public class LobotomiteCardThrownOutEvent
	{
		public LobotomiteCard Card;
		
		public LobotomiteCardThrownOutEvent(LobotomiteCard card)
			=> Card = card;
	}
}
