using System;
using EventBuses;
using UnityEngine;

namespace UI.Widgets
{
	public class LobotomiteSelection : MonoBehaviour
	{
		private LobotomiteData _hardSelectedLobotomite;// lobotomite is hard selected when clicking on its card. otherwise lobotomite is only displayed
		private LobotomiteData _displayedLobotomite;
		
		public Action<LobotomiteData> OnLobotomiteShouldBeDisplayed;
		
		private void Start()
		{
			EventBus<HardLobotomiteSelectionEventPayload>.Event += HardSelectLobotomite;
			EventBus<SoftLobotomiteSelectionEventPayload>.Event += SoftSelectLobotomite;
			EventBus<SoftLobotomiteDeselectionEventPayload>.Event += SoftDeselectLobotomite;
		}

		private void HardSelectLobotomite(HardLobotomiteSelectionEventPayload payload)
		{
			bool wasNull = _hardSelectedLobotomite == null;
			bool nowNull = payload.Lobotomite == null;
			
			if (wasNull && !nowNull)
			{
				EventBus<SoftLobotomiteSelectionEventPayload>.Event -= SoftSelectLobotomite;
				EventBus<SoftLobotomiteDeselectionEventPayload>.Event -= SoftDeselectLobotomite;
			}
			else if (!wasNull && nowNull)
			{
				EventBus<SoftLobotomiteSelectionEventPayload>.Event += SoftSelectLobotomite;
				EventBus<SoftLobotomiteDeselectionEventPayload>.Event += SoftDeselectLobotomite;
			}
			
			_hardSelectedLobotomite = payload.Lobotomite;
			_displayedLobotomite = payload.Lobotomite;
			
			OnLobotomiteShouldBeDisplayed?.Invoke(payload.Lobotomite);
		}
		
		private void SoftSelectLobotomite(SoftLobotomiteSelectionEventPayload payload)
		{
			_displayedLobotomite = payload.Lobotomite;
			
			OnLobotomiteShouldBeDisplayed?.Invoke(payload.Lobotomite); 
		}

		private void SoftDeselectLobotomite(SoftLobotomiteDeselectionEventPayload payload)
		{
			LobotomiteData deselected = payload.Lobotomite;
			if (deselected == _displayedLobotomite)
			{
				OnLobotomiteShouldBeDisplayed?.Invoke(null);
			}
		}
	}
}
