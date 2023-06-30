using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : IAttackType
{
    public AttackSettings.Bullets Settings { get; }


    public BulletAttack(AttackSettings.Bullets bulletsSettings)
    {
        Settings = bulletsSettings;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.main.transform.position;
        Vector3 cameraRayTargetPoint = Camera.main.transform.forward * Settings.maxFalloffRange + cameraRayOrigin;

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint - cameraRayOrigin).normalized);
        bool didCameraHit = Physics.Raycast(cameraRay, out RaycastHit cameraHit, Settings.maxFalloffRange);

        Debug.Log(cameraHit.point);

        if (didCameraHit)
            return (cameraHit.point - attackAnchor.position).normalized * Settings.force;

        return (cameraRayTargetPoint - attackAnchor.position).normalized * Settings.force;
    }
}
