using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IPlayer
{
    [Header("Main")]
    [SerializeField]
    private float hp = 100f;

    public float HP
    {
        get { return hp; }
        set
        {
            if (value < hp)
            {
                float dmg = hp - value;
                value = hp - dmg * (1 - normalResistance * 0.01f);
                hp = value;
            }
            else
                hp = value;
        }
    }

    public float speed = 5f;
    public float mouseSensitivitie = 0.1f;
    public float jumpForce = 6f;

    [Header("Stats")]
    public float normalResistance = 0f;

    [Header("Jump")]
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _maxGroundDistance;
    [SerializeField] private LayerMask _groundMask;
    [HideInInspector] public bool grounded = false;

    private Rigidbody _rb;
    [HideInInspector] public Camera firstPersonCamera;
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
        firstPersonCamera = Camera.main;
    }

    private void Start()
    {
        InputManager.Instance.LockMouse(true);
        InputManager.Instance.InputActions.Main.Enable();
        InputManager.Instance.InputActions.Main.Jump.performed += Jump;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            HP -= 5;

        ReadInputs();
        Look();
    }

    private void FixedUpdate()
    {
        UpdateGroundCheck();
        Move();
    }

    public void SetResistanceValues(float normalResistance)
    {
        this.normalResistance += normalResistance;
    }

    private void Look()
    {
        transform.Rotate(transform.up, _mouseDelta.x * mouseSensitivitie);

        rotationX -= _mouseDelta.y * mouseSensitivitie;
        rotationX = Mathf.Clamp(rotationX, -90, 85);

        firstPersonCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
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
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawRay(_groundCheck.position, Vector3.down * _maxGroundDistance);
    }
}
