using ScriptableObjects;

namespace EventBuses
{
	public class HardLobotomiteSelectionEventPayload
	{
		public LobotomiteData Lobotomite;
		
		public HardLobotomiteSelectionEventPayload(LobotomiteData lobotomite)
			=> Lobotomite = lobotomite;
	}
}
