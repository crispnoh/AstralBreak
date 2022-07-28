using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public float animationTime;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void Damage(int dmg)
    {
        if (currentHealth - dmg <= 0) { Death(); }
        else { currentHealth -= dmg; }
    }

    void Death()
    {
        //play death animation
        Destroy(gameObject, animationTime);
    }
}
