using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "NewDogData", menuName = "Data/BaseData/DogData")]
public class DogData : ScriptableObject
{
    [Header("Wander Attributes")]
    public float wanderRadius;
    public float wanderDuration;
    public float distanceBeforeFollow;

    [Header("Barking")]
    public float timeBeforePotentialBark;
    [Range(0.1f, 1f)] public float chanceOfBark;
}
