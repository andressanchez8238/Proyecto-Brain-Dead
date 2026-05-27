using System.ComponentModel.Design.Serialization;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private StatsPlayer statsPlayer;
    private InputSystem_Actions inputs;
    private CharacterController controller;
    private Vector2 moveInput;
    public CinemachineCamera characterCamera;
    public Vector3 DampingCamera;

    private bool IsSprint;


    public float rotationSpeed = 200f;
    [SerializeReference] private float moveSpeed = 10f;
    [SerializeReference] private float SprintSpeed = 15f;
    [SerializeField] private float DampingSprint=0.1f;
    [SerializeField] private float velocityTransicion = 5f;
    [SerializeReference] private float jumpForce = 10f;
    private float verticalVelocity;

    private void Awake()
    {
        statsPlayer = GetComponent<StatsPlayer>();
        inputs = new();
        controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        inputs.Player.Sprint.performed += OnSprint;
        inputs.Player.Sprint.canceled += OnSprint;
        inputs.Player.Jump.performed += OnJump;

    }
    void Start()
    {
        
    }
    void Update()
    {
        OnMove();
    }
    public void OnMove()
    {
        Vector3 cameraForwardDir=characterCamera.transform.forward;
        cameraForwardDir.y = 0;
        cameraForwardDir.Normalize();
        Vector3 moveDir;
        if (moveInput != Vector2.zero)
        {
            Quaternion TargetQuaternion = Quaternion.LookRotation(cameraForwardDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetQuaternion, rotationSpeed * Time.deltaTime);
        }
        if (IsSprint && statsPlayer.Stamina>=0)
        {
            DampingCamera=new Vector3 (DampingSprint,DampingSprint,DampingSprint);
            moveDir = (cameraForwardDir * moveInput.y + transform.right * moveInput.x) * SprintSpeed;
            statsPlayer.StaminaRecarga = false;
            statsPlayer.DisminuirStamina();
            statsPlayer.cooldownStamina = 0f;
        }
        else
        {
            DampingCamera=Vector3.zero;
            moveDir = (cameraForwardDir * moveInput.y + transform.right * moveInput.x) * moveSpeed;
            statsPlayer.StaminaRecarga= true;
            statsPlayer.cooldownStamina+= Time.deltaTime;
            if (statsPlayer.cooldownStamina >= statsPlayer.cooldownStaminaTotal)
            {
                statsPlayer.AumentarStamina();
            }
        }

        CinemachineOrbitalFollow orbitalFollow=characterCamera.GetComponent<CinemachineOrbitalFollow>();
        if (orbitalFollow != null) 
        {
            orbitalFollow.TrackerSettings.PositionDamping = Vector3.Lerp(orbitalFollow.TrackerSettings.PositionDamping, DampingCamera, Time.deltaTime * velocityTransicion);
        }

        verticalVelocity += Physics.gravity.y * Time.deltaTime;
        if (controller.isGrounded && verticalVelocity < 0)
            verticalVelocity = -2f;
        moveDir.y = verticalVelocity;

        controller.Move(moveDir*Time.deltaTime);
    }
    public void OnSprint(InputAction.CallbackContext Context)
    {
        IsSprint = Context.performed;
    }
    private void OnJump(InputAction.CallbackContext Context)
    {
        if (!controller.isGrounded) return;
        verticalVelocity = jumpForce;
        if (statsPlayer.Stamina >= 5)
        {
            statsPlayer.Stamina -= 5;
            statsPlayer.cooldownStamina = 0f;
        }
    }
    private void CambioDamping()
    {

    }
}
