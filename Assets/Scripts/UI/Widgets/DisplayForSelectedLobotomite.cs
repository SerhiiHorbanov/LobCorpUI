using UnityEngine;

namespace UI.Widgets
{
	public class DisplayForSelectedLobotomite : MonoBehaviour
	{
		[SerializeField] private LobotomiteSelection _Selection;

		[SerializeField] private LobotomiteVisuals _LobotomiteVisuals;
		
		[SerializeField] private VirtueDisplay _FortitudeDisplay;
		[SerializeField] private VirtueDisplay _PrudenceDisplay;
		[SerializeField] private VirtueDisplay _TemperanceDisplay;
		[SerializeField] private VirtueDisplay _JusticeDisplay;
		
		private void Start()
		{
			_Selection.OnLobotomiteShouldBeDisplayed += Display;
		}

		private void Display(LobotomiteData lobotomite)
		{
			UpdateVirtueDisplays(lobotomite);
			_LobotomiteVisuals.SetLobotomite(lobotomite._Visuals);
		}

		private void UpdateVirtueDisplays(LobotomiteData lobotomite)
		{
			_FortitudeDisplay.SetValue(lobotomite?._Fortitude ?? 0);
			_PrudenceDisplay.SetValue(lobotomite?._Prudence ?? 0);
			_TemperanceDisplay.SetValue(lobotomite?._Temperance ?? 0);
			_JusticeDisplay.SetValue(lobotomite?._Justice ?? 0);
		}
	}
}
