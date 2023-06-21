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

    public static IWeaponType GenerateWeapon(this Attack attack)
    {
        switch (attack.attackT)
        {
            case AttackType.Range:
                return attack.rangeWeaponType switch
                {
                    RangeWeaponType.Railgun => attack.railgun,
                    RangeWeaponType.None => null,
                    _ => null
                };
            case AttackType.Melee:
                return attack.meleeWeaponType switch
                {
                    MeleeWeaponType.Sword => null,
                    _ => null
                };
            default:
                return null;
        }
    }

}
