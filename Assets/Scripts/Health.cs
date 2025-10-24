using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int health;
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public abstract void Die();
}
