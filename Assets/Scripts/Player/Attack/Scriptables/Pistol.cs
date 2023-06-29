using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Type/Pistol")]
public class Pistol : ScriptableObject, IWeaponType
{
    public GameObject bulletPrefab;

    public IEnumerator Animate(Transform attackPoint, object data)
    {
        throw new System.NotImplementedException();
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    public void Initialize(Transform parent)
    {
        throw new System.NotImplementedException();
    }
}
