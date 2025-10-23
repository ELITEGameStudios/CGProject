using UnityEngine;

public abstract class Health : MonoBehaviour
{
    public int health;
    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {

        }
    }

    public abstract void Die();
}
