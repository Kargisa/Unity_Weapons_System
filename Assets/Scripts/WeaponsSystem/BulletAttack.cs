using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : IAttackType
{
    public AttackSettings.Bullets AttackSettings { get; }
    public SecondarySettings.Scope ScopeSettings{ get; set; }
    public Attack AttackInfo { get; set; }
    public Camera Camera { get; }

    public BulletAttack(AttackSettings.Bullets attackSettings, SecondarySettings.Scope scopeSettings, Attack attack)
    {
        AttackSettings = attackSettings;
        ScopeSettings = scopeSettings;
        AttackInfo = attack;
        Camera = attack.firstpersonCamera;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.transform.position;
        Vector3 cameraRayTargetPoint = Camera.transform.forward * AttackSettings.maxFalloffRange + cameraRayOrigin;

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint - cameraRayOrigin).normalized);
        bool didCameraHit = Physics.Raycast(cameraRay, out RaycastHit cameraHit, AttackSettings.maxFalloffRange);
        
        Vector3 force;
        
        if (didCameraHit)
            force = (cameraHit.point - attackAnchor.position).normalized * AttackSettings.bulletrForce;
        else
            force = (cameraRayTargetPoint - attackAnchor.position).normalized * AttackSettings.bulletrForce;

        return new BulletData(AttackSettings ,force, cameraRayOrigin, CalculateDamage);
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        float distance = Vector3.Distance(origin, hitpoint);

        //Debug.Log("origin: " + origin + " hitpoint: " + hitpoint + " distance: " + distance);

        if (distance <= AttackSettings.minFalloffRange)
            return AttackSettings.damage * AttackSettings.falloffCurve.Evaluate(0);
        if (distance >= AttackSettings.maxFalloffRange)
            return AttackSettings.damage * AttackSettings.falloffCurve.Evaluate(1);

        float zoomFactor = ScopeSettings.rangeIncrease * ScopeSettings.zoom * (AttackInfo.holdsSecondary ? 1 : 0);
        float min = AttackSettings.minFalloffRange + zoomFactor;
        float max = AttackSettings.maxFalloffRange + zoomFactor;

        float pointOnCurve = (distance - min) / (max - min);
        float y = AttackSettings.falloffCurve.Evaluate(pointOnCurve);
        float damage = y * AttackSettings.damage;

        return damage;
    }

    public void MakeSecondary()
    {
        Camera.fieldOfView *= 1 - (ScopeSettings.zoom * 0.01f);
    }

    public void ReleaseSecondary()
    {
        Camera.fieldOfView /= 1 - (ScopeSettings.zoom * 0.01f);
    }
}
