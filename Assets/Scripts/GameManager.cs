using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scoreMarkerForMoreEnemies = 25;
    [SerializeField] EnemyFactory enemyFactory;
    WaitForSeconds scoreDelay = new WaitForSeconds(1);
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncrementScore", score);
    }

    IEnumerator IncrementScore(int score)
    {
        while (!gameOver)
        {
            score++;
            scoreText.text = $"Score: {score}";
            PlayerStats.Score = score;
            if (score % scoreMarkerForMoreEnemies == 0)
            {
                enemyFactory.DecreaseSpawnTime();
            }
            yield return scoreDelay;
        }
    }

    public void ToggleGameOver()
    {
        gameOver = !gameOver;
        new WaitForSeconds(1f);
        sceneLoader.LoadNextScene();
    }
}
