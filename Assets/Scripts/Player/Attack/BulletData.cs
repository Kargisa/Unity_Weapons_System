using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData
{
    public BulletData(AttackSettings.Bullets settings, Vector3 force, Vector3 origin, Func<Vector3, Vector3, float> calculateDamage)
    {
        Settings = settings;
        Force = force;
        Origin = origin;
        OnCalculateDamage = calculateDamage;
    }

    /// <summary>
    /// The AttackSettings of the weapon
    /// </summary>
    public AttackSettings.Bullets Settings { get; set; }

    /// <summary>
    /// The force of the bullet
    /// </summary>
    public Vector3 Force { get; set; }

    /// <summary>
    /// The origin of the bullet
    /// </summary>
    public Vector3 Origin { get; set; }

    /// <summary>
    /// Calculates the damage of the attack
    /// </summary>
    public Func<Vector3, Vector3, float> OnCalculateDamage { get; set; }

}
