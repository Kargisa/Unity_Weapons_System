using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    public Attack weapon_main;

    private void Start()
    {
        InputManager.Instance.InputActions.Main.Enable();
        InputManager.Instance.InputActions.Main.Shoot.performed += Attack_Main;
    }

    private void Attack_Main(InputAction.CallbackContext context)
    {
        if (weapon_main == null)
            return;

        weapon_main.MakeAttack();
    }

    //Rotate weapon to camera maby using unit circle

    private void OnDestroy()
    {
        InputManager.Instance.InputActions.Main.Shoot.performed -= Attack_Main;
    }
}
