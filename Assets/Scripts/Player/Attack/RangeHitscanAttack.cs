using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHitscanAttack : IAttackType
{
    public AttackSettings.RangeHitscan Settings { get; }
    public IWeaponType Weapon { get; set; }

    public RangeHitscanAttack(AttackSettings.RangeHitscan settings)
    {
        Settings = settings;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.main.transform.position;
        Vector3 cameraRayTargetPoint = Camera.main.transform.forward * Settings.maxFalloffRange + cameraRayOrigin;

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint - cameraRayOrigin).normalized);
        bool didCameraHit = Physics.Raycast(cameraRay, out RaycastHit cameraHit, Settings.maxFalloffRange);

        if (!didCameraHit)
            return cameraRayTargetPoint;

        Ray weaponRay = new Ray(attackAnchor.position, (cameraHit.point - attackAnchor.position).normalized);

        bool didWeaponHit = Physics.Raycast(weaponRay, out RaycastHit weaponHit, Settings.maxFalloffRange);

        if (!didWeaponHit)
            return cameraHit.point;

        float damage = CalculateDamage(attackAnchor.position, weaponHit.point);
        
        Debug.Log(damage);

        return weaponHit.point;
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        float distance = Vector3.Distance(origin, hitpoint);

        if (distance <= Settings.minFalloffRange)
            return Settings.damage;
        if (distance >= Settings.maxFalloffRange)
            return 0f;

        //Debug.Log("origin: " + origin + " hitpoint: " + hitpoint + " distance: " + distance);

        float pointOnCurve = (distance - Settings.minFalloffRange) / (Settings.maxFalloffRange - Settings.minFalloffRange);
        float y = Settings.falloffCurve.Evaluate(pointOnCurve);
        float damage = y * Settings.damage;

        return damage;
    }
}
