using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Type/Pistol")]
public class Pistol : ScriptableObject, IWeaponType
{
    public GameObject bulletPrefab;

    public IEnumerator Animate(Transform attackPoint, object data)
    {
        BulletData bulletData = (BulletData)data;

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Bullet bl = bullet.GetComponent<Bullet>();
        
        bl.Initalize(data);

        yield return null;
    }

    public void Destroy()
    {
        
    }

    public void Initialize(Transform parent)
    {
        if (!bulletPrefab.TryGetComponent(out Bullet bullet))
            bulletPrefab.AddComponent<Bullet>();
        if (!bulletPrefab.TryGetComponent(out Rigidbody rb)) 
            bulletPrefab.AddComponent<Rigidbody>();
    }
}
