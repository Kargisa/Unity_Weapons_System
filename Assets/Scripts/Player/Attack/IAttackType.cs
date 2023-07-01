using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackType
{
    /// <summary>
    /// Calculates the attack using the point from where it starts
    /// </summary>
    /// <param name="attackTransform">the point where the attack emits from</param>
    /// <returns>Data produced by the attack</returns>
    public object MakeAttack(Transform attackTransform);

    /// <summary>
    /// Calculates the damage to a target
    /// </summary>
    /// <returns>The amount of damage to be dealt</returns>
    public float OnDamageDealt(Vector3 origin, Vector3 hitpoint);
}