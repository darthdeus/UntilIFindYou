using UnityEngine;
using System.Collections;

public class Audio_player : MonoBehaviour {


	AudioSource playaudio;
	public AudioClip clip;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void PlayAudio ()
	{
		playaudio = GetComponent<AudioSource>();
		playaudio.clip =clip;
		playaudio.Play();

	}
}
