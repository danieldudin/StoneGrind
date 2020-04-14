using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent _navMeshAgent;

    public GameObject placedTarget;
    private PhotonView PV;

    Animator animation;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        animation = GetComponent<Animator>();

        if (PV.IsMine) {
            placedTarget = GameObject.Find("TargetPointer").GetComponent<PlaceTarget>().gameObject;

            _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (_navMeshAgent == null)
            {
                Debug.Log("The nav mesh agent component is not attached to the Character");
            }
            else
            {
                _navMeshAgent.SetDestination(placedTarget.transform.position);
            }
        }
    }

    void Update() {
        if (PV.IsMine) {
            if (Vector3.Distance(_navMeshAgent.destination, placedTarget.transform.position) > 1.0f)
            {
                animation.SetInteger("condition", 1); 
                _navMeshAgent.SetDestination(placedTarget.transform.position);
            }
            if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    animation.SetInteger("condition", 0);
                }
            }
        }
    }
}
