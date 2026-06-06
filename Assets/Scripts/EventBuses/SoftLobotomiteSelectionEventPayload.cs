using ScriptableObjects;

namespace EventBuses
{
	public class SoftLobotomiteSelectionEventPayload
	{
		public LobotomiteData Lobotomite;
		
		public SoftLobotomiteSelectionEventPayload(LobotomiteData lobotomite)
			=> Lobotomite = lobotomite;	
	}
}
