using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Handles collision logic for the Player to interact with specific objects.
    /// </summary>
    public class PlayerCollisionHandler : MonoBehaviour
    {
        private void Update()
        {
            // Robust check using an OverlapSphere to detect the door or its parent
            CheckForDoorProximity();
        }

        private void CheckForDoorProximity()
        {
            // Checks a radius around the player for objects with the DOOR tag
            Collider[] hitColliders = Physics.OverlapSphere(transform.position + Vector3.up, 1.5f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("DOOR"))
                {
                    Debug.Log($"[PlayerCollisionHandler] Found Door via proximity: {hitCollider.gameObject.name}");
                    Destroy(hitCollider.gameObject);
                }
                // Check if the parent is tagged DOOR (common in modular sets)
                else if (hitCollider.transform.parent != null && hitCollider.transform.parent.CompareTag("DOOR"))
                {
                    Debug.Log($"[PlayerCollisionHandler] Found Parent Door via proximity: {hitCollider.transform.parent.name}");
                    Destroy(hitCollider.transform.parent.gameObject);
                }
            }
        }

        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            ProcessHit(hit.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            ProcessHit(other.gameObject);
        }

        private void ProcessHit(GameObject hitObject)
        {
            if (hitObject.CompareTag("DOOR"))
            {
                Debug.Log($"[PlayerCollisionHandler] Destroying Door: {hitObject.name}");
                Destroy(hitObject);
            }
            else if (hitObject.transform.parent != null && hitObject.transform.parent.CompareTag("DOOR"))
            {
                Debug.Log($"[PlayerCollisionHandler] Destroying Parent Door: {hitObject.transform.parent.name}");
                Destroy(hitObject.transform.parent.gameObject);
            }
        }
    }
}
