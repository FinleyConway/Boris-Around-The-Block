using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(CameraController))]
public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    public float CurrentMovementSpeed { get; set; }
    private float movementSpeed;
    private float velocityY;

    private Vector2 targetDirection = Vector2.zero;
    private Vector3 currentDirection = Vector3.zero;
    private Vector2 currentDirectionVelocity = Vector2.zero;
    private Vector3 velocity = Vector3.zero;
    private Vector3 RecordedMoveToPos;
    private Vector3 RecordedStartToPos;
    #endregion

    #region State Variables
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState IdleState { get; private set; }
    public PlayerWalkingState WalkingState { get; private set; }
    public PlayerRunState RunState { get; private set; }
    #endregion

    #region Player Components And References
    [SerializeField] private PlayerData playerData;
    [SerializeField] private DialogueUI dialogueUI;

    public CharacterController Controller { get; private set; }
    public CameraController CameraController { get; private set; }
    public InputSystem InputSystem { get; private set; }
    public DialogueUI DialogueUI => dialogueUI;
    public IInteractable Interactable { get; set; }
    #endregion

    #region Unity Functions
    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        IdleState = new PlayerIdleState(this, StateMachine, playerData);
        WalkingState = new PlayerWalkingState(this, StateMachine, playerData);
        RunState = new PlayerRunState(this, StateMachine, playerData);
    }

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
        CameraController = GetComponent<CameraController>();
        InputSystem = InputSystem.Instance;

        StateMachine.Initialize(IdleState);
    }

    private void Update()
    {
        if (dialogueUI.IsOpen)
            return;

        StateMachine.CurrentState.LogicUpdate();

        MovementHandler();
        GravityHandler();
        SetMovementSpeed();
        DialogueHandler();
    }

    private void FixedUpdate()
    {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Set Functions
    public void SetVelocity(Vector2 velocity) => targetDirection.Set(velocity.x, velocity.y);

    public void SetVelocityY(float velocity) => velocityY = velocity;

    // Accelerate the player by slowly lerping movement speed vales between each other. 
    private void SetMovementSpeed() => movementSpeed = Mathf.Lerp(movementSpeed, CurrentMovementSpeed, Time.deltaTime * playerData.movementVelocityBuildUp);
    #endregion

    #region Movement Handlers
    private void MovementHandler()
    {
        // Normalize input values.
        targetDirection.Normalize();

        // Smooths the movement.
        currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref currentDirectionVelocity, playerData.movingSmoothTime);
        // Moves the player.
        velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * movementSpeed + Vector3.up * velocityY;
        Controller.Move(velocity * Time.deltaTime);
    }

    private void GravityHandler()
    {
        // Resets gravity velocity when is grounded.
        if (Controller.isGrounded)
        {
            // Set Y velocity to force to player down.
            velocityY = -2;
        }
        // Rapidly fall faster when not grounded.
        velocityY += playerData.gravity * Time.deltaTime;
    }
    #endregion

    #region DialogueHandler
    private void DialogueHandler()
    {
        if (InputSystem.InteractInput)
        {
            InputSystem.UseInteractInput();
            if (Interactable != null)
            {
                Interactable?.Interact(this);
            }
        }
    }
    #endregion
}