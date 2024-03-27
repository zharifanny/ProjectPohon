using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] AimComponent aimComp;
    [SerializeField] float Damage = 5f; 

    public override void Attack()
    {
        Vector3 aimDir;
        GameObject target = aimComp.GetAimTarget(out aimDir);
        DamageGameObject(target, Damage);
    }
}
