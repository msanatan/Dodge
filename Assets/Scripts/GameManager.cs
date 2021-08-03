using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    WaitForSeconds scoreDelay = new WaitForSeconds(1);
    bool gameOver = false;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] int scoreMarkerForMoreEnemies = 25;
    [SerializeField] EnemyFactory enemyFactory;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncrementScore", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IncrementScore(int score)
    {
        while (!gameOver)
        {
            score++;
            scoreText.text = $"Score: {score}";
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
    }
}
