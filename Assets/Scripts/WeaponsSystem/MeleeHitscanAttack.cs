using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;

public class MeleeHitscanAttack : IAttackType
{
    public AttackSettings.HitScanMelee Settings { get; }
    public Attack AttackInfo { get; set; }
    public Camera Camera { get; }

    public MeleeHitscanAttack(AttackSettings.HitScanMelee settings, Attack attack)
    {
        Settings = settings;
        AttackInfo = attack;
        Camera = attack.firstpersonCamera;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.transform.position;
        Vector3 cameraRayTargetPoint = Camera.transform.forward * Settings.range + cameraRayOrigin;

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

    public void ReleaseSecondary()
    {
        throw new System.NotImplementedException();
    }
}
