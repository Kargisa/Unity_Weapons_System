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
    /// <param name="attackTransform">The point from where the attack emits from</param>
    /// <param name="data">The informational data from the attack (hitpoint, velocity, etc)</param>
    /// <returns></returns>
    public IEnumerator Animate(Transform attackTransform, object data);

    /// <summary>
    /// Destroys the weapons dependencies
    /// </summary>
    public void Destroy();
}
