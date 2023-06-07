using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Attack : MonoBehaviour
{
#if UNITY_EDITOR
    [HideInInspector] public bool foldSettings;
#endif

    public AttackStats attackMaker;

    [SerializeField, HideInInspector] private IAttackType attackType;

    private void OnEnable()
    {
        attackType ??= AttackTypeGenerator.GenerateAttackType(attackMaker);
    }

    public void MakeAttack()
    {
          attackType.MakeAttack();
    }
}
