using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IAttackType
{
    public AttackSettings.Range Settings { get; }
    public IWeapon Weapon { get; set; }

    public RangeAttack(AttackSettings.Range settings)
    {
        Settings = settings;
    }

    public Vector3 MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.main.transform.position;
        Vector3 cameraRayTargetPoint = Camera.main.transform.forward * Settings.maxFalloffRange + cameraRayOrigin;

        Debug.Log(Camera.main.transform.forward);

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint).normalized);
        bool didCameraHit = Physics.Raycast(cameraRay, out RaycastHit cameraHit, Settings.maxFalloffRange);


        if (!didCameraHit)
            return cameraRayTargetPoint;

        Ray weaponRay = new Ray(attackAnchor.position, (cameraHit.point).normalized);


        Debug.DrawRay(attackAnchor.position, (cameraHit.point - attackAnchor.position).normalized * 100, Color.green, 1f);
        bool didWeaponHit = Physics.Raycast(weaponRay, out RaycastHit weaponHit, Settings.maxFalloffRange);

        if (!didWeaponHit)
            return cameraHit.point;

        return weaponHit.point;
    }
}
