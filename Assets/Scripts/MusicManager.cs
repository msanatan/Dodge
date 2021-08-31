using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    private static MusicManager musicManagerInstance;
    AudioSource backgroundMusic;
    
    void Awake()
    {
        DontDestroyOnLoad(this);

        if (musicManagerInstance == null)
        {
            musicManagerInstance = this;
            backgroundMusic = GetComponent<AudioSource>();
        }
        else if (musicManagerInstance != this)
        {
            Destroy(musicManagerInstance.gameObject);
            musicManagerInstance = this;
            backgroundMusic = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic()
    {
        if (backgroundMusic.isPlaying)
        {
            return;
        }

        backgroundMusic.Play();
    }

    public void StopMusic()
    {
        backgroundMusic.Stop();
    }

    public void PauseMusic()
    {
        backgroundMusic.Pause();
    }
}
