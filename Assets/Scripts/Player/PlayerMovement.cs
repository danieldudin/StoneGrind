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

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        playerAnimator = GetComponent<Animator>();

        if (PV.IsMine) {
            placedTarget = GameObject.Find("TargetPointer").GetComponent<PlaceTarget>().gameObject;

            _navMeshAgent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();

            if (_navMeshAgent == null)
            {
                Debug.Log("The nav mesh agent component is not attached to the Character");
            }
        }
    }

    void Update() {
        if (PV.IsMine) {
            if (Vector3.Distance(_navMeshAgent.destination, placedTarget.transform.position) > 0.5f)
            {
                playerAnimator.SetInteger("condition", 1);
                playerAnimator.SetBool("running", true);

                _navMeshAgent.SetDestination(placedTarget.transform.position);
            } 
            else if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
            {
                if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f)
                {
                    playerAnimator.SetInteger("condition", 0);
                    playerAnimator.SetBool("running", false);
                }
            }

            if (Input.GetKey(KeyCode.Alpha1)) {
                Attack();
            }
        }
    }

    void Attack() {
        StartCoroutine(AttackRoutine());
    }

    IEnumerator AttackRoutine() {
        playerAnimator.SetBool("attacking", true);
        playerAnimator.SetInteger("condition", 2);

        yield return new WaitForSeconds(1);

        playerAnimator.SetInteger("condition", 0);
        playerAnimator.SetBool("attacking", false);
    }
}
