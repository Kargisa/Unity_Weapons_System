using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : IAttackType
{
    public AttackSettings.Bullets WeaponInfo { get; }
    public SecondarySettings.Scope ScopeSettings{ get; set; }
    public Weapon AttackInfo { get; set; }
    public Camera Camera { get; }

    public SecondarySettings SecondarySettings { get; }

    private float zoomFactor = 0f;

    public BulletAttack(AttackSettings.Bullets attackSettings, SecondarySettings.Scope scopeSettings, Weapon weapon)
    {
        WeaponInfo = attackSettings;
        ScopeSettings = scopeSettings;
        SecondarySettings = scopeSettings;
        AttackInfo = weapon;
        Camera = weapon.firstpersonCamera;
    }

    public object MakeAttack(Transform attackAnchor)
    {
        Vector3 cameraRayOrigin = Camera.transform.position;
        Vector3 cameraRayTargetPoint = Camera.transform.forward * WeaponInfo.maxFalloffRange + cameraRayOrigin;

        Ray cameraRay = new Ray(cameraRayOrigin, (cameraRayTargetPoint - cameraRayOrigin).normalized);
        bool didCameraHit = Physics.Raycast(cameraRay, out RaycastHit cameraHit, WeaponInfo.maxFalloffRange);
        
        Vector3 force;
        
        if (didCameraHit)
            force = (cameraHit.point - attackAnchor.position).normalized * WeaponInfo.bulletrForce;
        else
            force = (cameraRayTargetPoint - attackAnchor.position).normalized * WeaponInfo.bulletrForce;

        return new BulletData(WeaponInfo ,force, cameraRayOrigin, CalculateDamage);
    }

    public float CalculateDamage(Vector3 origin, Vector3 hitpoint)
    {
        float distance = Vector3.Distance(origin, hitpoint);

        //Debug.Log("origin: " + origin + " hitpoint: " + hitpoint + " distance: " + distance);

        if (distance <= WeaponInfo.minFalloffRange)
            return WeaponInfo.damage * WeaponInfo.falloffCurve.Evaluate(0);
        if (distance >= WeaponInfo.maxFalloffRange)
            return WeaponInfo.damage * WeaponInfo.falloffCurve.Evaluate(1);

        float min = WeaponInfo.minFalloffRange + zoomFactor;
        float max = WeaponInfo.maxFalloffRange + zoomFactor;

        float pointOnCurve = (distance - min) / (max - min);
        float y = WeaponInfo.falloffCurve.Evaluate(pointOnCurve);
        float damage = y * WeaponInfo.damage;

        return damage;
    }

    public void MakeSecondary()
    {
        zoomFactor = ScopeSettings.rangeIncrease * ScopeSettings.zoom;
    }

    public void ReleaseSecondary()
    {
        zoomFactor = 0f;
    }
}
