using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class AttackSettings
{
    [Min(0f)]
    public float damage = 5f;

    [System.Serializable]
    public class Melee : AttackSettings
    {
        [Header("Melee")]
        [Min(0f), Tooltip("The attack speed in attack per seconds")]
        public float speed;
    }

    [System.Serializable]
    public class Range : AttackSettings
    {
        [Header("Range")]
        [Min(0f), Tooltip("The moment the damage starts to fall off")]
        public float minFalloffRange;
        [Min(0f), Tooltip("The moment the damage has fallen off to zero")]
        public float maxFalloffRange;
        [Min(0f), Tooltip("The rounds per minute")]
        public float RPM;
    }
}