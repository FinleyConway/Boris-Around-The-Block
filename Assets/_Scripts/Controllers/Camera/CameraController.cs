using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class CameraController : MonoBehaviour
{
    [Header("Camera Location")]
    [SerializeField] private Transform cameraHolder = null;
    public Transform CameraHolder => cameraHolder;
    [SerializeField] private PlayerData playerData;

    private Vector2 currentMouseDelta = Vector2.zero;
    private Vector2 currentMouseDeltaVelocity = Vector2.zero;

    private bool isSelfRelative;

    private float xRotation;

    public Camera cam { get; private set; }
    private InputSystems inputSystem;
    [SerializeField] private DialogueUI dialogueUI;

    private void Start()
    {
        inputSystem = InputSystems.Instance;
        cam = Camera.main;

        isSelfRelative = true;
    }

    private void LateUpdate()
    {
        if (dialogueUI.IsOpen)
            return;

        CameraLookRotationHandler();

        cam.transform.rotation = Quaternion.Euler(new Vector2(cam.transform.rotation.eulerAngles.x, CameraHolder.transform.parent.rotation.eulerAngles.y));
        cam.transform.localPosition = CameraHolder.position;
    }

    private void CameraLookRotationHandler()
    {
        // Gets raw delta movement input.
        Vector2 targetMouseDelta = inputSystem.GetMouseInput;

        // Smooth look rotations.
        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, playerData.mouseSmoothTime);

        // Setting raw values with mouse sensitivity.
        currentMouseDelta.x *= playerData.mouseSensitivity;
        currentMouseDelta.y *= playerData.mouseSensitivity;
        xRotation -= currentMouseDelta.y;

        // Prevents object from breaking its neck.
        xRotation = Mathf.Clamp(xRotation, playerData.minCameraClamp, playerData.maxCameraClamp);
        // Turns the camera up and down.
        cam.transform.localRotation = Quaternion.Euler(xRotation, cam.transform.eulerAngles.y, 0);

        // Turns the player left and right.
        transform.Rotate(isSelfRelative ? Vector3.up * currentMouseDelta.x : Vector3.zero);
    }

    public void SetCameraRelative(bool newState) => isSelfRelative = newState;
}
