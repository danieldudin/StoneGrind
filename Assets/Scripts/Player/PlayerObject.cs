using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Renderer))]
public class PlayerObject : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The speed that the player will move.")]
    float speed = 1f;

    Vector3 movePosition;

    public NavMeshAgent playerAgent;

    void Start() {
        playerAgent = this.GetComponent<NavMeshAgent>();
    }

    void Awake()
    {
        movePosition = transform.position;
    }

    internal void SetMovePosition(Vector3 newPosition)
    {
        playerAgent.stoppingDistance = 0;
        playerAgent.destination = newPosition;
    }
}
