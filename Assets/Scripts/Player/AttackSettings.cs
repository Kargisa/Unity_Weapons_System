using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class AttackSettings
{
    [Min(0f)]
    public float damage = 5f;

    [System.Serializable]
    public class Melee : AttackSettings
    {
        public string sus;
    }

    [System.Serializable]
    public class Range : AttackSettings
    {
        public string bus;
    }
}
