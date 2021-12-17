using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    #region Movement Variables
    [SerializeField] private Transform cameraHolder = null;
    [SerializeField] private Transform feet =  null;

    private float horizontal;
    private float vertical;
    private float movementSpeed;
    private float velocityY;
    private float stepTimer;

    private Vector3 defaultCamHeight;
    private Vector3 velocity;
    private Vector3 currentDirection;
    private Vector2 currentDirectionVelocity;
    private Vector2 targetDirection;

    private bool isRunning;
    #endregion

    #region Camera Variables
    private Vector2 currentMouseDelta;
    private Vector2 currentMouseDeltaVelocity;

    private float mouseX;
    private float mouseY;
    private float xRotation;
    private float defaultCameraYPosition;
    private float headBobbingTimer;
    #endregion

    #region Player Components and References
    [SerializeField] private PlayerData playerData = null;
    private CharacterController controller;
    private Camera cam;
    #endregion

    private void Awake()
    {
        // referecing components
        controller = GetComponent<CharacterController>();
        cam = Camera.main;

        // hides the mouse in game
        Cursor.lockState = CursorLockMode.Locked;

        // setting default values
        defaultCamHeight.y = cam.transform.localPosition.y;
        movementSpeed = playerData.walkingVelocity;
        defaultCameraYPosition = cameraHolder.localPosition.y;
    }

    private void Update()
    {
        if (DialogueManager.instance.isDialoguePlaying)
            return;

        // input
        // temp, unity export system doesnt like Input System ;-;
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        MoveHandler();
        LookHandler();
        if (playerData.canHeadBob)
            HeadBobHandler();
        SetMovementSpeed();
        PlayFootSound();
    }

    // allows the player to move around
    private void MoveHandler()
    {
        #region Gravity Handler
        // resets gravity velocity when is grounded
        if (controller.isGrounded)
        {
            velocityY = 0;
        }
        // rapidly fall faster when not grounded
        velocityY += playerData.gravity * Time.deltaTime;
        #endregion

        #region Move Input
        // gets raw movement input and normalises it
        targetDirection = new Vector2(horizontal, vertical);
        targetDirection.Normalize();
        #endregion

        #region Move Handler
        // smooths the movement
        currentDirection = Vector2.SmoothDamp(currentDirection, targetDirection, ref currentDirectionVelocity, playerData.movingSmoothTime);
        // moves player
        velocity = (transform.forward * currentDirection.y + transform.right * currentDirection.x) * movementSpeed + Vector3.up * velocityY;
        controller.Move(velocity * Time.deltaTime);
        #endregion
    }

    // allows the player to look around
    private void LookHandler()
    {
        // gets raw delta movement input
        Vector2 targetMouseDelta = new Vector2(mouseX, mouseY);

        // smooth look rotations
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, playerData.mouseSmoothTime);

        // setting raw values with mouse sensitivity
        xRotation -= currentMouseDelta.y * playerData.mouseSensitivity;
        // prevents object from breaking its neck
        xRotation = Mathf.Clamp(xRotation, -90f, 90);

        // turns the camera up and down
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        // turns the player left and right
        transform.Rotate(Vector3.up * currentMouseDelta.x * playerData.mouseSensitivity);
    }

    // makes the player feel more realstic with a headbobbing effect
    private void HeadBobHandler()
    {
        if (!controller.isGrounded)
            return;

        if (Mathf.Abs(controller.velocity.x) > 0.1f || Mathf.Abs(controller.velocity.z) > 0.1f)
        {
            headBobbingTimer += Time.deltaTime * (isRunning ? playerData.runBobbingSpeed : playerData.walkBobbingSpeed);
            cameraHolder.localPosition = new Vector3(cameraHolder.localPosition.x, defaultCameraYPosition + Mathf.Sin(headBobbingTimer) * (isRunning ? playerData.runBobbingAmount : playerData.walkBobbingAmount), cameraHolder.localPosition.z);
        }
    }

    // allows the player to gradually change between speeds
    private void SetMovementSpeed()
    {
        if (!controller.isGrounded)
            return;

        if (isRunning)
        {
            movementSpeed = Mathf.Lerp(movementSpeed, playerData.runningVelocity, Time.deltaTime * playerData.runVelocityBuildUp);
        }
        else
        {
            movementSpeed = Mathf.Lerp(movementSpeed, playerData.walkingVelocity, Time.deltaTime * playerData.runVelocityBuildUp);
        }
    }

    private void PlayFootSound()
    {
        float timer = (isRunning ? playerData.runningFootStepDuration : playerData.footStepDuration);
        if (horizontal != 0 || vertical != 0)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer < 0)
            {
                stepTimer = timer;
                SoundManager.i.PlaySound("FootStep", feet.position);
            }
        }
    }
}