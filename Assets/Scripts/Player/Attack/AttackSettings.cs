using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class AttackSettings
{
    [Header("General")]
    [Min(0f)]
    public float damage = 5f;

    [System.Serializable]
    public class Melee : AttackSettings
    {
        [Header("Passives")]
        [Min(0f), Tooltip("The attacks per minute")]
        public float speed;
    }

    [System.Serializable]
    public class RangeHitscan : AttackSettings
    {
        [Header("Passives")]
        [Min(0f), Tooltip("The rounds per minute")]
        public float RPM;
        [Header("Range")]
        [Min(0f), Tooltip("The range the damage starts to fall off")]
        public float minFalloffRange;
        [Min(0f), Tooltip("The range the damage has fallen off to zero")]
        public float maxFalloffRange;
    }

    [System.Serializable]
    public class Bullets : AttackSettings
    {
        [Header("Passives")]
        [Min(0f), Tooltip("The rounds per minute")]
        public float RPM;
        [Min(0f), Tooltip("The time the bullet lives in seconds")]
        public float ttl;
        [Header("Range")]
        [Min(0f), Tooltip("The range the damage starts to fall off")]
        public float minFalloffRange;
        [Min(0f), Tooltip("The range the damage has fallen off to zero")]
        public float maxFalloffRange;
        [Min(0f), Tooltip("The force the weapon shoots with")]
        public float force;
    }
}