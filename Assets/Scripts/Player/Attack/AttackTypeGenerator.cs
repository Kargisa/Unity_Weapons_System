using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AttackTypeGenerator
{
    public static IAttackType GenerateAttackType(AttackStats stats)
    {
        return stats.attackType switch
        {
            AttackType.Range => new RangeAttack(stats.rangeSettings),
            AttackType.Melee => new MeleeAttack(stats.meleeSettings),
            _ => null,
        };
    }
}
