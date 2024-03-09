using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 0.05f;
    // Start is called before the first frame update
    void Awake()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy only the enemy object
            Destroy(collision.gameObject);
        }

        // Always destroy the bullet, regardless of what it collided with
        Destroy(gameObject);
    }
}
