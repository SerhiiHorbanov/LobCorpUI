using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
	public class LobotomiteVisuals : MonoBehaviour
	{
		[SerializeField] private LobotomiteData _Lobotomite;

		[SerializeField] private RawImage _BodyImage;
		
		[SerializeField] private RawImage _MouthImage;
		[SerializeField] private RawImage _EyesImage;
		[SerializeField] private RawImage _HairFrontImage;
		[SerializeField] private RawImage _HairBackImage;
		
		[SerializeField] private Texture[] _MouthTextures;
		[SerializeField] private Texture[] _EyesTextures;
		[SerializeField] private Texture[] _HairFrontTextures;
		[SerializeField] private Texture[] _HairBackTextures;

		public void SetLobotomite(LobotomiteData lobotomite)
		{
			if (lobotomite == null)
			{
				gameObject.SetActive(false);
				return;
			}
			else
			{
				gameObject.SetActive(true);
			}
			
			_Lobotomite = lobotomite;
			_MouthImage.texture = _MouthTextures[_Lobotomite._MouthIdx];
			_EyesImage.texture = _EyesTextures[_Lobotomite._EyesIdx];
			_HairFrontImage.texture = _HairFrontTextures[_Lobotomite._HairFrontIdx];
			_HairBackImage.texture = _HairBackTextures[_Lobotomite._HairBackIdx];
			
			_HairFrontImage.color = _Lobotomite._HairColor.WithAlpha(1);
			_HairBackImage.color = _Lobotomite._HairColor.WithAlpha(1);
		}
	}
}
