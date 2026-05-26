using TMPro;
using UnityEngine;

namespace UI.Widgets
{
	public class VirtueDisplay : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI _Text;
		
		public void SetValue(int value)
		{
			_Text.text = GetStatTextForValue(value);
		}
		
		private string GetStatTextForValue(int value)
			=> value switch
			{
				< 1 => "-",
				1 => "I",
				2 => "II",
				3 => "III",
				4 => "IV",
				5 => "V",
				> 5 => "EX",
			};
	}
}
