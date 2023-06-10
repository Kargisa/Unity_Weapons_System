using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackType
{
    public Vector3 MakeAttack(Transform attackAnchor);
}
