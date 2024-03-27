using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
//{
//    public float lifeTime = 1f;
//    // Start is called before the first frame update
//    void Awake()
//    {
//        Destroy(gameObject, lifeTime);
//    }

//    // Update is called once per frame
//    void OnCollisionEnter(Collision collision)
//    {
//        if (collision.gameObject.CompareTag("Enemy"))
//        {
//            // Destroy only the enemy object
//            Destroy(collision.gameObject);
//        }

//        // Always destroy the bullet, regardless of what it collided with
//        Destroy(gameObject);
//    }
//}

{
    [SerializeField] private float damage = 10f;

    private void OnTriggerEnter(Collider other)
    {
        HealthComponent healthComponent = other.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.changeHealth(-damage);

            // Check if health is zero or less
            if (healthComponent.health <= 0)
            {
                Destroy(other.gameObject); // Destroy the target GameObject
            }

            Destroy(gameObject); // Destroy the projectile regardless
        }
    }
}