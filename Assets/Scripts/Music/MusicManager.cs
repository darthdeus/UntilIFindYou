using System;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public float Volume;
    public MusicStorage Tracks;
    AudioSource PlayingMusic;
    AudioSource NextMusic;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        PlayingMusic = Tracks.HomePlanet;
        TurnOffMusic = false;
    }

    bool TurnOffMusic;
    event EventHandler TurnOnMusic;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // Nothing else needs to be done here //
        if (TurnOffMusic)
        {
            if (PlayingMusic != null && PlayingMusic.volume > 0.1)
                PlayingMusic.volume -= 1.3f * Time.deltaTime;
            else
            {
                StopPlayingMusic_DONOTCALL();
                if (TurnOnMusic != null && NextMusic != null)
                    TurnOnMusic(this, EventArgs.Empty);
            }
        }
        // Nothing else needs to be done here //
    }

    // Call this method in order to play new music //
    public void PlayMusic(AudioSource toPlay)
    {
        if (NextMusic == null)
        {
            TurnOffMusic = true;
            NextMusic = toPlay;
            TurnOnMusic += PlayNextMusic_DONOTCALL;
        }
    }

    // DO NOT CALL THIS //
    void PlayNextMusic_DONOTCALL(object sender, EventArgs e)
    {
        TurnOffMusic = false;
        PlayingMusic = NextMusic;
        if (PlayingMusic != null)
            PlayingMusic.Play();
        NextMusic = null;
        TurnOnMusic -= PlayNextMusic_DONOTCALL;
    }

    // DO NOT CALL THIS //
    void StopPlayingMusic_DONOTCALL()
    {
        if (PlayingMusic != null)
        {
            PlayingMusic.Stop();
            PlayingMusic.volume = Volume;
        }
    }
    // Call this if there should be no music played //
    public void PlayNoMusic()
    {
        PlayMusic(null);
    }
}