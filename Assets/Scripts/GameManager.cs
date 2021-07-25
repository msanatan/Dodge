using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    WaitForSeconds scoreDelay = new WaitForSeconds(1);
    WaitForSeconds spawnDelay = new WaitForSeconds(3);
    bool gameOver = false;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject squareEnemyPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncrementScore", score);
        StartCoroutine("SpawnEnemy");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemy()
    {
        Vector3 topLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 bottomRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        while (!gameOver)
        {
            var xPosition = Random.value < 0.5f ? -2.5f : 2.5f;
            Instantiate(squareEnemyPrefab, new Vector3(xPosition, Random.Range(topLeft.y, bottomRight.y), 0), Quaternion.Euler(0, 0, 0));
            yield return spawnDelay;
        }
    }

    IEnumerator IncrementScore(int score)
    {
        while (!gameOver)
        {
            score++;
            scoreText.text = $"Score: {score}";
            yield return scoreDelay;
        }
    }
}
