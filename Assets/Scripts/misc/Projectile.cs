using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float time = 3f;
    public int damage = 15;
    PlayerStats playerStats;
    EnemyStats enemyStats;
        
    private void OnCollisionEnter(Collision collision)
    {
        //damage if something hit
        Destroy(gameObject);

        if (collision.gameObject.tag == "Player")
        {
            playerStats = collision.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDmg(damage);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            enemyStats = collision.gameObject.GetComponent<EnemyStats>();
            enemyStats.Damage(damage);
        }
    }
    
    private void Start()
    {
        Destroy(gameObject, time);
    }
}
