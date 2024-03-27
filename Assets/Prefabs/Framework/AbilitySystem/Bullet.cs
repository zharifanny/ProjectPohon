using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour


{
    [SerializeField] private float damage = 10f;
    public float lifeTime = 1f;


    void Awake() //destroy bullet after lifeTime duration
    {
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        HealthComponent healthComponent = other.GetComponent<HealthComponent>();
        if (healthComponent != null)
        {
            healthComponent.changeHealth(-damage);

            //Check if health is zero or less
            //if (healthComponent.health <= 0)
            //    {
            //        Destroy(other.gameObject); // Destroy the target GameObject
            //    }

            Destroy(gameObject); // bulletnya didestroy pas kena object
        }
    }
}