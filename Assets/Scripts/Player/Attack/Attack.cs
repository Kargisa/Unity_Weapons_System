using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
#if UNITY_EDITOR
    [HideInInspector] public bool foldSettings;
#endif

    public AttackStats attackStats;

    [HideInInspector] public IAttackType attackType;
    [HideInInspector] public IWeapon weaponType;
    [HideInInspector] public AttackType attackT;

    float timeOfLastShot = 0;


    /// <summary>
    /// The <b>point</b> from where the attack emerges from
    /// </summary>
    [HideInInspector] public Transform attackAnchor;

    private void OnEnable()
    {
        attackType = attackStats.GenerateAttackType();
        weaponType = attackStats.GenerateWeapon();
        attackAnchor = transform.Find("AttackAnchor");
        timeOfLastShot = Time.time;
        InitAttack();
    }

    private void InitAttack()
    {
        weaponType.Initialize(transform, attackStats.attackType == AttackType.Range ? attackStats.rangeSettings : attackStats.meleeSettings);
    }

    public void MakeAttack()
    {
        float timeBetweenShots = Time.time - timeOfLastShot;

        switch (attackStats.attackType)
        {
            case AttackType.Range:
                if (timeBetweenShots <= 60 / attackStats.rangeSettings.RPM)
                    return;
                break;
            case AttackType.Melee:
                if (timeBetweenShots <= 60 / attackStats.meleeSettings.speed)
                    return;
                break;
        }

        Vector3 hitPoint = attackType.MakeAttack(attackAnchor);
        StartCoroutine(weaponType.Animate(attackAnchor, hitPoint));
        timeOfLastShot = Time.time;
    }

    private void OnDisable()
    {
        weaponType.Destroy();
    }
}
