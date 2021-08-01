using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    // Start is called before the first frame update
    public void Move(float speed, Vector2 direction)
    {
        GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
