using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float speed = 80f;
    [SerializeField] Vector2 direction = new Vector2(-1, 0);

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
