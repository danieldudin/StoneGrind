using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(PlayerObject))]
public class MouseController : MonoBehaviour
{
    PlayerObject playerObject;
    public NavMeshAgent playerAgent;
    void Awake() {
        playerAgent = this.GetComponent<NavMeshAgent>();
        playerObject = GetComponent<PlayerObject>();
    }

    void Update () {
        if (!Input.GetMouseButton(0) || PointerOverUI())
        {
            return;
        }

        GetInteraction();
    }

    void GetInteraction() {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity)) {
            playerAgent.updateRotation = true;

            GameObject interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject.tag == "Enemy") {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            } else if (interactedObject.tag == "InteractableObject") {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else {
                playerObject.SetMovePosition(interactionInfo.point);
            }
        }
    }

    public bool PointerOverUI() {
        return UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject();
    }

    void OnCollisionEnter(Collision collision) {

        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
