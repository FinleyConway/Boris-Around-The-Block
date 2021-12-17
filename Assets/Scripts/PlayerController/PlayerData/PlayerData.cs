using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewPlayerData", menuName ="Data/BaseData/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Moving Variables")]
    [Range(0.0f, 0.5f)] public float movingSmoothTime;
    public float runVelocityBuildUp;
    public float walkingVelocity;
    public float runningVelocity;
    public float footStepDuration;
    public float runningFootStepDuration;

    [Header("Check Variables")]
    public float gravity = -9.81f;
    public LayerMask ground;

    [Header("Camera Variables")]
    public bool canHeadBob;
    [Range(0.1f, 10)] public float mouseSensitivity;
    [Range(0.0f, 0.5f)] public float mouseSmoothTime;
    public float walkBobbingSpeed = 14;
    public float walkBobbingAmount = 0.05f;
    public float runBobbingSpeed = 18;
    public float runBobbingAmount = 0.1f;
}