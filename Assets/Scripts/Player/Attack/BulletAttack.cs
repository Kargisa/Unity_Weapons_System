using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : IAttackType
{
    public AttackSettings.Bullets BulletsSettings { get; }


    public BulletAttack(AttackSettings.Bullets bulletsSettings)
    {
        BulletsSettings = bulletsSettings;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        GameObject bullet = new GameObject("Bullet");
        Rigidbody rb = bullet.AddComponent<Rigidbody>();
        bullet.AddComponent<Bullet>();

        rb.AddRelativeForce(Vector3.forward * BulletsSettings.force, ForceMode.VelocityChange);

        return bullet;
    }
}
