using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttackType
{
    public SecondarySettings SecondarySettings { get; }

    /// <summary>
    /// Makes the attack using the point from where it starts
    /// </summary>
    /// <param name="attackTransform">the point where the attack emits from</param>
    /// <returns>Data produced by the attack</returns>
    public object MakeAttack(Transform attackTransform);

    /// <summary>
    /// Makes the secondray move of the attack
    /// </summary>
    public void MakeSecondary();

    /// <summary>
    /// Makes a move when the secondary move stops
    /// </summary>
    public void ReleaseSecondary();

    /// <summary>
    /// Calculates the damage to a target
    /// </summary>
    /// <param name="origin">The origin of the attack</param>
    /// <param name="hitpoint">The point where the attack hit</param>
    /// <returns>The damage that will be dealt to a target</returns>
    /// <exception cref="System.NotImplementedException"></exception>
    public float CalculateDamage(Vector3 origin, Vector3 hitpoint);

}