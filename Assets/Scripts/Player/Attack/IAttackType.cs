using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackType
{
    /// <summary>
    /// Calculates the attack using the point from where it starts
    /// </summary>
    /// <param name="attackAnchor">the point where the attack emits from</param>
    /// <returns>The point where the attack hit in world space</returns>
    public object MakeAttack(Transform attackAnchor);
}
