using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Vector3 Direction { get; set; }
    public float Range { get; set; }
    public int Damage { get; set; }

    Vector3 spawnPosition;

    void Start() {
        Range = 15f;
        spawnPosition = transform.position;
        GetComponent<Rigidbody>().AddForce(Direction * 50f);
        Debug.Log("Fireball is gonna hit damage: " + Damage);
    }

    void Update() {
        if (Vector3.Distance(spawnPosition, transform.position) >= Range) {
            Extinguish();
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.transform.tag == "Enemy") {
            col.transform.GetComponent<IEnemy>().TakeDamage(Damage);
            Debug.Log("Enemy hit");
        }
        Extinguish();
    }

    void Extinguish() {
        Destroy(gameObject);
    }
}
