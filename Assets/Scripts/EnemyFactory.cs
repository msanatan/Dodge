using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] float spawnDelay = 2;
    [SerializeField] float diagonalSpawnDelay = 50;
    [SerializeField] float spawnDecrement = 0.1f;
    [SerializeField] float minSpawnDelay = 1;
    [SerializeField] float minSpeed = 70;
    [SerializeField] float maxSpeed = 100;
    [SerializeField] GameObject horizontalEnemyPrefab;
    [SerializeField] GameObject verticalEnemyPrefab;
    [SerializeField] GameObject diagonalEnemyPrefab;
    WaitForSeconds spawnDelayYield;
    WaitForSeconds diagonalSpawnDelayYield;
    bool spawnEnemies = true;
    bool fireDiagonal = false;
    Vector3 topLeft;
    Vector3 bottomRight;

    // Start is called before the first frame update
    void Start()
    {
        topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, Camera.main.nearClipPlane));
        bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, Camera.main.nearClipPlane));
        spawnDelayYield = new WaitForSeconds(spawnDelay);
        StartCoroutine("SpawnEnemy");
        diagonalSpawnDelayYield = new WaitForSeconds(diagonalSpawnDelay);
        StartCoroutine("SpawnDiagonalEnemy");
    }

    IEnumerator SpawnEnemy()
    {
        while (spawnEnemies)
        {
            var xPosition = Random.value < 0.5f ? topLeft.x : bottomRight.x;
            var yPosition = Random.value < 0.5f ? topLeft.y : bottomRight.y;

            var horizontalEnemy = Instantiate<GameObject>(horizontalEnemyPrefab, new Vector3(xPosition, Random.Range(topLeft.y, bottomRight.y), 0), Quaternion.Euler(0, 0, 0));
            var horizontalEnemyController = horizontalEnemy.GetComponent<EnemyController>();
            var horizontalEnemyDirection = new Vector2(xPosition  < 0 ? 1 : -1, 0);
            horizontalEnemyController.Move(Random.Range(minSpeed, maxSpeed), horizontalEnemyDirection);

            var verticalEnemy = Instantiate<GameObject>(verticalEnemyPrefab, new Vector3(Random.Range(topLeft.x, bottomRight.x), yPosition, 0), Quaternion.Euler(0, 0, 0));
            var verticalEnemyController = verticalEnemy.GetComponent<EnemyController>();
            var verticalEnemyDirection = new Vector2(0, yPosition < 0 ? 1 : -1);
            verticalEnemyController.Move(Random.Range(minSpeed, maxSpeed), verticalEnemyDirection);
            yield return spawnDelayYield;
        }
    }

    IEnumerator SpawnDiagonalEnemy()
    {
        if (!fireDiagonal) // Don't fire immediately
        {
            fireDiagonal = true;
            yield return diagonalSpawnDelayYield;
        }

        while (spawnEnemies)
        {
            for (int i = 0; i < 5; i++) {
                var diagonalEnemy = Instantiate<GameObject>(diagonalEnemyPrefab, new Vector3(Random.Range(topLeft.x, bottomRight.x), topLeft.y, 0), Quaternion.Euler(0, 0, 0));
                diagonalEnemy.transform.Rotate(0, 0, 135);
                var diagonalEnemyController = diagonalEnemy.GetComponent<EnemyController>();
                var diagonalEnemyDirection = new Vector2(-0.5f, -1);
                diagonalEnemyController.Move(Random.Range(minSpeed, maxSpeed), diagonalEnemyDirection);
                new WaitForSeconds(1f);
            }

            yield return diagonalSpawnDelayYield;
        }
    }

    public void DecreaseSpawnTime()
    {
        if (spawnDelay > minSpawnDelay)
        {
            spawnDelay -= spawnDecrement;
            spawnDelayYield = new WaitForSeconds(spawnDelay);
        }
    }

    public void ToggleSpawnFlag()
    {
        spawnEnemies = !spawnEnemies;
    }
}
