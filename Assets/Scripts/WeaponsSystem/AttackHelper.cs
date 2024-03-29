using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackHelper
{
    /// <summary>
    /// Generates a new <c>IAttackType</c> depending on the <c>AttackType</c> in the <c>AttackStats</c>
    /// </summary>
    /// <param name="attack">The attack </param>
    /// <returns>A new <c>IAttackType</c></returns>
    public static IAttackType GenerateAttackType(this Weapon attack)
    {
        return attack.attackT switch
        {
            AttackType.RangeHitscan => new RangeHitscanAttack(attack.rangeHitscanAttackStats.rangeHitscanSettings, attack.rangeHitscanAttackStats.scopeRangeHitscanSettings, attack),
            AttackType.MeleeHitscan => new MeleeHitscanAttack(attack.meleeHitscanAttackStats.meleeHitscanSettings,attack.meleeHitscanAttackStats.blockMeleeHistcanSettings ,attack),
            AttackType.Bullet => new BulletAttack(attack.bulletAttackStats.bulletsSettings,attack.bulletAttackStats.scopeBulletSettings ,attack),
            _ => null,
        };
    }

    /// <summary>
    /// Gets the <c>IWeaponType</c> of the <c>Attack</c>
    /// </summary>
    /// <param name="attack">The attack from where the weapon comes from</param>
    /// <returns>The specific <c>IWeaponType</c> from the given <c>Attack</c></returns>
    public static IWeaponType GetWeapon(this Weapon attack)
    {
        return attack.attackT switch
        {
            AttackType.RangeHitscan => attack.rangeHitscanWeaponType switch
            {
                RangeHitscanWeaponType.Railgun => attack.railgun,
                RangeHitscanWeaponType.None => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException($"{attack.rangeHitscanWeaponType} not implemeted")
            },
            AttackType.MeleeHitscan => attack.meleeHitscanWeaponType switch
            {
                MeleeHitscanWeaponType.Knife => attack.knife,
                _ => throw new System.NotImplementedException($"{attack.meleeHitscanWeaponType} not implemeted")
            },
            AttackType.Bullet => attack.bulletWeaponType switch
            {
                BulletWeaponType.Pistol => attack.pistol,
                _ => throw new System.NotImplementedException($"{attack.bulletWeaponType} not implemeted")
            },
            _ => throw new NotImplementedException()
        };
    }

}
