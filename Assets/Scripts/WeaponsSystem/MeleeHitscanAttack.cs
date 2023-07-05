using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class MeleeHitscanAttack : IAttackType
{
    public AttackSettings.HitScanMelee Settings { get; }

    public MeleeHitscanAttack(AttackSettings.HitScanMelee settings)
    {
        Settings = settings;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.main.transform.position;
        Vector3 cameraRayTargetPoint = Camera.main.transform.forward * Settings.range + cameraRayOrigin;

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint - cameraRayOrigin).normalized);

        bool didMeleeHit;
        
        didMeleeHit = Physics.Raycast(cameraRay, out RaycastHit meleeHit, Settings.range);
        if (!didMeleeHit)
            didMeleeHit = Physics.SphereCast(cameraRay, Settings.magnetRadius, out meleeHit, Settings.range - Settings.magnetRadius);

        if (!didMeleeHit)
            return cameraRayOrigin;

        if (meleeHit.collider.TryGetComponent(out Rigidbody rb))
        {
            Vector3 direction = (meleeHit.point - cameraRayOrigin).normalized;
            rb.AddForceAtPosition(direction * Settings.pushForce, meleeHit.point, Settings.pushForceMode);
        }

        float damage = CalculateDamage(cameraRayOrigin, meleeHit.point);

        return meleeHit.point;
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        return Settings.damage;
    }

    public void MakeSecondary()
    {
        throw new System.NotImplementedException();
    }
}
