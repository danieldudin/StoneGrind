using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Skeleton : MonoBehaviour, IEnemy
{
    public Animator skeletonAnimator;
    private Player player;
    public LayerMask aggroLayerMask;
    private NavMeshAgent navAgent;
    public float currentHealth;
    public float maxHealth;
    public int Experience { get; set; }

    private CharacterStats characterStats;
    private Collider[] withinAggroColliders;

    void Start() {
        maxHealth = 100;
        Experience = 34;
        navAgent = GetComponent<NavMeshAgent>();
        skeletonAnimator = GetComponent<Animator>();
        characterStats = new CharacterStats(6, 10, 2);
        currentHealth = maxHealth;
    }

    void FixedUpdate() {
        withinAggroColliders = Physics.OverlapSphere(transform.position, 5, aggroLayerMask);

        if (withinAggroColliders.Length > 0) {
            ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
        } else {
            skeletonAnimator.SetBool("isWalking", false);
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

    public void Die() {
        CombatEvents.EnemyDied(this);
        Destroy(gameObject);
    }

    void ChasePlayer(Player player) {
        this.player = player;
        navAgent.SetDestination(player.transform.position);
        skeletonAnimator.SetBool("isWalking", true);

        if (navAgent.remainingDistance <= navAgent.stoppingDistance) {
            skeletonAnimator.SetBool("isWalking", false);
            skeletonAnimator.SetBool("isAttacking", true);

            if (!IsInvoking("PerformAttack")) {
                InvokeRepeating("PerformAttack", .5f, 2f);
            }
        } else {
            skeletonAnimator.SetBool("isAttacking", false);
            CancelInvoke();
        }
    }
}
