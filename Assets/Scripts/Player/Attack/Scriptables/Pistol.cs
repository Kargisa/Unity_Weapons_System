using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Type/Pistol")]
public class Pistol : ScriptableObject, IWeaponType
{
    public GameObject bulletPrefab;

    public IEnumerator Animate(Transform attackPoint, object data)
    {
        Vector3 force = (Vector3)data;

        GameObject bullet = Instantiate(bulletPrefab, attackPoint.position, attackPoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(force);
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

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
