using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    //public GameObject explosionEffect;
    public float fuseTime;
    public float blastRadius;
    public float explosionForce;
    public int dmg = 25;
    public bool exploded = false;

    void Start()
    {
        Invoke("Explode", fuseTime);
    }

    private void Explode()
    {
        // damage enemy here
        exploded = true;

        Collider[] colliders = Physics.OverlapSphere(transform.position, blastRadius);

        foreach(Collider near in colliders)
        {
            Rigidbody rb = near.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(explosionForce, transform.position, blastRadius, 1f, ForceMode.Impulse);
        }

        //Instantiate(explosionEffect, transform.position, transform.rotation);
        Destroy(gameObject,0.05f);
    }

    void OnTriggerStay(Collider entity)
    {
        if (exploded && entity.tag == "Enemy")
        {
            //Debug.Log("exploded");
            exploded = false;
            entity.GetComponent<EnemyStats>().Damage(dmg); 
        }
    }
}
