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

    [SerializeField, HideInInspector] public IAttackType attackType;
    [SerializeField, HideInInspector] public IWeapon weaponType;


    /// <summary>
    /// The <b>point</b> from where the attack emerges from
    /// </summary>
    [HideInInspector] public Transform attackAnchor;

    private void OnEnable()
    {
        attackType ??= attackStats.GenerateAttackType();
        weaponType ??= attackStats.GenerateWeapon();
        if (attackAnchor == null)
            attackAnchor = transform.Find("AttackAnchor");
        InitAttack();
    }

    private void InitAttack()
    {
        weaponType.Initialize(transform, attackStats.attackType == AttackType.Range ? attackStats.rangeSettings : attackStats.meleeSettings);
    }

    public void MakeAttack()
    {
        Vector3 hitPoint = attackType.MakeAttack(attackAnchor);
        StartCoroutine(weaponType.Animate(attackAnchor, hitPoint));
    }

    private void OnDisable()
    {
        weaponType.Destroy();
    }
}
