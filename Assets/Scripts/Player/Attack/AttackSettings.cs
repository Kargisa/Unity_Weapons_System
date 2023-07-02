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
        public float speed = 200f;
    }

    [System.Serializable]
    public class RangeHitscan : AttackSettings
    {
        [Header("Passives")]
        [Min(0f), Tooltip("The rounds per minute")]
        public float RPM = 140f;
        [Header("Range")]
        [Min(0f), Tooltip("The distance at witch the damage starts to fall off")]
        public float minFalloffRange = 10f;
        [Min(0f), Tooltip("The distance at witch the damage has fallen off to the lowest value in the curve")]
        public float maxFalloffRange = 750f;
        [Tooltip("The curve that determines the falloff of the damage")]
        public AnimationCurve falloffCurve;
    }

    [System.Serializable]
    public class Bullets : AttackSettings
    {
        [Header("Passives")]
        [Min(0f), Tooltip("The rounds per minute")]
        public float RPM = 140f;
        [Min(0f), Tooltip("The time to live of the bullet")]
        public float ttl = 30f;
        [Header("Range")]
        [Min(0f), Tooltip("The distance at witch the damage starts to fall off")]
        public float minFalloffRange = 10f;
        [Min(0f), Tooltip("The distance at witch the damage has fallen off to the lowest value in the curve")]
        public float maxFalloffRange = 75f;
        [Tooltip("The curve that determines the falloff of the damage")]
        public AnimationCurve falloffCurve;
        [Min(0f), Tooltip("The force the weapon shoots with")]
        public float force = 5000f;
    }
}