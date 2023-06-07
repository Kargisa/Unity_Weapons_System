using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    [Header("Attack")]
    public Attack weapon_main;

    [Header("Jump")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _maxGroundDistance;
    [SerializeField] private LayerMask _groundMask;
    [HideInInspector] public bool grounded;

    [SerializeField, HideInInspector] private Rigidbody _rb;

    private Vector3 _moveDirection;

#if UNITY_EDITOR
    [HideInInspector]
    public bool foldoutInfo = true;
#endif
    
    void Awake()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        InputManager.Instance.InputActions.Main.Enable();
        InputManager.Instance.InputActions.Main.Jump.performed += Jump;
        InputManager.Instance.InputActions.Main.Shoot.performed += Attack_Main;
    }

    private void Update()
    {
        _moveDirection = InputManager.Instance.InputActions.Main.Move.ReadValue<Vector3>();
    }

    private void FixedUpdate()
    {
        UpdateGroundCheck();
        Move();
    }

    private void Move()
    {
        _rb.velocity = new Vector3(_moveDirection.x * speed, _rb.velocity.y, _moveDirection.z * speed);
    }

    private void Attack_Main(InputAction.CallbackContext context)
    {
        if (weapon_main == null)
            return;

        weapon_main.MakeAttack();
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (!grounded)
            return;

        _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);

    }

    private void UpdateGroundCheck()
    {
        Ray ray = new Ray(_groundCheck.position, Vector3.down);
        grounded = Physics.Raycast(ray, _maxGroundDistance, _groundMask);
    }

    private void OnDisable()
    {
        InputManager.Instance.InputActions.Main.Jump.performed -= Jump;
        InputManager.Instance.InputActions.Main.Shoot.performed -= Attack_Main;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_groundCheck.position, Vector3.down * _maxGroundDistance);
    }
}
