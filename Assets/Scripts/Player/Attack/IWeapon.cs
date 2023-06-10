using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeapon
{
    public void Initialize(Transform transform, AttackSettings attackSettings);
    public IEnumerator Animate(Transform shootPoint, Vector3 hitPoint);
    public void Destroy();
}
