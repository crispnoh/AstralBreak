using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float fireRate;
    public float fireForce;

    public GameObject projectilePrefab;

    public Transform target;
    //public Transform rotationPoint;
    public Transform firePoint;

    public Animator anim;

    void Fire()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        projectile.GetComponent<Rigidbody>().AddForce(firePoint.up * fireForce, ForceMode.Impulse);
    }

    void OnTriggerEnter(Collider entity)
    {
        if (entity.tag == "Player")
        {
            anim.SetBool("isAttacking", true);
            InvokeRepeating("Fire", 0.5f, fireRate);
        }
    }

    void OnTriggerStay(Collider entity)
    {
        if (entity.tag == "Player")
            transform.LookAt(target, Vector3.up);
    }

    void OnTriggerExit(Collider entity)
    {
        if (entity.tag == "Player")
        {
            CancelInvoke();
            anim.SetBool("isAttacking", false);
        }
    }
}
