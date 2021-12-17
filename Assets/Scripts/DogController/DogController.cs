using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogController : MonoBehaviour
{
    [SerializeField] private Transform followerLocation;

    private float wanderTimer;
    private float barkTimer;
    private float distanceBetweenPlayer;

    [SerializeField] private DogData dogData;
    private NavMeshAgent agent;
    private Animator anim;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        wanderTimer = dogData.wanderDuration;
        barkTimer = dogData.timeBeforePotentialBark;
    }

    private void Update()
    {
        // gets distance between player and dog
        distanceBetweenPlayer = Vector3.Distance(followerLocation.position, transform.position);

        // if player is too far away, dog run to player
        if (distanceBetweenPlayer >= dogData.distanceBeforeFollow)
        {
            // dog travel to player
            agent.SetDestination(followerLocation.position);
        }
        else
        {
            // dogs wander around player when idle
            wanderTimer += Time.deltaTime;
            if (wanderTimer >= dogData.wanderDuration)
            {
                Vector3 wanderPos = RandomLocation(followerLocation.position, dogData.wanderRadius, -1);
                agent.SetDestination(wanderPos);
                wanderTimer = 0;
            }
        }

        // bark
        DoBark();

        // animation
        DoAnimation();
    }

    // get random location in a area
    private Vector3 RandomLocation(Vector3 origin, float distance, int layerMask)
    {
        // get a random location in a area
        Vector3 randomPoint = Random.insideUnitSphere * distance;
        randomPoint += origin;

        // raycast positon for navmesh to follow towards to
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomPoint, out navHit, distance, layerMask);
        return navHit.position;
    }

    // a chance to bark every x seconds
    private void DoBark()
    {
        barkTimer -= Time.deltaTime;
        if (barkTimer < 0)
        {
            if (Random.value <= dogData.chanceOfBark)
            {
                SoundManager.i.PlaySound("Woof", transform.position);
                barkTimer = dogData.timeBeforePotentialBark;
            }
        }
    }

    private void DoAnimation()
    {
        if (agent.velocity != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(followerLocation.position, dogData.wanderRadius);
    }
}
