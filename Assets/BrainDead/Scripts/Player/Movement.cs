using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private InputSystem_Actions inputs;
    private CharacterController controller;
    private Vector2 moveInput;

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
        
    }
    public void OnMove()
    {

    }
}
