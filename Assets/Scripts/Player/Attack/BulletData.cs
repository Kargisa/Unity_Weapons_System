using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    public BulletData(float ttl, Vector3 force, Vector3 origin, Func<Vector3, Vector3, float> dealDamage)
    {
        TTL = ttl;
        Force = force;
        Origin = origin;
        OnDamageDealt = dealDamage;
    }

    /// <summary>
    /// The origin of the bullet.
    /// </summary>
    public Vector3 Origin { get; set; }

    /// <summary>
    /// The time to live of the bullet.
    /// </summary>
    public float TTL { get; set; }

    /// <summary>
    /// The force of the bullet.
    /// </summary>
    public Vector3 Force { get; set; }

    /// <summary>
    /// Calculates the damage to a target
    /// </summary>
    public Func<Vector3, Vector3, float> OnDamageDealt { get; set; }
}
