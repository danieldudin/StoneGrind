using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent playerAgent;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();

        playerAgent = this.GetComponent<NavMeshAgent>();

        if (playerAgent == null)
        {
            Debug.Log("The nav mesh agent component is not attached to the Character");
        }

    }

    void Update() {
        if (!playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
            {
                if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude == 0f)
                {
                    // Destination reached
                }
            }
        }

        if (!Input.GetMouseButton(0) || PointerOverUI())
        {
            return;
        }

        GetInteraction();
    }

    void GetInteraction() {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject.tag == "InteractableObject") {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else {
                playerAgent.stoppingDistance = 0;
                playerAgent.destination = interactionInfo.point;
            }
        }
    }

    public bool PointerOverUI()
    {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
