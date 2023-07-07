using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SecondarySettings
{
    [System.Serializable]
    public class Block : SecondarySettings
    {
        [Range(0f, 100f), Tooltip("The amount of damage reduced from incoming attacks in percent %")]
        public float damageReduction = 10f;
    }

    [System.Serializable]
    public class Scope : SecondarySettings
    {
        [Min(0f), Tooltip("The time the weapon needs to fully scope in")]
        public float scopeinTime = 0.2f;
        [Min(0f), Tooltip("The amount of fieldOfView to zoom in")]
        public int zoom = 5;
        [Range(0f, 2f), Tooltip("The range increase per zoom stat in meters")]
        public float rangeIncrease = 0.5f;
    }
}
