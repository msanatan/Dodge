using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XEnemyController : MonoBehaviour
{
    private float speed = 80f;
    private Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        var sign = transform.position.y < 0 ? 1 : -1;
        direction = new Vector2(0, sign);
        GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
