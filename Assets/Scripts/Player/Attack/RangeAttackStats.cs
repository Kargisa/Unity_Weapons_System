using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Stats/RangeStats")]
public class RangeAttackStats : ScriptableObject
{
    public AttackSettings.RangeHitscan rangeHitscanSettings;
}