using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int health = 50;

    void OnTriggerEnter2D(Collider2D collision)
    {
        DamageDealer damageDealer = collision.GetComponent<DamageDealer>();

        if (damageDealer != null)
        {
            TakeDamage(damageDealer.Damage);
            damageDealer.Hit();
        }
    }

    void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) Destroy(gameObject);
    }
}
