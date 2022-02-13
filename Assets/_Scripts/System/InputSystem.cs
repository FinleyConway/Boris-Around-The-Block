using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystem : Singleton<InputSystem>
{
    public bool SprintInput { get; private set; }
    public bool InteractInput { get; private set; }

    private PlayerInputActions PlayerInputActions;

    protected override void Awake()
    {
        base.Awake();
        PlayerInputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        PlayerInputActions.Enable();
        PlayerInputActions.Player.Sprint.started += OnSprintToggle;
        PlayerInputActions.Player.Interact.started += OnInteract;
    }

    private void OnDisable()
    {
        PlayerInputActions.Disable();
        PlayerInputActions.Player.Sprint.started -= OnSprintToggle;
        PlayerInputActions.Player.Interact.started -= OnInteract;
    }

    public Vector2 GetMoveInput => PlayerInputActions.Player.Move.ReadValue<Vector2>();

    public Vector2 GetMouseInput => PlayerInputActions.Player.Look.ReadValue<Vector2>();

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

    public bool OnSelect => PlayerInputActions.Player.Select.triggered;
}