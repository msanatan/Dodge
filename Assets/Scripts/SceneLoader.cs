using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadSceneAsync(currentSceneIndex + 1, LoadSceneMode.Single);
    }

    public void LoadStartScene()
    {
        SceneManager.LoadSceneAsync(0, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
