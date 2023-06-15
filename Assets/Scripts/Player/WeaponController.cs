using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Player))]
public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    public Attack weapon_main;

    private Player _player;

    private void Start()
    {
        InputManager.Instance.InputActions.Main.Enable();
        InputManager.Instance.InputActions.Main.Shoot.performed += Attack_Main;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        Quaternion camRotation = _player.camera.transform.localRotation;
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
