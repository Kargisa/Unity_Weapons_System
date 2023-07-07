using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class MeleeHitscanAttack : IAttackType
{
    public AttackSettings.HitScanMelee AttackSettings { get; }
    public SecondarySettings.Block BlockSettings { get; }
    public Weapon WeaponInfo { get; set; }
    public Camera Camera { get; }
    public SecondarySettings SecondarySettings { get; }

    public MeleeHitscanAttack(AttackSettings.HitScanMelee attackSettings, SecondarySettings.Block blockSettings, Weapon weapon)
    {
        AttackSettings = attackSettings;
        BlockSettings = blockSettings;
        WeaponInfo = weapon;
        Camera = weapon.firstpersonCamera;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.transform.position;
        Vector3 cameraRayTargetPoint = Camera.transform.forward * AttackSettings.range + cameraRayOrigin;

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint - cameraRayOrigin).normalized);

        bool didMeleeHit;
        
        didMeleeHit = Physics.Raycast(cameraRay, out RaycastHit meleeHit, AttackSettings.range);
        if (!didMeleeHit)
            didMeleeHit = Physics.SphereCast(cameraRay, AttackSettings.magnetRadius, out meleeHit, AttackSettings.range - AttackSettings.magnetRadius);

        if (!didMeleeHit)
            return cameraRayOrigin;

        if (meleeHit.collider.TryGetComponent(out Rigidbody rb))
        {
            Vector3 direction = (meleeHit.point - cameraRayOrigin).normalized;
            rb.AddForceAtPosition(direction * AttackSettings.pushForce, meleeHit.point, AttackSettings.pushForceMode);
        }

        float damage = CalculateDamage(cameraRayOrigin, meleeHit.point);

        return meleeHit.point;
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        return AttackSettings.damage;
    }

    public void MakeSecondary()
    {
        WeaponInfo.player.SetResistanceValues(BlockSettings.damageReduction);
    }

    public void ReleaseSecondary()
    {
        WeaponInfo.player.SetResistanceValues(-BlockSettings.damageReduction);
    }
}
