using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystems : Singleton<InputSystems>
{
    public bool SprintInput { get; private set; }
    public bool InteractInput { get; private set; }

    private PlayerInputActions playerInputActions;

    protected override void Awake()
    {
        base.Awake();
        playerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        playerInputActions.Enable();
        playerInputActions.Player.Sprint.started += OnSprintToggle;
        playerInputActions.Player.Interact.started += OnInteract;
    }

    private void OnDisable()
    {
        playerInputActions.Disable();
        playerInputActions.Player.Sprint.started -= OnSprintToggle;
        playerInputActions.Player.Interact.started -= OnInteract;
    }

    public Vector2 GetMoveInput => playerInputActions.Player.Move.ReadValue<Vector2>();

    public Vector2 GetMouseInput => playerInputActions.Player.Look.ReadValue<Vector2>();

    private void OnSprintToggle(InputAction.CallbackContext obj)
    {
        SprintInput = !SprintInput;
    }

    public void UseSprintInput() => SprintInput = false;

    private void OnInteract(InputAction.CallbackContext obj)
    {
        InteractInput = !InteractInput;
    }

    public void UseInteractInput() => InteractInput = false;

    public bool OnSelect => playerInputActions.Player.Select.triggered;
}