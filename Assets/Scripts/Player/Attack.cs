using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
#if UNITY_EDITOR
    [HideInInspector] public bool foldSettings;
#endif

    public AttackMaker settings;
}
