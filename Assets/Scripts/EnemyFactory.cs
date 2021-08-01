using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] float spawnDelay = 2;
    [SerializeField] float minSpeed = 70;
    [SerializeField] float maxSpeed = 100;
    private WaitForSeconds spawnDelayYield;
    [SerializeField] GameObject squareEnemyPrefab;
    [SerializeField] GameObject xEnemyPrefab;
    bool spawnEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
        spawnDelayYield = new WaitForSeconds(spawnDelay);
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

        while (spawnEnemies)
        {
            var xPosition = Random.value < 0.5f ? -2.5f : 2.5f;
            var yPosition = Random.value < 0.5f ? -5.5f : 5.5f;
            var square = Instantiate<GameObject>(squareEnemyPrefab, new Vector3(xPosition, Random.Range(topLeft.y, bottomRight.y), 0), Quaternion.Euler(0, 0, 0));
            var squareController = square.GetComponent<EnemyController>();
            var squareDirection = new Vector2(xPosition  < 0 ? 1 : -1, 0);
            squareController.Move(Random.Range(minSpeed, maxSpeed), squareDirection);

            var xEnemy = Instantiate<GameObject>(xEnemyPrefab, new Vector3(Random.Range(topLeft.x, bottomRight.x), yPosition, 0), Quaternion.Euler(0, 0, 0));
            var xController = xEnemy.GetComponent<EnemyController>();
            var xDirection = new Vector2(0, yPosition < 0 ? 1 : -1);
            xController.Move(Random.Range(minSpeed, maxSpeed), xDirection);
            yield return spawnDelayYield;
        }
    }

    public void ToggleSpawnFlag()
    {
        spawnEnemies = !spawnEnemies;
    }
}
