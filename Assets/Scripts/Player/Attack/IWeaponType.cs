using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponType
{

    /// <summary>
    /// Initializes all settings for the weapon
    /// </summary>
    /// <param name="parent">The parent for the weapon</param>
    public void Initialize(Transform parent);

    /// <summary>
    /// Animates the attack
    /// </summary>
    /// <param name="attackPoint">The point from where the attack emits from</param>
    /// <param name="hitPoint">The point where the attack hit in world space</param>
    /// <returns></returns>
    public IEnumerator Animate(Transform attackPoint, Vector3 hitPoint);

    /// <summary>
    /// Destroys the weapon
    /// </summary>
    public void Destroy();
}
