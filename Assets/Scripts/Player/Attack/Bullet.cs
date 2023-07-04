using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletData bulletData;
    AttackSettings.Bullets settings;
    Rigidbody _rb;

    public void Initalize(object data)
    {
        bulletData = data as BulletData;
        if (bulletData == null)
            throw new System.InvalidCastException($"{nameof(bulletData)} can not be cast into {nameof(BulletData)}");

        settings = bulletData.Settings;

        _rb = GetComponent<Rigidbody>();
        _rb.mass = settings.mass;
        _rb.useGravity = false;
        _rb.collisionDetectionMode = settings.collisionDetectionMode;
        _rb.AddForce(bulletData.Force, ForceMode.VelocityChange);

        Destroy(gameObject, settings.ttl);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (bulletData == null)
            return;

        if (collision.collider.TryGetComponent(out Rigidbody rb))
        {
            Vector3 hitpoint = collision.GetContact(0).point;
            Vector3 direction = (hitpoint - bulletData.Origin).normalized;
            rb.AddForceAtPosition(direction * settings.pushForce, hitpoint, settings.pushForceMode);
        }

        float damage = bulletData.OnCalculateDamage(bulletData.Origin, collision.GetContact(0).point);

        Destroy(gameObject);
    }
}
