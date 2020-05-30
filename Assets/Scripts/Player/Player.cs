using UnityEngine;
using System.Collections;
using DarkRift.Client.Unity;
using DarkRift;

public class Player : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The distance we can move before we send a position update.")]
    float moveDistance = 0.05f;

    public UnityClient Client { get; set; }

    Vector3 lastPosition;

    public int currentHealth;
    public int maxHealth;
    public int ID { get; set; }

    public CharacterStats characterStats;

    public PlayerLevel PlayerLevel { get; set; }

    void Awake() {
        lastPosition = transform.position;
        this.maxHealth = 100;
        this.currentHealth = this.maxHealth;
        characterStats = new CharacterStats(10, 10, 10);
        PlayerLevel = GetComponent<PlayerLevel>();
    }

    void Start() {
        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    public void TakeDamage(int amount) {
        currentHealth -= amount;

        Debug.Log("Player took " + amount + " Damage");

        if (currentHealth <= 0) {
            Die();
        }

        UIEventHandler.HealthChanged(this.currentHealth, this.maxHealth);
    }

    void Die() {
        Debug.Log("Player died");
        this.currentHealth = this.maxHealth;
    }

    void Update()
    {
        if (Vector3.Distance(lastPosition, transform.position) > moveDistance)
        {
            lastPosition = transform.position;

            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(transform.position.x);
                writer.Write(transform.position.y);
                writer.Write(transform.position.z);

                using (Message message = Message.Create(Tags.MovePlayerTag, writer))
                    Client.SendMessage(message, SendMode.Unreliable);
            }
        }
    }
}
