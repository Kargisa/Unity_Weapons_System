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
    public Transform anchor;

    [System.Serializable]
    public class Melee : AttackSettings
    {
        [Header("Melee")]
        public float speed;
    }

    [System.Serializable]
    public class Range : AttackSettings
    {

        [Header("Range")]
        public float range;
        public float fireRate;
    }
}