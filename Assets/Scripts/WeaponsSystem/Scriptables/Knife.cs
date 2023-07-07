using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Type/Sword")]
public class Knife : ScriptableObject, IWeaponType
{
    public Camera Camera { get; set; }

    public IEnumerator AnimateMain(Transform attackTransform, object data)
    {
        //throw new System.NotImplementedException();
        yield return null;
    }

    public IEnumerator AnimateReleaseSecondary(object data)
    {
        //throw new System.NotImplementedException();
        yield return null;
    }

    public IEnumerator AnimateSecondary(object data)
    {

        //throw new System.NotImplementedException();
        yield return null;
    }

    public void Destroy()
    {
        //throw new System.NotImplementedException();
    }

    public void Initialize(Transform parent, Camera camera)
    {
        Camera = camera;
    }
}
