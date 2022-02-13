using UnityEngine;

public class RigidbodyInteraction : MonoBehaviour
{
    [Header("How much force the player will apply")]
    [SerializeField] private float pushForce = 0.75f;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;

        if (rigidbody == null || rigidbody.isKinematic) return;

        if (hit.moveDirection.y < -0.3f) return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);

        rigidbody.velocity = pushDirection * pushForce;
    }
}
