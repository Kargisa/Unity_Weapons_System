using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    BulletData bulletData;

    public void Initalize(object data)
    {
        bulletData = (BulletData)data;
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rb.AddForce(bulletData.Force);

        Destroy(gameObject, bulletData.TTL);
    }

    void OnCollisionEnter(Collision collision)
    {
        float damage = bulletData.OnDamageDealt(bulletData.Origin, collision.GetContact(0).point);
        Debug.Log(damage);
        Destroy(gameObject);
    }
}
