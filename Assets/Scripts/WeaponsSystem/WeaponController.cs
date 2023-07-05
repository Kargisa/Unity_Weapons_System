using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapons")]
    public Attack weapon_main;

    public bool HoldsMainAttack { get; set; } = false;
    public bool HoldsSecondary { get; set; } = false;
    private float mainAttackPressed;
    private float secondaryPressed;


    private void Start()
    {
        InputManager.Instance.InputActions.Main.Enable();
    }

    private void Update()
    {
        mainAttackPressed = InputManager.Instance.InputActions.Main.Attack.ReadValue<float>();
        secondaryPressed = InputManager.Instance.InputActions.Main.Secondary.ReadValue<float>();

        if (mainAttackPressed > 0)
            Attack_Main();
        else
            HoldsMainAttack = false;

        if (secondaryPressed > 0)
            Secondary();
        else
            HoldsSecondary = false;

        weapon_main.HoldsMainAttack = HoldsMainAttack;
        weapon_main.HoldsSecondary = HoldsSecondary;
    }

    private void Attack_Main()
    {
        if (weapon_main == null)
            return;
        if (HoldsMainAttack && !weapon_main.fullauto)
            return;
        HoldsMainAttack = true;

        weapon_main.MakeAttack();
    }

    private void Secondary()
    {
        if (weapon_main == null)
            return;
        HoldsSecondary = true;

        weapon_main.MakeSecondary();
    }
}
