using UnityEngine;

[CreateAssetMenu(fileName = "New Lobotomite Data")]
public class LobotomiteData : ScriptableObject
{
	[SerializeField] public int _Fortitude;
	[SerializeField] public int _Prudence;
	[SerializeField] public int _Temperance;
	[SerializeField] public int _Justice;
}
