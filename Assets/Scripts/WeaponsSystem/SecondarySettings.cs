using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SecondarySettings
{
    [System.Serializable]
    public class Block : SecondarySettings
    {
        
    }

    [System.Serializable]
    public class Scope : SecondarySettings
    {
        [Min(0f), Tooltip("The amount of zoom added while scoping")]
        public int zoom = 5;
        [Range(1f, 2f), Tooltip("The range increase per zoom stat")]
        public float rangeIncrease = 1.05f;
    }
}
