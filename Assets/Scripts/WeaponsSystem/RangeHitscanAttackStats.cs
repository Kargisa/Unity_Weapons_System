using UnityEngine;

[CreateAssetMenu(menuName = "WeaponsSystem/Stats/RangeHitscanStats")]
public class RangeHitscanAttackStats : ScriptableObject
{
    public AttackSettings.RangeHitscan rangeHitscanSettings;
    public SecondarySettings.Scope scopeHitscanSettings;
}