using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IAttackType
{
    public AttackSettings.Melee Settings { get; set; }

    public MeleeAttack(AttackSettings.Melee settings)
    {
        Settings = settings;
    }

    public void MakeAttack()
    {
        throw new System.NotImplementedException();
    }
}
