using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Type/Sword")]
public class Knife : ScriptableObject, IWeaponType
{
    public IEnumerator Animate(Transform attackTransform, object data)
    {
        //throw new System.NotImplementedException();
        yield return null;
    }

    public void Destroy()
    {
        //throw new System.NotImplementedException();
    }

    public void Initialize(Transform parent)
    {
        //throw new System.NotImplementedException();
    }
}