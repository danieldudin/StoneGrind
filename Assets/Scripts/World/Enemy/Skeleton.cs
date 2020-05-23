using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour, IEnemy
{   
    private Player player;
    public LayerMask aggroLayerMask;
    private NavMeshAgent navAgent;
    public float currentHealth;
    public float maxHealth;

    private CharacterStats characterStats;
    private Collider[] withinAggroColliders;

    void Start() {
        navAgent = GetComponent<NavMeshAgent>();
        characterStats = new CharacterStats(6, 10, 2);
        currentHealth = maxHealth;
    }

    void FixedUpdate() {
        withinAggroColliders = Physics.OverlapSphere(transform.position, 3, aggroLayerMask);

        if (withinAggroColliders.Length > 0) {
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        }
    }

    public void PerformAttack()
    {
        player.TakeDamage(5);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0) {
            Die();
        }
    }

    void ChasePlayer(Player player) {
        this.player = player;
        navAgent.SetDestination(player.transform.position);

        if (navAgent.remainingDistance <= navAgent.stoppingDistance) {
            if (!IsInvoking("PerformAttack")) {
                InvokeRepeating("PerformAttack", .5f, 2f);
            }
        } else {
            CancelInvoke();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
