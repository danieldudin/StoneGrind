using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon, IProjectileWeapon
{
    public List<BaseStat> Stats { get; set; }
    public Animator animator;

    public Transform ProjectileSpawn{ get; set; }

    Fireball fireball;

    void Start() {
        fireball = Resources.Load<Fireball>("Prefabs/Projectiles/Fireball");
        animator = GetComponent<Animator>();
    }

    public void PerformAttack()
    {
        Debug.Log(this.name + " attack!");
        animator.SetTrigger("Base_Attack");
    }

    public void CastProjectile() {
        Fireball fireballInstance = (Fireball)Instantiate(fireball, ProjectileSpawn.position, ProjectileSpawn.rotation);
        fireballInstance.Direction = ProjectileSpawn.forward;
    }
}
