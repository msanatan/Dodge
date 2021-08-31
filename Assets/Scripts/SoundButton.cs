using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField] MusicManager musicManager;
    bool isPlaying = false;
    TextMeshProUGUI buttonText;

    public void Awake()
    {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void ToggleMusic()
    {
        isPlaying = !isPlaying;
        if (isPlaying)
        {
            musicManager.PlayMusic();
            buttonText.SetText("Sound (On)");
        } else
        {
            musicManager.StopMusic();
            buttonText.SetText("Sound (Off)");
        }
    }
}
