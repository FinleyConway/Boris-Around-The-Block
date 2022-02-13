using UnityEngine;

[CreateAssetMenu(fileName ="NewPlayerData", menuName ="Data/BaseData/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Moving Variables")]
    [Range(0.0f, 0.5f)] public float movingSmoothTime;
    public float movementVelocityBuildUp;
    public float walkingVelocity;
    public float runningVelocity;

    [Header("Check Variables")]
    public float gravity = -9.81f;

    [Header("Camera Variables")]
    [Range(0.1f, 1)] public float mouseSensitivity;
    [Range(0.0f, 0.5f)] public float mouseSmoothTime;
    public float maxCameraClamp = 90;
    public float minCameraClamp = -90;
}