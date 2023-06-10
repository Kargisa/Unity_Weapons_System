using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IAttackType
{
    public AttackSettings.Range Settings { get; private set; }
    public IWeapon Weapon { get; set; }

    public RangeAttack(AttackSettings.Range settings)
    {
        Settings = settings;
    }

    public Vector3 MakeAttack(Transform attackAnchor)
    {
        Ray ray = new Ray(attackAnchor.position, attackAnchor.forward);
        RaycastHit hit;
        bool didHit = Physics.Raycast(ray, out hit, Settings.range);

        if (didHit)
            return hit.point;

        return Vector3.zero;
    }

}
