using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public NavMeshAgent playerAgent;
    private PhotonView PV;

    public Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();
        playerAnimator = GetComponent<Animator>();

        if (PV.IsMine) {
            playerAgent = this.GetComponent<NavMeshAgent>();

            if (playerAgent == null)
            {
                Debug.Log("The nav mesh agent component is not attached to the Character");
            }
        }
    }

    void Update() {
        if (PV.IsMine) {

            if (!playerAgent.pathPending)
            {
                if (playerAgent.remainingDistance <= playerAgent.stoppingDistance)
                {
                    if (!playerAgent.hasPath || playerAgent.velocity.sqrMagnitude == 0f)
                    {
                        playerAnimator.SetBool("running", false);
                        playerAnimator.SetInteger("condition", 0);
                    }
                }
            }

            if (Input.GetKey(KeyCode.Alpha1))
            {
                Attack();
            }

            if (!Input.GetMouseButton(0) || PointerOverUI())
            {
                return;
            }

            GetInteraction();
        }
    }

    void GetInteraction() {
        Ray interactionRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(interactionRay, out interactionInfo, Mathf.Infinity)) 
        {
            GameObject interactedObject = interactionInfo.collider.gameObject;

            if (interactedObject.tag == "InteractableObject") 
            {
                interactedObject.GetComponent<Interactable>().MoveToInteraction(playerAgent);
            }
            else 
            {
                playerAgent.stoppingDistance = 0;
                playerAgent.destination = interactionInfo.point;

                playerAnimator.SetInteger("condition", 1);
                playerAnimator.SetBool("running", true);
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
