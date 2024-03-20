using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] HealthComponent healthComponent;
    [SerializeField] Animator animator;
    [SerializeField] PerceptionComponent perceptionComp;
    [SerializeField] MovementComponent movementComponent;

    GameObject target;
    Vector3 prevPos;
    public Animator Animator
    {
        get { return animator; }
        private set { animator = value; }
    }

    private void Awake()
    {
        perceptionComp.onPerceptionTargetChanged += TargetChanged;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(healthComponent!=null)
        {
            healthComponent.onHealthEmpty += StartDeath;
            healthComponent.onTakeDamage += TakenDamage;
        }
        prevPos = transform.position;
    }

    private void TargetChanged(GameObject target, bool sensed)
    {
        if(sensed)
        {
            this.target = target;
        }
        else
        {
            this.target = null;
        }
    }

    private void TakenDamage(float health, float delta, float maxHealth, GameObject Instigator)
    {
        
    }

    private void StartDeath(GameObject Killer)
    {
        TriggerDeathAnimation();
        GetComponent<CapsuleCollider>().enabled = false;
    }

    private void TriggerDeathAnimation()
    {
        if(animator!= null)
        {
            animator.SetTrigger("Dead");
        }
    }

    public void OnDeathAnimationFinished()
    {
        Dead();
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSpeed();
        if (transform.position.y < -100)
        {
            StartDeath(gameObject);
            Debug.Log("Enemy Dropped to oblivion");
        }
    }

    private void CalculateSpeed()
    {
        if (movementComponent == null) return;

        Vector3 posDelta = transform.position - prevPos;
        float speed = posDelta.magnitude / Time.deltaTime;
        Animator.SetFloat("Speed", speed);
        prevPos = transform.position;
    }

    private void OnDrawGizmos()
    {
        if(target!=null)
        {
            Vector3 drawTargetPos = target.transform.position + Vector3.up;
            Gizmos.DrawWireSphere(drawTargetPos, 0.7f);

            Gizmos.DrawLine(transform.position + Vector3.up, drawTargetPos);
        }
    }

    public void RotateTowards(GameObject target, bool vertialAim = false)
    {
        Vector3 AimDir = target.transform.position - transform.position;
        AimDir.y = vertialAim ? AimDir.y : 0;
        AimDir = AimDir.normalized;

        movementComponent.RotateTowards(AimDir);
    }

    public virtual void AttackTarget(GameObject target)
    {
        //override in child
    }

    public void SpawnedBy(GameObject spawnerGameobject)
    {
        
    }

    protected virtual void Dead() { }
}
