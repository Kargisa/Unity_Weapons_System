using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackHelper
{
    public static IAttackType GenerateAttackType(this AttackStats stats)
    {
        return stats.attackType switch
        {
            AttackType.Range => new RangeAttack(stats.rangeSettings),
            AttackType.Melee => new MeleeAttack(stats.meleeSettings),
            _ => null,
        };
    }

    public static IWeapon GenerateWeapon(this AttackStats stats)
    {
        switch (stats.attackType)
        {
            case AttackType.Range:
                return stats.rangeWeaponType switch
                {
                    RangeWeaponType.Railgun => stats.railgun,
                    RangeWeaponType.None => null,
                    _ => null
                };
            case AttackType.Melee:
                return stats.meleeWeaponType switch
                {
                    MeleeWeaponType.Sword => null,
                    _ => null
                };
            default:
                return null;
        }
    }

}
