using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IAttackType
{
    public AttackSettings.Range Settings { get; private set; }

    public RangeAttack(AttackSettings.Range settings)
    {
        Settings = settings;
    }

    public void MakeAttack()
    {
        
    }

}
