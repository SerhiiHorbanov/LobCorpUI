using UnityEngine;

namespace UI.Widgets
{
	public class LobotomiteCard : MonoBehaviour
	{
		private LobotomiteData _data;
		
		[SerializeField] private VirtueDisplay _FortitudeDisplay;
		[SerializeField] private VirtueDisplay _PrudenceDisplay;
		[SerializeField] private VirtueDisplay _TemperanceDisplay;
		[SerializeField] private VirtueDisplay _JusticeDisplay;
		
		public void Initialize(LobotomiteData data)
		{
			_data = data;
			
			_FortitudeDisplay.SetValue(_data._Fortitude);
			_PrudenceDisplay.SetValue(_data._Prudence);
			_TemperanceDisplay.SetValue(_data._Temperance);
			_JusticeDisplay.SetValue(_data._Justice);
		}

		
	}
}
