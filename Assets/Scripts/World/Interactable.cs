using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    public virtual void MoveToInteraction(NavMeshAgent playerAgent) {
        this.playerAgent = playerAgent;

        playerAgent.stoppingDistance = 2f;
        playerAgent.destination = this.transform.position;

        Interact();
    }

    public virtual void Interact() 
    {
        Debug.Log("Interacting with base class");
    }
}
