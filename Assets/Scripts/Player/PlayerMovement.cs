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

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

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
                _navMeshAgent.SetDestination(placedTarget.transform.position);
            }
        }
    }
}
