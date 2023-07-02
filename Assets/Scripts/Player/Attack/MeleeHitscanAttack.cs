using System.Collections;
using System.Collections.Generic;
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
        bool didMeleeHit = Physics.SphereCast(cameraRay, Settings.magnetRadius, out RaycastHit meleeHit, Settings.range);

        if (!didMeleeHit)
            return cameraRayOrigin;

        float damage = CalculateDamage(cameraRayOrigin, meleeHit.point);

        return meleeHit.point;
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        return Settings.damage;
    }
}
