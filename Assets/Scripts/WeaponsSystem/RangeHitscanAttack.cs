using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeHitscanAttack : IAttackType
{
    public AttackSettings.RangeHitscan AttackSettings { get; }
    public SecondarySettings.Scope ScopeSettings { get; }
    public Attack AttackInfo { get; }
    public Camera Camera { get; }

    public IWeaponType Weapon { get; set; }

    public RangeHitscanAttack(AttackSettings.RangeHitscan attackSettings, SecondarySettings.Scope scopeSettings, Attack attack)
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
        bool didCameraHit = Physics.Raycast(cameraRay, out RaycastHit hitcanHit, AttackSettings.maxFalloffRange);

        if (!didCameraHit)
            return cameraRayTargetPoint;

        if (hitcanHit.collider.TryGetComponent(out Rigidbody rb))
        {
            Vector3 direction = (hitcanHit.point - cameraRayOrigin).normalized;
            rb.AddForceAtPosition(direction * AttackSettings.pushForce, hitcanHit.point, AttackSettings.pushForceMode);
        }

        float damage = CalculateDamage(cameraRayOrigin, hitcanHit.point);

        return hitcanHit.point;
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        float distance = Vector3.Distance(origin, hitpoint);

        if (distance <= AttackSettings.minFalloffRange)
            return AttackSettings.damage * AttackSettings.falloffCurve.Evaluate(0);
        if (distance >= AttackSettings.maxFalloffRange)
            return AttackSettings.damage * AttackSettings.falloffCurve.Evaluate(1);

        //Debug.Log("origin: " + origin + " hitpoint: " + hitpoint + " distance: " + distance);

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
