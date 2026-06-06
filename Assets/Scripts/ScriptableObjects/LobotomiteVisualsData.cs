using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "New lobotomite visuals")]
	public class LobotomiteVisualsData : ScriptableObject
	{
		[SerializeField] public int _MouthIdx;
		[SerializeField] public int _EyesIdx;
	
		[SerializeField] public int _HairFrontIdx;
		[SerializeField] public int _HairBackIdx;
		[SerializeField] public Color _HairColor;
	}
}