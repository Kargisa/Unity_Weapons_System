using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
#if UNITY_EDITOR
    [HideInInspector] public bool foldSettings;
    [HideInInspector] public bool foldWeapon;
    [HideInInspector] public bool lockAttackType;
    [HideInInspector] public bool lockWeaponType;
#endif

    [HideInInspector] public IAttackType attackType;
    [HideInInspector] public IWeaponType weaponType;

    [HideInInspector] public AttackType attackT;
    [HideInInspector] public RangeAttackStats rangeAttackStats;
    [HideInInspector] public MeleeAttackStats meleeAttackStats;


    #region Range Weapons
    //Range Weapons ScriptableObjects
    [HideInInspector]
    public RangeWeaponType rangeHitscanWeaponType;

    [HideInInspector]
    public Railgun railgun;
    #endregion

    #region Melee Weapons
    //Melee Weapons ScriptableObjects
    [HideInInspector]
    public MeleeWeaponType meleeWeaponType;
    #endregion


    float timeOfLastAttack = 0;

    Vector3 hitPoint;

    /// <summary>
    /// The <b>point</b> from where the attack emerges from
    /// </summary>
    [HideInInspector] public Transform attackAnchor;

    private void OnEnable()
    {
        if (rangeAttackStats == null)
        {
            Debug.LogError($"No {nameof(rangeAttackStats)} defined");
            return;
        }
        InitAttack();
    }
    
    private void InitAttack()
    {
        attackType = this.GenerateAttackType();
        weaponType = this.GetWeapon();
        attackAnchor = transform.Find("AttackAnchor");
        if (attackAnchor == null)
            throw new ArgumentException($"Missing Child of object {name}: AttackAnchor");
        weaponType.Initialize(transform);
    }


    /// <summary>
    /// Makes Attack and Animation using the current <b>AttackType</b> and <b>WeaponType</b>
    /// </summary>
    public void MakeAttack()
    {
        float timeBetweenShots = Time.time - timeOfLastAttack;

        switch (attackT)
        {
            case AttackType.RangeHitscan:
                if (timeBetweenShots <= 60 / rangeAttackStats.rangeHitscanSettings.RPM)
                    return;
                break;
            case AttackType.Melee:
                if (timeBetweenShots <= 60 / meleeAttackStats.meleeSettings.speed)
                    return;
                break;
        }

        hitPoint = attackType.MakeAttack(attackAnchor);
        StartCoroutine(weaponType.Animate(attackAnchor, hitPoint));
        timeOfLastAttack = Time.time;
    }

    private void OnDisable()
    {
        weaponType.Destroy();
    }
}
