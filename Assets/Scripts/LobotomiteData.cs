using UnityEngine;

[CreateAssetMenu(fileName = "New Lobotomite Data")]
public class LobotomiteData : ScriptableObject
{
	[SerializeField] public string _Name;
	
	[SerializeField] public int _Fortitude;
	[SerializeField] public int _Prudence;
	[SerializeField] public int _Temperance;
	[SerializeField] public int _Justice;
	
	[SerializeField] public int _MouthIdx;
	[SerializeField] public int _EyesIdx;
	
	[SerializeField] public int _HairFrontIdx;
	[SerializeField] public int _HairBackIdx;
	[SerializeField] public Color _HairColor;
}
