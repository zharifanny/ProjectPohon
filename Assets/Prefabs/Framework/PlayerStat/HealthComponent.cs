using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public delegate void OnHealthChange(float health, float delta, float maxHealth);
    public delegate void OnTakeDamage(float health, float delta, float maxHealth);
    public delegate void OnHealthEmpty();

    [SerializeField] public float health = 100;
    [SerializeField] float maxhealth = 100;

    public event OnHealthChange onHealthChange;
    public event OnTakeDamage onTakeDamage;
    public event OnHealthEmpty onHealthEmpty;

    public void changeHealth(float amount)
    {
        if(amount == 0)
        {
            return;
        }

        health += amount;
        if(amount<0)
        {
            onTakeDamage?.Invoke(health, amount, maxhealth);
        }

        onHealthChange?.Invoke(health, amount, maxhealth);

        if(health<=0)
        {
            health = 0;
            onHealthEmpty?.Invoke();
            Destroy(gameObject); //destroy pas healthnya 0
        }

        Debug.Log($"{gameObject.name}, taking damage: {amount}, health is now: {health}");
    }
}
