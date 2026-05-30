using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundEvent : MonoBehaviour
{
	private AudioSource _audioSource;

	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public void Play()
	{
		_audioSource.Play();
	}
}