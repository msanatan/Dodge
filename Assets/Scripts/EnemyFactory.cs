using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    WaitForSeconds spawnDelay = new WaitForSeconds(3);
    [SerializeField] GameObject squareEnemyPrefab;
    bool spawnEnemies = true;

    // Start is called before the first frame update
    void Start()
    {
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
            Instantiate(squareEnemyPrefab, new Vector3(xPosition, Random.Range(topLeft.y, bottomRight.y), 0), Quaternion.Euler(0, 0, 0));
            yield return spawnDelay;
        }
    }

    public void ToggleSpawnFlag()
    {
        spawnEnemies = !spawnEnemies;
    }
}
