using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputSystem_Actions inputs;
    private CharacterController controller;
    private Vector2 moveInput;
    public CinemachineCamera characterCamera;


    [SerializeReference] private float moveSpeed = 10f;
    public float rotationSpeed = 200f;

    private void Awake()
    {
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
        if (moveInput != Vector2.zero)
        {
            Quaternion TargetQuaternion = Quaternion.LookRotation(cameraForwardDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, TargetQuaternion, rotationSpeed * Time.deltaTime);
        }
        Vector3 moveDir =(cameraForwardDir*moveInput.y+transform.right*moveInput.x)* moveSpeed;
        controller.Move(moveDir*Time.deltaTime);
    }
}
