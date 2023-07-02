using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    public Attack weapon_main;

    private bool holdMainAttack = false;

    private void Start()
    {
        InputManager.Instance.InputActions.Main.Enable();
    }

    private void Update()
    {
        float input = InputManager.Instance.InputActions.Main.Attack.ReadValue<float>();
        if (input > 0)
            Attack_Main();
        else
            holdMainAttack = false;
    }

    private void Attack_Main()
    {
        if (weapon_main == null)
            return;
        if (holdMainAttack && !weapon_main.fullauto)
            return;
        holdMainAttack = true;

        weapon_main.MakeAttack();
    }
}
