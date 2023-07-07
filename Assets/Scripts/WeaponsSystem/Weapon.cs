using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
#if UNITY_EDITOR
    [HideInInspector] public bool foldSettings;
    [HideInInspector] public bool foldWeapon;
    [HideInInspector] public bool lockAttackType;
    [HideInInspector] public bool lockWeaponType;
#endif

    public IAttackType attackType;
    public IWeaponType weaponType;

    [HideInInspector] public AttackType attackT;
    [HideInInspector] public RangeHitscanAttackStats rangeHitscanAttackStats;
    [HideInInspector] public BulletAttackStats bulletAttackStats;
    [HideInInspector] public MeleeHitscanAttackStats meleeHitscanAttackStats;

    [HideInInspector] public bool fullauto = false;
    [HideInInspector] public Camera firstpersonCamera;
    [HideInInspector] public IPlayer player;

    #region Range Hitscan Weapons
    //Range Hitscan Weapons ScriptableObjects
    [HideInInspector]
    public RangeHitscanWeaponType rangeHitscanWeaponType;

    [HideInInspector]
    public Railgun railgun;
    #endregion

    #region Range Bullet Weapons
    //Range Hitscan Weapons ScriptableObjects
    [HideInInspector]
    public BulletWeaponType bulletWeaponType;

    [HideInInspector]
    public Pistol pistol;
    #endregion

    #region Melee Hitscan Weapons
    //Melee Weapons ScriptableObjects
    [HideInInspector]
    public MeleeHitscanWeaponType meleeHitscanWeaponType;

    [HideInInspector]
    public Knife knife;
    #endregion

    [HideInInspector] public bool holdsMainAttack;
    [HideInInspector] public bool holdsSecondary;

    float timeOfLastAttack = 0;


    /// <summary>
    /// The <b>point</b> from where the attack emerges from
    /// </summary>
    [HideInInspector] public Transform attackAnchor;

    private void OnEnable()
    {
        InitAttack();
    }

    private void InitAttack()
    {

        firstpersonCamera = Camera.main;
        attackType = this.GenerateAttackType();
        weaponType = this.GetWeapon();
        attackAnchor = transform.Find("AttackAnchor");
        if (attackAnchor == null)
            throw new ArgumentException($"Missing Child of object {name}: AttackAnchor");
        weaponType.Initialize(transform, firstpersonCamera);
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
                if (timeBetweenShots <= 60 / rangeHitscanAttackStats.rangeHitscanSettings.RPM)
                    return;
                break;
            case AttackType.Bullet:
                if (timeBetweenShots <= 60 / bulletAttackStats.bulletsSettings.RPM)
                    return;
                break;
            case AttackType.MeleeHitscan:
                if (timeBetweenShots <= 60 / meleeHitscanAttackStats.meleeHitscanSettings.speed)
                    return;
                break;
        }

        object data = attackType.MakeAttack(attackAnchor);
        StartCoroutine(weaponType.AnimateMain(attackAnchor, data));
        timeOfLastAttack = Time.time;
    }

    public void MakeSecondary()
    {
        attackType.MakeSecondary();
        StartCoroutine(weaponType.AnimateSecondary(attackType.SecondarySettings));
    }

    public void ReleaseSecondary()
    {
        attackType.ReleaseSecondary();
        StartCoroutine(weaponType.AnimateReleaseSecondary(attackType.SecondarySettings));
    }

    private void OnDisable()
    {
        weaponType.Destroy();
    }
}
