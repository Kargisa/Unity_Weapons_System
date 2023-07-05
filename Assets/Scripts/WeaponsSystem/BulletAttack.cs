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
        
        Vector3 force;
        
        if (didCameraHit)
            force = (cameraHit.point - attackAnchor.position).normalized * Settings.bulletrForce;
        else
            force = (cameraRayTargetPoint - attackAnchor.position).normalized * Settings.bulletrForce;

        return new BulletData(Settings ,force, cameraRayOrigin, CalculateDamage);
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        float distance = Vector3.Distance(origin, hitpoint);

        //Debug.Log("origin: " + origin + " hitpoint: " + hitpoint + " distance: " + distance);

        if (distance <= Settings.minFalloffRange)
            return Settings.damage * Settings.falloffCurve.Evaluate(0);
        if (distance >= Settings.maxFalloffRange)
            return Settings.damage * Settings.falloffCurve.Evaluate(1);

        float pointOnCurve = (distance - Settings.minFalloffRange) / (Settings.maxFalloffRange - Settings.minFalloffRange);
        float y = Settings.falloffCurve.Evaluate(pointOnCurve);
        float damage = y * Settings.damage;

        return damage;
    }

    public void MakeSecondary()
    {
        
    }
}
