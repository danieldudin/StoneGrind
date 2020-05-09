using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AgarObject))]
public class MouseController : MonoBehaviour
{
    AgarObject agarObject;
    public NavMeshAgent playerAgent;
    void Awake()
    {
        playerAgent = this.GetComponent<NavMeshAgent>();
        agarObject = GetComponent<AgarObject>();
    }

    void Update ()
    {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity))
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject.tag == "InteractableObject") {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else {
                agarObject.SetMovePosition(interactionInfo.point);
            }
        }
	}
}
