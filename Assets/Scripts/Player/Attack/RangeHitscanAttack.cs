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

        return weaponHit.point;
    }

    public float OnDamageDealt(Vector3 origin, Vector3 hitpoint)
    {
        throw new System.NotImplementedException();
    }
}
