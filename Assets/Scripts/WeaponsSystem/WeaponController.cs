using System.Collections;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public Player player;
    [Header("Weapons")]
    public Weapon weapon_main;

    [HideInInspector] public bool holdsMainAttack = false;
    [HideInInspector] public bool holdsSecondary = false;
    private float mainAttackPressed;
    private float secondaryPressed;


    private void Start()
    {
        if (player == null)
        {
            Debug.LogWarning($"{nameof(player)} was null, getting component from gameObject");
            if (!TryGetComponent(out player))
                throw new MissingComponentException($"{gameObject.name} has no component type of {nameof(Player)}");
        }
        if (weapon_main != null)
            PickUpMainWeapon(weapon_main);

        InputManager.Instance.InputActions.Main.Enable();
    }

    public void PickUpMainWeapon(Weapon weapon)
    {
        if (player is not IPlayer)
            throw new System.NotImplementedException($"{nameof(player)} does not implement the interface {nameof(IPlayer)}");
        weapon_main = weapon;
        weapon_main.player = player;
    }

    private void Update()
    {
        mainAttackPressed = InputManager.Instance.InputActions.Main.Attack.ReadValue<float>();
        secondaryPressed = InputManager.Instance.InputActions.Main.Secondary.ReadValue<float>();

        if (mainAttackPressed > 0)
            Attack_Main();
        else
            holdsMainAttack = false;

        if (secondaryPressed > 0)
            Secondary();
        else
            ReleaseSecondary();

        weapon_main.holdsMainAttack = holdsMainAttack;
        weapon_main.holdsSecondary = holdsSecondary;
    }

    private void Attack_Main()
    {
        if (weapon_main == null)
            return;
        if (holdsMainAttack && !weapon_main.fullauto)
            return;
        holdsMainAttack = true;

        weapon_main.MakeAttack();
    }

    private void Secondary()
    {
        if (weapon_main == null || holdsSecondary)
            return;
        holdsSecondary = true;
        weapon_main.MakeSecondary();
    }

    private void ReleaseSecondary()
    {
        if (weapon_main == null || !holdsSecondary)
            return;
        holdsSecondary = false;
        weapon_main.ReleaseSecondary();
    }
}
