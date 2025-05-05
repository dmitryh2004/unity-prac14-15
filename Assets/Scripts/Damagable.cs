using UnityEngine;

public class Damagable : MonoBehaviour
{
    int health;
    [SerializeField] int maxHealth;

    public void InitHealth()
    {
        health = maxHealth;
    }

    public int GetHealth()
    {
        return health;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Heal(int amount)
    {
        health += amount;
        health = Mathf.Min(health, maxHealth);
    }

    public virtual void OnDeath()
    {

    }

    void Die()
    {
        OnDeath();
        Destroy(gameObject);
    }
}
