namespace EventBuses
{
	public class SoftLobotomiteDeselectionEventPayload
	{
		public LobotomiteData Lobotomite;
		
		public SoftLobotomiteDeselectionEventPayload(LobotomiteData lobotomite)
			=> Lobotomite = lobotomite;	
	}
}
