using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeaponType
{
    public Camera Camera { get; set; }

    /// <summary>
    /// Initializes all settings for the weapon
    /// </summary>
    /// <param name="parent">The parent for the weapon</param>
    public void Initialize(Transform parent, Camera camera);

    /// <summary>
    /// Animates the attack
    /// </summary>
    /// <param name="attackTransform">The point from where the attack emits from</param>
    /// <param name="data">The informational data from the attack (hitpoint, velocity, etc)</param>
    /// <returns></returns>
    public IEnumerator Animate(Transform attackTransform, object data);

    /// <summary>
    /// Makes the secondary move of the attack
    /// </summary>
    public IEnumerator AnimateSecondary(object data);

    /// <summary>
    /// Releases the secondary move of the attack
    /// </summary>
    public IEnumerator AnimateReleaseSecondary(object data);

    /// <summary>
    /// Destroys the weapons dependencies
    /// </summary>
    public void Destroy();
}
