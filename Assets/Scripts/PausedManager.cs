using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedManager : MonoBehaviour
{
    public static bool paused = false;
    PausedAction action;
    [SerializeField] GameObject menu;

    public void Awake()
    {
        action = new PausedAction();
    }

    public void OnEnable()
    {
        action.Enable();
    }

    public void OnDisable()
    {
        action.Disable();
    }

    public void Start()
    {
        action.Pause.PauseGame.performed += _ => TogglePause();
    }

    private void TogglePause()
    {
        if (paused)
        {
            ResumeGame();
        } else
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        paused = true;
        menu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        paused = false;
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
