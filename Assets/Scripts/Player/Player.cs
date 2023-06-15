using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed;
    public float mouseSensitivitie;
    public float jumpForce;

    [Header("Attack")]
    public Attack weapon_main;

    [Header("Jump")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _maxGroundDistance;
    [SerializeField] private LayerMask _groundMask;
    [HideInInspector] public bool grounded;

    [SerializeField] private Rigidbody _rb;
    private Camera _camera;
    private float rotationX = 0;

    private Vector3 _moveDirection;
    private Vector2 _mouseDelta;

#if UNITY_EDITOR
    [HideInInspector]
    public bool foldoutInfo = true;
#endif

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    private void Start()
    {
        InputManager.Instance.LockMouse(true);
        InputManager.Instance.InputActions.Main.Enable();
        InputManager.Instance.InputActions.Main.Jump.performed += Jump;
        InputManager.Instance.InputActions.Main.Shoot.performed += Attack_Main;
    }

    private void Update()
    {
        ReadInputs();
        Look();
    }

    private void FixedUpdate()
    {
        UpdateGroundCheck();
        Move();
    }

    private void Look()
    {
        transform.Rotate(transform.up, _mouseDelta.x * mouseSensitivitie);
        
        rotationX -= _mouseDelta.y * mouseSensitivitie;
        rotationX = Mathf.Clamp(rotationX, -90, 85);

        _camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
    }

    private void ReadInputs()
    {
        _moveDirection = InputManager.Instance.InputActions.Main.Move.ReadValue<Vector3>();
        _mouseDelta = InputManager.Instance.InputActions.Main.Look.ReadValue<Vector2>();
    }

    private void Move()
    {
        Vector3 worldDir = transform.TransformDirection(_moveDirection);
        _rb.MovePosition(_rb.position + new Vector3(worldDir.x, 0, worldDir.z) * speed * Time.fixedDeltaTime);

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

    private void OnDestroy()
    {
        InputManager.Instance.InputActions.Main.Jump.performed -= Jump;
        InputManager.Instance.InputActions.Main.Shoot.performed -= Attack_Main;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_groundCheck.position, Vector3.down * _maxGroundDistance);
    }
}
